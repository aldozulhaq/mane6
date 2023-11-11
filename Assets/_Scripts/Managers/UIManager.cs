using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text textTimer;
    [SerializeField] Image healthBar;

    Material healthBarMat;

    float currentHealthBackProgress;
    float currentHealthFrontProgress;

    private void Start()
    {
        healthBarMat = healthBar.material;
        currentHealthBackProgress = healthBarMat.GetFloat("_BackProgress");
        currentHealthFrontProgress = healthBarMat.GetFloat("_FrontProgress");
    }

    public void UpdateTime(float currentTime)
    {
        textTimer.text = currentTime.ToString("F0");
    }

    public void UpdateHealthBar(float currentHealth)
    {
        //healthBar.fillAmount = currentHealth / PlayerStats.maxHealth;

        //check if healing or taking damage
        if (currentHealth > currentHealthBackProgress * PlayerStats.maxHealth)
        {
            StartCoroutine(IncreaseHealth(currentHealth));
        }
        else
        {
            StartCoroutine(DecreaseHealth(currentHealth));
        }
    }

    private string IncreaseHealth(float currentHealth)
    {
        throw new NotImplementedException();
    }

    private string DecreaseHealth(float currentHealth)
    {
        throw new NotImplementedException();
    }
}
