using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField] protected FloatEventChannel onEnemyHit;

    protected override void OnParticleCollision(GameObject other)
    {
        float finalDamage = damage;
        //calculate crit chance
        float critChance = Random.Range(0, 100);
        float critDamage = finalDamage * (PlayerStats.critDamagePercentage / 100);
        
        if (critChance <= PlayerStats.critChancePercentage)
        {
            finalDamage += critDamage;
        }

        damageChannel.Invoke(new DamageData(other, finalDamage));
        onEnemyHit.Invoke(finalDamage);
    }
}
