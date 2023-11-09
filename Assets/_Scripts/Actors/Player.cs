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
        health -= damage;
    }

    void ApplyModifiers()
    {
        
    }
}
