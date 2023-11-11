using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ShooterFish : Shooter
{
    float fishShooterAmount = 0;

    protected override void Start()
    {
        fishShooterAmount = PlayerStats.shooterProjectileAmount;
        InitCurrentLevelToFish();
        AdjustFireRate();
    }

    void InitCurrentLevelToFish()
    {
        for (int i = 1; i <= fishShooterAmount; i++)
        {
            AddShooter(i);
        }
    }

    protected override void AddShooter()
    {
        AddShooter(shooterAmount);
    }

    protected void AddShooter(float shooterIndex)
    {
        var newShooter = Instantiate(baseShooter, transform.position, Quaternion.identity);
        newShooter.transform.parent = transform;
        newShooter.transform.localPosition = Vector3.zero.WithY(9);
        newShooter.transform.localScale = Vector3.one;
        newShooter.transform.localRotation = Quaternion.Euler(Vector3.zero.WithX(-90));

        if(shooterIndex == 1)
        {
            currentAngle = 0;
        }
        if (shooterIndex % 2 == 0)
        {
            currentAngle = Mathf.Abs(currentAngle) + baseAngleProgression;
        }
        else
        {
            currentAngle *= -1;
        }

        var newShooterFireRate = newShooter.GetComponent<ParticleSystem>().emission;
        var newShooterShape = newShooter.GetComponent<ParticleSystem>().shape;
        newShooterShape.rotation = Vector3.zero.WithY(currentAngle);
        newShooterFireRate.rateOverTime = (1 - baseShooterFireRate * (PlayerStats.fireRatePercentage / 100));
        Debug.Log("Fire rate: " + newShooterFireRate.rateOverTime.constant);
        shooterPS.Add(newShooter.GetComponent<ParticleSystem>());
    }
}
