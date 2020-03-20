using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    private Dictionary<ResourceType, int> Ressources { get; set; } = new Dictionary<ResourceType, int>();
    public PlayerUiController Ui { get; set; }
    private List<Item> Inventory = new List<Item>();

    private List<Item> CraftableItems = new List<Item>()
    {
        new WoodenThrowingKnife()
    };

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CraftItem();
            Ui.UpdateResources(Ressources);
            Ui.UpdateInventory(Inventory, CraftableItems, Ressources);
        }
    }

    public void CraftItem()
    {
        if (WoodenThrowingKnife.GetMaxNumberOfCraftsPossible(Ressources) > 0)
        {
            var createdCount = Item.Craft(Ressources);
            var item = Inventory.SingleOrDefault(i => i.Id == 1);
            if (item == null)
            {
                item = new WoodenThrowingKnife(createdCount);
                Inventory.Add(item);
            }
            else
            {
                item.Count += createdCount;
            }
        }
    }

    public int AddToResource(ResourceValue resourceValue)
    {
        return AddToResource(resourceValue.Type, resourceValue.Value);
    }

    public int AddToResource(ResourceType type, int numberToAdd)
    {
        if (Ressources.ContainsKey(type))
        {
            Ressources[type] += numberToAdd;
        }
        else
        {
            Ressources.Add(type, numberToAdd);
        }

        Ui.UpdateResources(Ressources);
        Ui.UpdateInventory(Inventory, CraftableItems, Ressources);
        return Ressources[type];
    }

    public bool HasItem()
    {
        var item = Inventory.SingleOrDefault(i => i.Id == 1);
        if(item == null)
            return false;
        return item.Count > 0;
    }

    public void RemoveItem()
    {
        var item = Inventory.SingleOrDefault(i => i.Id == 1);
        if(item == null)
            return;

        item.Count -= 1;
        Ui.UpdateInventory(Inventory, CraftableItems, Ressources);
    }
}