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

        _ui.UpdateInventory(Items);
    }
}
