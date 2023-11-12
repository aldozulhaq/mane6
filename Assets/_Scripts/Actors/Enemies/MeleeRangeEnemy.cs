using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRangeEnemy : Enemy
{
    [SerializeField] float fireArea;
    [SerializeField] float ceaseFireArea;       // Enemy will walk within this area and no shooting

    Bullet[] bullets;
    ParticleSystem[] bulletParticleSystems;
    bool canHit = true;

    protected override void OnEnable()
    {
        canHit = true;
        base.OnEnable();
    }

    private void Start()
    {
        canHit = true;
        bulletParticleSystems = GetComponentsInChildren<ParticleSystem>();
        bullets = GetComponentsInChildren<Bullet>();
        SetBulletDamage(damage);
    }

    protected override IEnumerator MoveToPlayer()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            
            if (canHit)
                AttackPlayer();

            yield return null;
        }
    }

    protected override void AttackPlayer()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) <= attackRadius)
        {
            StartCoroutine(AttackByHit());
        }

        if (Vector3.Distance(this.transform.position, player.transform.position) >= ceaseFireArea && Vector3.Distance(this.transform.position, player.transform.position) <= fireArea)
        {
            StartCoroutine(AttackByBullet());
        }
    }

    protected IEnumerator AttackByHit()
    {
        canHit = false;

        player.TakeDamage(new DamageData(player, damage));
        AudioManager.instance.PlaySFX("Hit");
        yield return new WaitForSeconds(hitCooldown);

        canHit = true;
    }

    protected IEnumerator AttackByBullet()
    {
        canHit = false;

        Debug.Log("Attack w bullet");
        foreach (ParticleSystem bulletParticle in bulletParticleSystems)
        {
            bulletParticle.Play();
            AudioManager.instance.PlaySFX("Bullet");
        }
        yield return new WaitForSeconds(hitCooldown);

        canHit = true;
    }
    public void SetBulletDamage(float damage)
    {
        foreach (Bullet bullet in bullets)
        {
            bullet.SetDamage(damage);
        }
    }
}
