using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected string nameTag;        // for object pool dictionary
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRadius;

    protected Player player;
    private bool canHit;
    private float hitCooldown = 1f;

    private Action<Enemy> deathAction;

    protected void OnEnable()
    {
        canHit = true;
        player = FindObjectOfType<Player>();
        StartCoroutine(MoveToPlayer());

        GameplayEvents.OnWaveEndE += Death;
        GameplayEvents.OnBulletHitE += OnHitBullet;
    }

    protected void OnDisable()
    {
        GameplayEvents.OnWaveEndE -= Death;
        GameplayEvents.OnBulletHitE -= OnHitBullet;
    }

    private void Update()
    {
        transform.LookAt(player.transform);
    }

    protected IEnumerator MoveToPlayer()
    {
        while (true)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) <= attackRadius)
            {
                AttackPlayer();
                yield return new WaitForSeconds(hitCooldown);
                continue;
            }

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    protected virtual void AttackPlayer()
    {
        Debug.Log("Attacking Player");
    }

    private IEnumerator OnPlayerHit()
    {
        player.TakeDamage(damage);
        canHit = false;

        yield return new WaitForSeconds(hitCooldown);

        canHit = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Death();
        }
    }

    public string GetNameTag()
    {
        return nameTag;
    }

    public void Init(Action<Enemy> _deathAction)
    {
        deathAction = _deathAction;
    }

    [ContextMenu("Kill")]
    public virtual void Death()
    {
        deathAction(this);
    }

    private void OnHitBullet(GameObject target, float damage)
    {
        if (target == this.gameObject)
        {
            // Calculate crit
            if (UnityEngine.Random.value < PlayerStats.critDamagePercentage)
            {
                // calculate crit damage
                damage *= PlayerStats.critDamagePercentage;
            }

            TakeDamage(damage);
        }

        GameplayEvents.OnEnemyHit(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition, attackRadius);
    }
}
