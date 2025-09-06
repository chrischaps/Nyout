using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] public int Owner;
    [SerializeField] private float moveSpeed = 0.5f;
    public Node CurrentNode { get; set; }
    public Node PreviousNode { get; set; }
    
    public async Awaitable MoveToNextNode()
    {
        Node nextNode;
        if (CurrentNode == null)
        {
            nextNode = GameManager.Instance.StartingNode;
        }
        else
        {
            nextNode = CurrentNode.OutgoingEdges.First(x => x != PreviousNode);
        }
                
        if (nextNode == null) return; // No more nodes to move to
        await MoveToNode(nextNode);
    }
    
    public async Awaitable MoveToNode(Node node)
    {
        CurrentNode.RemoveHorse(this);
        PreviousNode = CurrentNode;
        CurrentNode = null;
        
        EventManager.Instance.FireEvent(EventType.PlayerMoved);
        
        transform.DOMove(node.transform.position, moveSpeed).SetEase(Ease.InOutQuad);
        await Awaitable.WaitForSecondsAsync(moveSpeed);
        
        node.AddOccupant(this);
        CurrentNode = node;
        transform.position = node.transform.position;
    }
    
    public async Awaitable SendHome()
    {
        if (CurrentNode == null) return; // Already at home
        var homeNode = GameManager.Instance.GetPlayerById(Owner).GetAvailableHomeNode();
        await MoveToNode(homeNode);
    }
    
    public void HighlightOptions(bool shouldHighlight)
    {
        foreach (var node in CurrentNode.OutgoingEdges)
        {
            if (node != PreviousNode)
            {
                node.HighlightNode(shouldHighlight);
            }
        }
    }
}
