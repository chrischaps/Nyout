using System;
using System.Linq;
using DefaultNamespace;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public enum GameState
{
    WaitingForRoll,
    WaitingForHorseSelection,
    WaitingForNodeSelection,
    MovingHorses,
    GameOver
}

public class GameManager : MonoBehaviour, INotifyBindablePropertyChanged
{
    [SerializeField] private int numPlayers = 2;
    [SerializeField] public Node StartingNode;

    public int CurrentPlayer;

    public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;
    private GameState _currentGameState;
    [CreateProperty]
    public GameState CurrentGameState
    {
        get => _currentGameState;
        set
        {
            _currentGameState = value;
            propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(nameof(CurrentGameState)));
            Debug.Log("Game State Changed: " + _currentGameState);
        }
    }
    
    public static GameManager Instance { get; private set; }

    [SerializeField, DontCreateProperty] 
    private int _currentRollValue;
    [CreateProperty]
    public int CurrentRollValue
    {
        get => _currentRollValue;
        set
        {
            _currentRollValue = value;
            propertyChanged?.Invoke(this, new BindablePropertyChangedEventArgs(""));
            Debug.Log("Roll Value: " + _currentRollValue);
        }
    }
    private Horse selectedHorse;
    private AwaitableCompletionSource<Node> nodeSelection = new AwaitableCompletionSource<Node>();
    
    void Awake()
    {
        Instance = this;
    }

    public void RollDice()
    {
        Debug.Log("Roll Dice");
        var diceRolls = GetDiceRolls();
        CurrentRollValue = GetDiceValue(diceRolls);
        EventManager.Instance.FireEvent(EventType.DiceRolled);
    }

    private int[] GetDiceRolls()
    {
        var numDice = 4;
        var numSidesPerDice = 2;
        
        var diceRolls = new int[numDice];
        for (int i = 0; i < numDice; i++)
        {
            diceRolls[i] = Random.Range(0, numSidesPerDice);
        }

        return diceRolls;
    }
    
    private int GetDiceValue(int[] rolls)
    {
        var value = rolls.Sum();
        if (rolls.IsAllTails()) value = 5;
        Debug.Log("Dice Rolls: " + string.Join(", ", rolls) + " | Value: " + value);
        
        return value;
    }

    public void SelectHorse(Horse horse)
    {
        selectedHorse = horse;
        MoveSelectedHorse();
    }
    
    public void SelectNode(Node node)
    {
        if (selectedHorse == null) return;
        nodeSelection.SetResult(node);
        nodeSelection.Reset();
    }

    private async void MoveSelectedHorse()
    {
        Node firstNode;
        if (selectedHorse.CurrentNode?.HasMultipleEdges ?? false)
        {
            selectedHorse.HighlightOptions(true);
            firstNode = await nodeSelection.Awaitable;
            selectedHorse.HighlightOptions(false);
        }
        else
        {
            var outgoingEdges = selectedHorse.CurrentNode?.OutgoingEdges;
            var autoEdge = outgoingEdges?.First(x => x != selectedHorse.PreviousNode);
            firstNode = autoEdge;
            //firstNode = selectedHorse.CurrentNode?.DefaultNode;
        }
        await selectedHorse.MoveToNode(firstNode);
        
        for (var i = 0; i < CurrentRollValue - 1; i++)
        {
            await selectedHorse.MoveToNextNode();
        }
        selectedHorse.CurrentNode?.KickOutSquatters(selectedHorse);
        selectedHorse = null;
        CurrentRollValue = 0;
    }
    
    public Player GetPlayerById(int playerId)
    {
        return FindObjectsOfType<Player>().FirstOrDefault(player => player.GetComponent<Player>().PlayerId == playerId);
    }
}
