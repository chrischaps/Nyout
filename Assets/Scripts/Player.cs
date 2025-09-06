using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Horse> horses;
    
    [SerializeField] public List<Node> HomeNodes;
    [SerializeField] private GameObject horsePrefab;
    [SerializeField] public int PlayerId;

    public void Start()
    {
        foreach (var node in HomeNodes)
        {
            var horseGO = Instantiate(horsePrefab);
            if (horseGO == null)
            {
                Debug.LogWarning("Couldn't instantiate horsePrefab");
                continue;
            }
            var horse = horseGO.GetComponent<Horse>();
            horse.Owner = this.PlayerId;
            node.AddOccupant(horse);
            horse.CurrentNode = node;
            horse.transform.position = node.transform.position;
        }
    }
    
    public Node GetAvailableHomeNode()
    {
        foreach (var homeNode in HomeNodes)
        {
            var node = homeNode.GetComponent<Node>();
            if (!node.Occupied())
            {
                return node;
            }
        }
        
        Debug.LogWarning($"Player {name} has no available home nodes.");
        return null; // No available home nodes
    }
}
