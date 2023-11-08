using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
