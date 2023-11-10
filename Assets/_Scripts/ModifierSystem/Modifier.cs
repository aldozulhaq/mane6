using System.Collections.Generic;
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
    LifeSteal,
    DamageReduction,
    FireRate,
    PointsGain
}

[System.Serializable]
public enum ModifierOperation
{
    Add,
    Subtract
}

[System.Serializable]
public struct ModifierData
{
    public ModifierType modifierType;
    public ModifierOperation modifierOperation;
    public float value;
}

[CreateAssetMenu(fileName = "New Modifier", menuName = "Modifier")]
public class Modifier : ScriptableObject
{
    public Sprite sprite;
    public List<ModifierData> datas;

    public string GetName()
    {
        return name;
    }

    /// <summary>
    /// Apply the modifiers to the PlayerStats.
    /// </summary>
    public void ApplyModifiers()
    {
        PlayerStats.ApplyModifiers(datas);
    }
}