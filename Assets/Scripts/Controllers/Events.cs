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

    public void TriggerInventoryUpdate(List<ItemQuantity> obtainedItems)
    {
        if (OnInventoryUpdate != null)
        {
            OnInventoryUpdate(obtainedItems);
        }
    }
}
