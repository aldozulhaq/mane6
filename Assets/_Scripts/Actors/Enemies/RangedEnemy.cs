using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    ParticleSystem[] bulletParticleSystems;
    Bullet[] bullets;

    private void Start()
    {
        bulletParticleSystems = GetComponentsInChildren<ParticleSystem>();
        bullets = GetComponentsInChildren<Bullet>();
        SetBulletDamage(damage);
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        foreach (ParticleSystem bulletParticle in bulletParticleSystems)
        {
            bulletParticle.Play();
        }
    }

    public void SetBulletDamage(float damage)
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.SetDamage(damage);
        }
    }
}
