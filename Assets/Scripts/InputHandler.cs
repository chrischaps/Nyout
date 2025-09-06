using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera camera;
    
    void Start()
    {
        if (camera == null) camera = Camera.main;
    }

    void OnWorldClick()
    {
        Debug.Log("OnWorldClick detected!");
        EventManager.Instance.FireEvent(EventType.MouseClick);
        
        var hits = Physics2D.GetRayIntersectionAll(camera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;
            
            var clickedObject = hit.collider.gameObject;
            Debug.Log("Clicked on: " + clickedObject.name);
                
            if (clickedObject.TryGetComponent<Horse>(out var horse))
            {
                Debug.Log("Selected Horse: " + horse.name);
                GameManager.Instance.SelectHorse(horse);
            }
            else if (clickedObject.TryGetComponent<Node>(out var node))
            {
                // Handle node selection
                Debug.Log("Selected Node: " + node.name);
                GameManager.Instance.SelectNode(node);
            }
        }
    }
}
