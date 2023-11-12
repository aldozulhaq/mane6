using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text textTimer;
    [SerializeField] Image healthBar;
    [SerializeField] Text killText;
    [SerializeField] Text waveText;
    [SerializeField] Text pointText;

    Material healthBarMat;

    float currentHealthBackProgress;
    float currentHealthFrontProgress;
    float healthTempDebug = 100;

    private void Start()
    {
        healthBarMat = healthBar.material;

        InitHealthBarToFull();

        currentHealthBackProgress = healthBarMat.GetFloat("_BackProgress");
        currentHealthFrontProgress = healthBarMat.GetFloat("_FrontProgress");
    }

    public void UpdateTime(float currentTime)
    {
        textTimer.text = currentTime.ToString("F0");
    }

    public void UpdateHealthBar(float currentHealth)
    {        
        DOVirtual.Float(currentHealthFrontProgress * PlayerStats.maxHealth, currentHealth, 1f, AnimateFrontHealth);
        DOVirtual.Float(currentHealthFrontProgress * PlayerStats.maxHealth, currentHealth, 1f, AnimateBackHealth).SetDelay(1f);
    }

    public void UpdateKillCount(float count)
    {
        killText.text = "x " + count.ToString();
    }

    public void UpdatePoint(float count)
    {
        pointText.text = "x " + count.ToString();
    }

    public void UpdateWaveText(int waveCount)
    {
        int currentLevel = waveCount / 10;
        if (waveCount % 10 != 0)
            currentLevel++;
        int currentWave = waveCount - (currentLevel - 1) * 10;

        waveText.text = currentLevel.ToString() + " - " + currentWave.ToString();
    }
    
    void AnimateFrontHealth(float currentHealth)
    {
        healthBarMat.SetFloat("_FrontProgress", currentHealth / PlayerStats.maxHealth);
        currentHealthFrontProgress = currentHealth / PlayerStats.maxHealth;
    }

    void AnimateBackHealth(float currentHealth)
    {
        healthBarMat.SetFloat("_BackProgress", currentHealth / PlayerStats.maxHealth);
        currentHealthBackProgress = currentHealth / PlayerStats.maxHealth;
    }

    void InitHealthBarToFull()
    {
        healthBarMat.SetFloat("_BackProgress", 1);
        healthBarMat.SetFloat("_FrontProgress", 1);
    }
}
