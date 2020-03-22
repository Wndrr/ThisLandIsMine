using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CraftingOverlay : MonoBehaviour
{

    private Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        Events.current.OnToggleCraftingOverlay += ToggleCraftingOverlay;
        GetComponentInChildren<Button>().onClick.AddListener(() =>  Events.current.TriggerCraftThing(ItemId.ThrowableStick));
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
        Events.current.OnToggleCraftingOverlay -= ToggleCraftingOverlay;
    }
}