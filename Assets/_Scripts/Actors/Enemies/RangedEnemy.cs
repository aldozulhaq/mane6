using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] ParticleSystem bulletParticleSystem;
    Bullet[] bullets;

    private void Start()
    {
        bullets = GetComponentsInChildren<Bullet>();
        SetBulletDamage(damage);
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        bulletParticleSystem.Play();
    }
    public void SetBulletDamage(float damage)
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.SetDamage(damage);
        }
    }
}
