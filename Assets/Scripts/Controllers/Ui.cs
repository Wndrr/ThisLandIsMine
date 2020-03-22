using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{

    private Text _inventoryText;

    // Start is called before the first frame update
    void Start()
    {
        Events.current.OnInventoryUpdate += OnInventoryUpdate;
        _inventoryText = GetComponentInChildren<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInventoryUpdate(List<ItemQuantity> items)
    {
        var builder = new StringBuilder();
        items.ForEach(i => builder.AppendLine(i.ToString()));

        _inventoryText.text = builder.ToString();
    }

    public void OnDestroy()
    {
        Events.current.OnInventoryUpdate -= OnInventoryUpdate;
    }
}
