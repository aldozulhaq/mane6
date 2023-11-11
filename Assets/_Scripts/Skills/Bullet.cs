using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected DamageChannel damageChannel;
    protected float damage;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    protected virtual void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit " + other + " for " + damage);
        damageChannel.Invoke(new DamageData(other,damage));
    }
}
