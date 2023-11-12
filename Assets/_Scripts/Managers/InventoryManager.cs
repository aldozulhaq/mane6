using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<InventoryItem> items;

    [SerializeField] GameObject itemUIPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ClearInventory();
    }

    public void AddItem(IUsable itemToAdd)
    {
        if (!items.Any(inventory => inventory.item == itemToAdd))
        {
            var a = Instantiate(itemUIPrefab, transform);
            a.GetComponent<InventoryItem>().item = itemToAdd;
            a.GetComponent<InventoryItem>().InitUI();
            items.Add(a.GetComponent<InventoryItem>());
        }
        else
        {
            UpdateItemLevel(itemToAdd);
        }
    }


    public void UpdateItemLevel(IUsable item)
    {
        InventoryItem inventoryItem = items.Find(x => x.item == item);
        inventoryItem.SetLevel(inventoryItem.GetLevel() + 1);
    }

    public void ClearInventory()
    {
        items.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
