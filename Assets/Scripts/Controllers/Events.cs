using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events current;

    private void Awake()
    {
        current = this;
    }

    public event Action<List<ItemQuantity>> OnInventoryUpdate;
    public event Action OnToggleCraftingOverlay;
    public event Action<ItemId> OnCraftThing;

    public void TriggerInventoryUpdate(List<ItemQuantity> obtainedItems) => OnInventoryUpdate?.Invoke(obtainedItems);
    public void TriggerToggleCraftingOverlay() => OnToggleCraftingOverlay?.Invoke();
    public void TriggerCraftThing(ItemId id) => OnCraftThing?.Invoke(id);
}
