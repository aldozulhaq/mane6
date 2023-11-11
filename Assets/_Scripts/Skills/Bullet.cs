using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] DamageChannel damageChannel;
    float damage;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    private void OnParticleCollision(GameObject other)
    {
        damageChannel.Invoke(new DamageData { target = other, damage = damage });
    }
}
