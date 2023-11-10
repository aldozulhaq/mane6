using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] FloatEventChannel playerHealthChannel;

    [Header("Runtime")]
    [SerializeField] float health;

    [SerializeField] float IframeDuration = 0.5f;

    [Header("Power Ups")]
    [SerializeField] List<PowerUp> powerUps = new List<PowerUp>();

    [Header("Modifiers")]
    [SerializeField] List<Modifier> modifiers = new List<Modifier>();

    private void Start()
    {
        health = PlayerStats.maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage = damage * (1 - PlayerStats.damageReducePercentage / 100); //reduce damage by damage reduction percentage
        health -= damage;
    }

    public void Heal(float amount)
    {
        Mathf.Clamp(health += amount, 0, PlayerStats.maxHealth); //clamp health between 0 and max health
    }
}
