﻿using System;
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

    public void Remove(params ItemQuantity[] removedItems)
    {
        foreach (var item in removedItems)
        {
            Remove(item);
        }
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

    private bool CanRecipeBeCrafted(IEnumerable<ItemQuantity> recipe)
    {
        return recipe.All(HasEnoughOfThisItem);
    }

    private bool HasEnoughOfThisItem(ItemQuantity searchedItem)
    {
        var alreadyStoredItemQuantity = Items.SingleOrDefault(i => i.Id == searchedItem.Id);
        if (alreadyStoredItemQuantity == null) return false;

        return alreadyStoredItemQuantity.Quantity >= searchedItem.Quantity;
    }

    private void CraftThing(ItemId id)
    {
        var itemToCraft = ItemsDatabase.Items.SingleOrDefault(s => s.Id == id);

        if (itemToCraft == null)
            throw new InvalidOperationException($"Item with id {id} does not exist");

        if (!itemToCraft.IsCraftable)
            throw new InvalidOperationException($"Item {itemToCraft} cannot be crafted");

        if (CanRecipeBeCrafted(itemToCraft.Recipe))
        {
            Remove(itemToCraft.Recipe.ToArray());
            Add(new ItemQuantity(ItemId.ThrowableStick, 1));

            Events.current.TriggerInventoryUpdate(Items);
        }
    }
}