using UnityEngine;

public class PowerUp : ScriptableObject
{
    [SerializeField] string powerUpName;
    [SerializeField] string description;
    [SerializeField] Sprite icon;
    [SerializeField] GameObject prefab;
}