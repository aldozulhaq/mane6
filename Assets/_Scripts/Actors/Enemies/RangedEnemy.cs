using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    Bullet[] bullets;

    private void Start()
    {
        bullets = GetComponentsInChildren<Bullet>();
        SetBulletDamage(damage);
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        ParticleSystem[] bulletParticleSystems;
        bulletParticleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem bulletParticle in bulletParticleSystems)
        {
            bulletParticle.Play();
            AudioManager.instance.PlaySFX("Bullet");
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
