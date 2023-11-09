using UnityEngine;

[System.Serializable]
public enum ModifierType
{
    Health,
    MoveSpeed,
    Damage,
    AttackSpeed,
    CritChance,
    CritDamage,
    LifeSteal
}

public class Modifier : ScriptableObject
{
    [SerializeField] public float value;
}