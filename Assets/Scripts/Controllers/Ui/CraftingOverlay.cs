﻿using Controllers.Misc;
using Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Ui
{
    public class CraftingOverlay : MonoBehaviour
    {

        private Canvas _canvas;
        // Start is called before the first frame update
        private void Start()
        {
            Events.Current.OnToggleCraftingOverlay += ToggleCraftingOverlay;
            GetComponentInChildren<Button>().onClick.AddListener(() =>  Events.Current.TriggerCraftThing(ItemId.ThrowableStick));
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        private void ToggleCraftingOverlay()
        {
            Cursor.visible = !Cursor.visible;
            _canvas.enabled = !_canvas.enabled;

            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }

        private void OnDestroy()
        {
            Events.Current.OnToggleCraftingOverlay -= ToggleCraftingOverlay;
        }
    }
}