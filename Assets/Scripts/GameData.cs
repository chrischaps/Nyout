using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField] public Node startingNode;
    int numPlayers = 2;
    int currentPlayer = 0;
}
