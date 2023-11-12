using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] public IUsable item;

    [Header("UI")]
    [SerializeField] TMPro.TextMeshProUGUI itemName;
    [SerializeField] TMPro.TextMeshProUGUI itemDesc;
    [SerializeField] UnityEngine.UI.Image itemImage;
    [SerializeField] TMPro.TextMeshProUGUI itemPrice;
    [SerializeField] UnityEngine.UI.Button buyButton;

    public int price = 50;

    Modifier modifier;
    Weapon weapon;
    public void InitUI()
    {
        int level = DetermineLevelToSell();

        itemName.text = item.GetName() + " " + level;
        itemDesc.text = item.GetDesc();
        itemImage.sprite = item.GetSprite();
        
        price = price * level;
        itemPrice.text = price.ToString() + "G";
    }

    int DetermineLevelToSell()
    {
        int level = 0;
        if(item is Modifier)
        {
            level = (ItemModifierManager.instance.GetModifierLevel(item as Modifier) + 1);
        }
        else if(item is Weapon)
        {
            level = (WeaponManager.instance.GetWeaponLevel(item as Weapon) + 1);
        }

        return level + 1;
    }

    public void Buy()
    {
        item.Use();
        buyButton.interactable = false;
    }
}
