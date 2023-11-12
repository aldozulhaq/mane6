using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] FloatEventChannel playerHealthChannel;

    [Header("Runtime")]
    [SerializeField] float health;
    [SerializeField] float damage;


    [SerializeField] GameObject bulletParent;
    Bullet[] bullets;

    private void Start()
    {
        bullets = bulletParent.GetComponentsInChildren<Bullet>();
        health = PlayerStats.maxHealth;

        SetBulletDamage();
    }

    public void Heal(float amount)
    {
        if(health + amount > PlayerStats.maxHealth)
        {
            health = PlayerStats.maxHealth;
        }
        else
        {
            health += amount;
        }
        playerHealthChannel.Invoke(health);
    }

    public void SetBulletDamage(ModifierType type) // Call this everytime player got damage modifier
    {
        if (type != ModifierType.Damage) return;
        SetBulletDamage();
    }

    public void SetBulletDamage()           
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.SetDamage(damage * (1 + PlayerStats.damagePercentage / 100));
        }
    }

    public void TakeDamage(DamageData damageData)
    {
        if (damageData.target == this.gameObject)
        {
            damage = damage * (1 - PlayerStats.damageReducePercentage / 100); //reduce damage by damage reduction percentage
            health -= damage;

            CamShake.instance.ShakeCamera();

            playerHealthChannel.Invoke(health);
        }
    }

    public void LifeStealing(float damageOutput)
    {
        // Calculate lifesteal
        float healthGet = damageOutput * (PlayerStats.lifeStealPercentage / 100);
        Heal(healthGet);
    }
}
