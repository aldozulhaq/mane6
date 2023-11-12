using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] List<Modifier> modifierSellList;
    [SerializeField] List<Weapon> weaponSellList;

    [SerializeField] GameObject shopItemPrefab;

    //combine both list count
    public int shopListCount;

    private void Start()
    {
        shopListCount = modifierSellList.Count + weaponSellList.Count;
        for(int i = 0; i < 3; i++)
        {
            InstantiateShopItem();
        }
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
}
