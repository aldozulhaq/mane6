using UnityEngine;

[CreateAssetMenu(fileName = "New Power Up", menuName = "Power Up")]
public class Weapon : ScriptableObject
{
    [SerializeField] string powerUpName;
    [SerializeField] string description;
    [SerializeField] Sprite icon;
    [SerializeField] GameObject prefab;
}