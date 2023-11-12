using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ModifierType
{
    MaxHealth,
    MoveSpeed,
    Damage,
    CritChance,
    CritDamage,
    LifeSteal,
    DamageReduction,
    FireRate,
    PointsGain,
    ShooterProjectileAmount
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
public class Modifier : ScriptableObject, IUsable
{
    public Sprite sprite;
    public List<ModifierData> datas;
    public string desc;

    public string GetName()
    {
        return name;
    }

    public void Use()
    {
        ItemModifierManager.instance.AddModifier(this);
    }

    public string GetDesc()
    {
        return desc;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}