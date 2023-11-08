using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnParticleCollision(GameObject other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Debug.Log(enemy);
        if (enemy)
        {
            enemy.TakeDamage(damage);
        }
    }
}
