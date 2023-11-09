using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] ParticleSystem bullet;

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        bullet.Play();
    }
}
