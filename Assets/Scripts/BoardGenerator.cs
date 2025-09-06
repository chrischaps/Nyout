using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private int radius = 100;
    [SerializeField] private int perimeterSubdivisions = 4;
    [SerializeField] private int radialSubdivisions = 2;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject cardinalNodePrefab;
    [SerializeField] private GameObject centerNodePrefab;
    private List<GameObject> nodes;
    
    public void GenerateBoard()
    {
        ClearBoard();
        
        CreateCenterNode();
        
        for (var i = 0; i < perimeterSubdivisions; i++)
        {
            var angle = (360f / perimeterSubdivisions) * i;
            if (IsApproximatelyCardinalDirection(angle))
            {
                CreateCardinalNode(angle);
                CreateRadialNodes(angle);
            }
            else
            {
                CreateNode(angle, radius);
            }
        }

        Debug.Log("Board generated!");
    }

    private void CreateRadialNodes(float angle)
    {
        for (int i = 0; i < radialSubdivisions; i++)
        {
            float radialDistance = radius * (i + 1) / (radialSubdivisions + 1);
            CreateNode(angle, radialDistance);
        }
    }

    private void CreateCenterNode()
    {
        var centerNode = PrefabUtility.InstantiatePrefab(centerNodePrefab, this.transform) as GameObject;
        nodes.Add(centerNode);
    }
    
    private void CreateNode(float angle, float radialDistance)
    {
        var node = PrefabUtility.InstantiatePrefab(nodePrefab, this.transform) as GameObject;
        var point = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radialDistance;
        node!.transform.position = point;
        nodes.Add(node);
    }
    
    private void CreateCardinalNode(float angle)
    {
        var node = PrefabUtility.InstantiatePrefab(cardinalNodePrefab, this.transform) as GameObject;
        var point = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
        node!.transform.position = point;
        nodes.Add(node);
    }
    
    private bool IsApproximatelyCardinalDirection(float angle)
    {
        return IsApproximatelyEqual(angle, 0f) || IsApproximatelyEqual(angle, 90f) ||
               IsApproximatelyEqual(angle, 180f) || IsApproximatelyEqual(angle, 270f);
    }
    
    private bool IsApproximatelyEqual(float a, float b, float tolerance = 0.1f)
    {
        return Mathf.Abs(a - b) < tolerance;
    }
    
    private void ClearBoard()
    {
        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                if (node != null)
                {
                    DestroyImmediate(node);
                }
            }
        }

        nodes = new List<GameObject>();
        Debug.Log("Board cleared!");
    }
}

[CustomEditor(typeof(BoardGenerator))]
public class BoardGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        BoardGenerator boardGenerator = (BoardGenerator)target;
        
        if (GUILayout.Button("Generate Board"))
        {
            boardGenerator.GenerateBoard();
        }
    }
}