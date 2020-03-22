using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingOverlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Events.current.OnToggleCraftingOverlay += ToggleCraftingOverlay;
    }

    private void ToggleCraftingOverlay()
    {
        Cursor.visible = !Cursor.visible;

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