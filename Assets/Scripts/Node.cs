using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> OutgoingEdges;
    [SerializeField] private GameObject highlight;

    private List<Horse> occupants = new List<Horse>();
    
    public bool HasMultipleEdges => OutgoingEdges.Count > 2;
    
    public Node DefaultNode => OutgoingEdges[0];
    
    
    public void HighlightNode(bool shouldHighlight)
    {
        if (highlight != null)
        {
            highlight.SetActive(shouldHighlight);
        }
    }

    public void KickOutSquatters(Horse newHorse)
    {
        var squatter = occupants.Find(x => x.Owner != newHorse.Owner);
        if (squatter != null)
        {
            Debug.LogWarning($"Node {name} already occupied by horse of owner {squatter.Owner}. Kicking out horse of owner {squatter.Owner}.");
            squatter.SendHome();
            EventManager.Instance.FireEvent(EventType.PlayerBumped);
        }
    }
    
    public void AddOccupant(Horse horse)
    {
        Debug.Log($"Occupying {this.name}");

        occupants.Add(horse);
        
    }

    public bool Occupied()
    {
        return occupants.Count > 0;
    }
    
    public void RemoveHorse(Horse horse)
    {
        if (occupants != null && occupants.Contains(horse))
        {
            occupants.Remove(horse);
        }
    }
}
