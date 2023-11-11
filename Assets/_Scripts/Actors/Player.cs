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

    [SerializeField] float IframeDuration = 0.5f;

    [SerializeField] GameObject bulletParent;
    Bullet[] bullets;

    private void OnEnable()
    {
        GameplayEvents.OnBulletHitE += OnHitBullet;
    }
    private void OnDisable()
    {
        GameplayEvents.OnBulletHitE -= OnHitBullet;
    }

    private void Start()
    {
        bullets = bulletParent.GetComponentsInChildren<Bullet>();
        health = PlayerStats.maxHealth;

        SetBulletDamage();
    }

    public void TakeDamage(float damage)
    {
        damage = damage * (1 - PlayerStats.damageReducePercentage / 100); //reduce damage by damage reduction percentage
        health -= damage;
    }

    public void Heal(float amount)
    {
        Mathf.Clamp(health += amount, 0, PlayerStats.maxHealth); //clamp health between 0 and max health
    }

    public void SetBulletDamage()           // Call this everytime player got attack modifier
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.SetDamage(damage);
        }
    }

    private void OnHitBullet(GameObject target, float damage)
    {
        if (target == this.gameObject)
            TakeDamage(damage);
    }
}
