using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public delegate void MaxHealthChanged(float newMaxHealth);
    public static event MaxHealthChanged OnMaxHealthChanged;

    public static float maxHealth = 100f;
    public static float moveSpeedPercentage = 100f;
    public static float damagePercentage = 100f;
    public static float attackSpeedPercentage = 100f;
    public static float fireRatePercentage = 100f;
    public static float critChancePercentage = 0f;
    public static float critDamagePercentage = 100f;
    public static float lifeStealPercentage = 0f;
    public static float damageReducePercentage = 0f;
    public static float pointsGainPercentage = 100f;
    public static float shooterProjectileAmount = 1f;

    public static void ApplyModifiers(List<ModifierData> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            switch (modifier.modifierType)
            {
                case ModifierType.MaxHealth:
                    ApplyModifier(ref maxHealth, modifier);
                    OnMaxHealthChanged?.Invoke(maxHealth); // Notify subscribers about the change
                    break;
                case ModifierType.MoveSpeed:
                    ApplyModifier(ref moveSpeedPercentage, modifier);
                    break;
                case ModifierType.Damage:
                    ApplyModifier(ref damagePercentage, modifier);
                    break;
                case ModifierType.AttackSpeed:
                    ApplyModifier(ref attackSpeedPercentage, modifier);
                    break;
                case ModifierType.CritChance:
                    ApplyModifier(ref critChancePercentage, modifier);
                    break;
                case ModifierType.CritDamage:
                    ApplyModifier(ref critDamagePercentage, modifier);
                    break;
                case ModifierType.LifeSteal:
                    ApplyModifier(ref lifeStealPercentage, modifier);
                    break;
                case ModifierType.DamageReduction:
                    ApplyModifier(ref damageReducePercentage, modifier);
                    break;
                case ModifierType.FireRate:
                    ApplyModifier(ref fireRatePercentage, modifier);
                    break;
                case ModifierType.PointsGain:
                    ApplyModifier(ref pointsGainPercentage, modifier);
                    break;
                case ModifierType.ShooterProjectileAmount:
                    ApplyModifier(ref shooterProjectileAmount, modifier);
                    break;
            }
        }
    }

    private static void ApplyModifier(ref float stat, ModifierData modifier)
    {
        switch (modifier.modifierOperation)
        {
            case ModifierOperation.Add:
                stat += modifier.value;
                break;
            case ModifierOperation.Subtract:
                stat -= modifier.value;
                break;
        }

        // Ensure the stat does not go below zero or exceed certain limits
        stat = Mathf.Clamp(stat, 0f, float.MaxValue);
    }
}
