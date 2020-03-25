using System.Collections;
using System.Collections.Generic;
using System.Text;
using Controllers.Player;
using Data.Models;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ResourceStorageController : MonoBehaviour
{
    private Inventory _inventory;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Store(ItemQuantity itemToStore)
    {
        _inventory.Add(itemToStore);
        var inventoryDisplay = GetComponentInChildren<TextMeshPro>();
        var builder = new StringBuilder();
        foreach (var item in _inventory.Items)
        {
            builder.AppendLine(item.ToString());
        }

        inventoryDisplay.text = builder.ToString();
    }
}
