using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        player.TakeDamage(damage);
    }
}
