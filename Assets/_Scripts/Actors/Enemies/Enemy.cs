using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected string nameTag;        // for object pool dictionary
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected float hitCooldown = 1f;

    protected Player player;

    private Action<Enemy> deathAction;

    [SerializeField] protected EventChannel OnEnemyDead;

    protected virtual void OnEnable()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(MoveToPlayer());
    }

    private void Update()
    {
        transform.LookAt(player.transform);
        transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, 0f);
    }

    protected virtual IEnumerator MoveToPlayer()
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
        OnEnemyDead.Invoke();
        deathAction(this);
    }

    public void OnHitBullet(DamageData damageData)
    {
        if (damageData.target == this.gameObject)
            TakeDamage(damageData.damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition, attackRadius);
    }
}
