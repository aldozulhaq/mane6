using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Shooter : MonoBehaviour
{
    [SerializeField] protected GameObject baseShooter;
    [SerializeField] protected float baseShooterFireRate;

    protected List<ParticleSystem> shooterPS = new List<ParticleSystem>();
    protected int shooterAmount = 1;
    protected int maxShooter = 5;

    readonly protected int baseAngleProgression = 15;
    protected int currentAngle = 0;
    
    protected virtual void Start()
    {
        baseShooterFireRate = baseShooter.GetComponent<ParticleSystem>().emission.rateOverTime.constant;
        shooterPS.Add(GetComponentInChildren<ParticleSystem>());
        AdjustFireRate();
    }

    public void AdjustFireRate()
    {
        foreach (ParticleSystem ps in shooterPS)
        {
            var emission = ps.emission;
            emission.rateOverTime = (1 - baseShooterFireRate * (PlayerStats.fireRatePercentage / 100));
        }
    }

    public void LevelUp()
    {
        shooterAmount++;
        if(shooterAmount > maxShooter) return;
        StopAllShooters(); // Stop all shooters before adding new one
        AddShooter();
        PlayAllShooters(); // Play all shooters after adding new one, ensuring that all shooters are playing at the same time
    }

    protected virtual void AddShooter()
    {

        var newShooter = Instantiate(baseShooter, transform.position, Quaternion.identity);
        newShooter.transform.parent = transform;
        newShooter.transform.localPosition = Vector3.zero.WithZ(-0.56f);

        if(shooterAmount %2 == 0)
        {
            currentAngle = Mathf.Abs(currentAngle) + baseAngleProgression;
        }
        else
        {
            currentAngle *= -1;
        }
        
        newShooter.transform.localRotation = Quaternion.Euler(Vector3.zero.WithY(currentAngle));
        var newShooterFireRate = newShooter.GetComponent<ParticleSystem>().emission;
        newShooterFireRate.rateOverTime = (1 - baseShooterFireRate * (PlayerStats.fireRatePercentage / 100));
        shooterPS.Add(newShooter.GetComponent<ParticleSystem>());
    }

    protected void StopAllShooters()
    {
        foreach (ParticleSystem ps in shooterPS)
        {
            ps.Clear();
        }
    }

    protected void PlayAllShooters()
    {
        foreach (ParticleSystem ps in shooterPS)
        {
            ps.Play();
        }
    }
}
