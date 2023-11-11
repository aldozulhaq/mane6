using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit " + other + " for " + damage);
        GameplayEvents.OnBulletHit(other, damage);
    }
}
