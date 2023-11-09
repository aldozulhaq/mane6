using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Base Stat")]
    [SerializeField] float maxHealth;
    [SerializeField] float moveSpeedPercentage = 100f;
    [SerializeField] float damagePercentage = 100f;
    [SerializeField] float attackSpeedPercentage = 100f;
    [SerializeField] int fireRate = 1;
    [SerializeField] float critChancePercentage = 0f;
    [SerializeField] float critDamagePercentage = 100f;
    [SerializeField] float lifeStealPercentage = 0f;
    [SerializeField] float damageReducePercentage = 0f;


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
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void ApplyModifiers()
    {
        
    }
}
