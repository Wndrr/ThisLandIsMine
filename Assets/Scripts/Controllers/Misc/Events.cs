using System;
using System.Collections.Generic;
using Data.Models;
using UnityEngine;

namespace Controllers.Misc
{
    public class Events : MonoBehaviour
    {
        public static Events Current;

        private void Awake()
        {
            Current = this;
        }

        public event Action<List<ItemQuantity>> OnInventoryUpdate;
        public event Action OnToggleCraftingOverlay;
        public event Action OnToggleResourceManagerOverlay;
        public event Action<ItemQuantity> OnResourceStorageUpdated;
        public event Action OnEsc;
        public event Action<ItemId> OnCraftThing;

        public void TriggerInventoryUpdate(List<ItemQuantity> obtainedItems) => OnInventoryUpdate?.Invoke(obtainedItems);
        public void TriggerToggleCraftingOverlay() => OnToggleCraftingOverlay?.Invoke();
        public void TriggerToggleResourceManagerOverlay() => OnToggleResourceManagerOverlay?.Invoke();
        public void TriggerResourceStorageUpdated(ItemQuantity item) => OnResourceStorageUpdated?.Invoke(item);
        public void TriggerEsc() => OnEsc?.Invoke();
        public void TriggerCraftThing(ItemId id) => OnCraftThing?.Invoke(id);
    }
}
