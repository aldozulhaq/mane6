using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyback : MonoBehaviour
{
    //TMP text price
    [SerializeField] TMPro.TextMeshProUGUI priceText;
    [SerializeField] private int price = 100;

    PointManager pointManager;
    UIManager uiManager;
    GameManager gameManager;
    Player player;

    private void Awake()
    {
        pointManager = FindObjectOfType<PointManager>();
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        priceText.text = price.ToString();
    }

    public void DoBuyback()
    {
        if (!pointManager.IsPointSufficient(price))
        {
            pointManager.ReducePoint(price);
            uiManager.UpdatePoint(pointManager.GetPoint());
            gameManager.StartWave();
            player.Heal(PlayerStats.maxHealth / 2);
            price *= 2;
            gameObject.SetActive(false);
        }

    }
}
