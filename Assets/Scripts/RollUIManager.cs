using UnityEngine;
using UnityEngine.UIElements;

public class RollUIManager : MonoBehaviour
{
    private Label rollAmountLabel;
    private VisualElement container;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        rollAmountLabel = uiDocument.rootVisualElement.Q<Label>("RollAmount");
        rollAmountLabel.dataSource = GameManager.Instance;
        container = uiDocument.rootVisualElement.Q<Button>("Container");
    }
    
    private void OnRollButtonClicked()
    {
        Debug.Log("Roll button clicked!");
        // Here you would typically call a method in GameManager to handle the dice roll
        // For example: GameManager.Instance.RollDice();
        GameManager.Instance.RollDice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
