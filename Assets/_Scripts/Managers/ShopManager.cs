using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] List<Modifier> modifierSellList;
    [SerializeField] List<Weapon> weaponSellList;

    [SerializeField] GameObject shopItemPrefab;

    [SerializeField] UnityEngine.UI.Button rerollButton;
    [SerializeField] TMPro.TextMeshProUGUI rerollPriceText;

    //combine both list count
    int shopListCount;

    List<Modifier> bannedModifierList;
    List<Weapon> bannedWeaponList;

    PointManager pointManager;

    float baseRerollPrice = 50;
    float rerollPrice;
    float rerollPriceIncrease = 2f;

    private void Awake()
    {
        pointManager = FindObjectOfType<PointManager>();
    }

    private void Start()
    {
        shopListCount = modifierSellList.Count + weaponSellList.Count;
        ClearShopItem();
    }

    //get random item from both list
    public IUsable GetRandomItem()
    {
        int random = Random.Range(0, shopListCount);

        if (random < modifierSellList.Count)
        {
            return modifierSellList[random];
        }
        else
        {
            return weaponSellList[random - modifierSellList.Count];
        }
    }

    //instantiating shop item
    public void InstantiateShopItem()
    {
        IUsable item = GetRandomItem();
        GameObject shopItemObj = Instantiate(shopItemPrefab, transform);
        
        ShopItem shopItem = shopItemObj.GetComponent<ShopItem>();

        shopItem.item = item;
        shopItem.InitUI();
    }

    //clear shop item
    public void ClearShopItem()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    //reroll shop item
    public void RerollShopItem()
    {
        if (pointManager.IsPointSufficient((int)rerollPrice))
        {
            pointManager.ReducePoint((int)rerollPrice);
            rerollPrice *= rerollPriceIncrease;
            rerollPriceText.text = rerollPrice.ToString() + "G";
            ClearShopItem();
            for (int i = 0; i < 3; i++)
            {
                InstantiateShopItem();
            }
        }
    }

    private void OnEnable()
    {
        ClearShopItem();
        for (int i = 0; i < 3; i++)
        {
            InstantiateShopItem();
        }

        rerollPrice = baseRerollPrice;
        rerollPriceText.text = rerollPrice.ToString() + "G";

        if (pointManager.IsPointSufficient((int)rerollPrice))
            rerollButton.interactable = true;
        else
            rerollButton.interactable = false;
    }

    //Ban item
    public void BanItem()
    {
        bannedWeaponList = WeaponManager.instance.maxLeveledWeapons.ToList();
        bannedWeaponList.ForEach(x =>
        {
            if (weaponSellList.Contains(x))
            {
                weaponSellList.Remove(x);
            }
        }
        );

        bannedModifierList = ItemModifierManager.instance.modifiers.Where(x => ItemModifierManager.instance.GetModifierLevel(x) == 3).ToList();
        bannedModifierList .ForEach(x =>
        {
            if (modifierSellList.Contains(x))
            {
                modifierSellList.Remove(x);
            }
        }
        );
    }

    //Unban item
    public void UnbanItem()
    {
        bannedWeaponList.ForEach(x =>
        {
            if (!weaponSellList.Contains(x))
            {
                weaponSellList.Add(x);
            }
        }
               );

        bannedModifierList.ForEach(x =>
        {
            if (!modifierSellList.Contains(x))
            {
                modifierSellList.Add(x);
            }
        }
               );
    }

}
