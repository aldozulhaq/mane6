using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject baseShooter;
    [SerializeField] float baseShooterFireRate;

    List<ParticleSystem> shooterPS = new List<ParticleSystem>();
    public int level { get; set; }

    void Start()
    {
        shooterPS.Add(baseShooter.GetComponent<ParticleSystem>());
    }


    public void LevelUp()
    {
        level++;
        
    }

    void AddShooter()
    {
        var newShooter = Instantiate(baseShooter, transform.position, Quaternion.identity);
        newShooter.transform.parent = transform;
        shooterPS.Add(newShooter.GetComponent<ParticleSystem>());
    }
}
