using System.Collections;
using System.Collections.Generic;
using Controllers.Ui;
using Data.Models;
using UnityEngine;
using UnityEngine.UI;

public class AddResourceScript : MonoBehaviour
{
    public ItemId itemId;
    public int quantity;

    private ResourcesManagerOveraly _resourceManger;

    // Start is called before the first frame update
    void Start()
    {
        _resourceManger = GetComponentInParent<ResourcesManagerOveraly>();
        GetComponent<Button>().onClick.AddListener(AddResource);
    }

    private void AddResource()
    {
        _resourceManger.AddResource(new ItemQuantity(itemId, quantity));
    }
}
