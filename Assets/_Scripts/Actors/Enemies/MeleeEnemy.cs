using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Event Channel")]
    [SerializeField] DamageChannel damageChannel;

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        damageChannel.Invoke(new DamageData(player, damage));
        AudioManager.instance.PlaySFX("Hit");
    }
}
