using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Ui))]
public class Inventory : MonoBehaviour
{
    private Ui _ui;

    private List<ItemQuantity> Items { get; set; } = new List<ItemQuantity>();

    private void Start()
    {
        _ui = GetComponent<Ui>();
        Events.current.OnCraftThing += CraftThing;
    }

    public void Add(ItemQuantity obtainedItems)
    {
        var alreadyStoredItemQuantity = Items.SingleOrDefault(i => i.Id == obtainedItems.Id);
        if (alreadyStoredItemQuantity != null)
        {
            obtainedItems.Quantity += alreadyStoredItemQuantity.Quantity;
            Items.Remove(alreadyStoredItemQuantity);
            Items.Add(obtainedItems);
        }
        else
        {
            Items.Add(obtainedItems);
        }

        Events.current.TriggerInventoryUpdate(Items);
    }

    public void Remove(ItemQuantity removedItems)
    {
        var alreadyStoredItemQuantity = Items.SingleOrDefault(i => i.Id == removedItems.Id);
        if (alreadyStoredItemQuantity == null) return;
        
        if (alreadyStoredItemQuantity.Quantity < removedItems.Quantity)
        {
            Items.Remove(alreadyStoredItemQuantity);
        }
        else
        {
            alreadyStoredItemQuantity.Quantity -= removedItems.Quantity;
            Items.Remove(alreadyStoredItemQuantity);
            Items.Add(alreadyStoredItemQuantity);
        }
    }

    private void CraftThing()
    {
        var requiredResource = new ItemQuantity(ItemId.Branch, 1);
        Remove(requiredResource);
        Add(new ItemQuantity(ItemId.Thing, 1));
        
        Events.current.TriggerInventoryUpdate(Items);
    }
}
