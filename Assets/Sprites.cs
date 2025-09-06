using UnityEngine;

[CreateAssetMenu(fileName = "Sprites", menuName = "Scriptable Objects/Sprites")]
public class Sprites : ScriptableObject
{
    [SerializeField] private Sprite BoardBackground;
    [SerializeField] private Sprite BoardSquare;
}
