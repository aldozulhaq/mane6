using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] public IUsable item;

    [Header("UI")]
    [SerializeField] UnityEngine.UI.Image itemImage;
    [SerializeField] TMPro.TextMeshProUGUI itemLevel;

    public void InitUI()
    {
        itemImage.sprite = item.GetSprite();
        itemLevel.text = "1";
    }

    public void SetLevel(int level)
    {
        itemLevel.text = level.ToString();
    }

    public int GetLevel()
    {
        return int.Parse(itemLevel.text);
    }
}
