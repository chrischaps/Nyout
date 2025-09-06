using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private Label playerTurnLabel;
    private Button rollButton;
    private Label rollAmountLabel;
    
    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        playerTurnLabel = uiDocument.rootVisualElement.Q<Label>("PlayerTurnLabel");
        rollButton = uiDocument.rootVisualElement.Q<Button>("RollButton");
        rollButton.clicked += OnRollButtonClicked;
        
        rollAmountLabel = uiDocument.rootVisualElement.Q<Label>("CurrentRollValue");
        rollAmountLabel.SetBinding("text", new DataBinding
        {
            dataSourcePath = PropertyPath.FromName(nameof(GameManager.CurrentRollValue)),
            bindingMode = BindingMode.ToTarget,
            dataSource = GameManager.Instance
        });
    }
    
    private void OnRollButtonClicked()
    {
        Debug.Log("Roll button clicked!");
        GameManager.Instance.RollDice();
    }
}
