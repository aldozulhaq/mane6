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

    PointManager pointManager;

    private void Awake()
    {
        pointManager = FindObjectOfType<PointManager>();
    }

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
            level = (ItemModifierManager.instance.GetModifierLevel(item as Modifier));
        }
        else if(item is Weapon)
        {
            level = (WeaponManager.instance.GetWeaponLevel(item as Weapon));
        }

        return level + 1;
    }

    public void Buy()
    {
        if (pointManager.IsPointSufficient(price))
        {
            pointManager.ReducePoint(price);
            item.Use();
            InventoryManager.instance.AddItem(item);
            FindObjectOfType<UIManager>().UpdatePoint(pointManager.GetPoint());
            buyButton.interactable = false;
        }
    }
}
