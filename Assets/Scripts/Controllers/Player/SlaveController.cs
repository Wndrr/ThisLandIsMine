using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Controllers.Misc;
using Controllers.Player;
using Controllers.Ui;
using Data.Models;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Inventory))]
public class SlaveController : MonoBehaviour
{
    private CharacterController controller;
    private Inventory _inventory;
    public float turnSpeed = 10;

    public float speed = 1;
    private bool _isCurrentTargetStorage;

    private string StatusText = string.Empty;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetObject = GetTargetObject();
        var direction = GetDirection(targetObject);
        if (direction.magnitude > 3)
        {
            GoToTarget(direction);
        }
        else
        {
            if (_isCurrentTargetStorage)
                StoreIn(targetObject);
            else
                Harvest(targetObject);
        }

        UpdateText();
    }

    private void StoreIn(GameObject targetObject)
    {
        var resourceScript = targetObject.GetComponent<ResourceStorageController>();
        if (resourceScript == null)
            return;

        for (var index = 0; index < _inventory.Items.Count; index++)
        {
            var itemQuantity = _inventory.Items[index];
            resourceScript.Store(itemQuantity);
            _inventory.Items.Remove(itemQuantity);
        }
    }

    private void GoToTarget(Vector3 direction)
    {
        var speedMultiplyer = 2f;
        controller.SimpleMove(direction.normalized * (speed * speedMultiplyer));
    }

    private Vector3 GetDirection(GameObject targetObject)
    {
        var direction = targetObject.transform.position - transform.position;
        Debug.DrawLine(transform.position, targetObject.transform.position, Color.blue);
        return direction;
    }

    private void Harvest(GameObject targetObject)
    {
        var resourceScript = targetObject.GetComponent<Resource>();
        if (resourceScript == null)
            return;

        var harvested = resourceScript.Harvest();
        _inventory.Add(harvested);
    }

    private void UpdateText()
    {
        var inventoryDisplay = GetComponentInChildren<TextMeshPro>();
        var builder = new StringBuilder();
        foreach (var item in _inventory.Items)
        {
            builder.AppendLine(item.ToString());
        }

        builder.AppendLine(StatusText);
        inventoryDisplay.text = builder.ToString();
    }

    private GameObject GetTargetObject()
    {
        if (_inventory.Items.Sum(i => i.Quantity) > 15)
        {
            _isCurrentTargetStorage = true;
            StatusText = "Storage";
            return LocateStorageObject();
        }

        _isCurrentTargetStorage = false;
        StatusText = "Harvest";
        var resources = GameObject.FindGameObjectsWithTag("Resource");

        var min = resources.OrderBy(resource => (resource.transform.position - transform.position).magnitude).FirstOrDefault();
        return min;
    }

    private GameObject LocateStorageObject()
    {
        var resources = GameObject.FindGameObjectsWithTag("Construction").Where(r => r.name == "ResourceStorage");

        var min = resources.OrderBy(resource => (resource.transform.position - transform.position).magnitude).FirstOrDefault();
        return min;
    }

    private ResourceEntityType GetTargetResource()
    {
        var requests = ResourcesManagerOveraly.MissingResourceQuantities;
        var targetItemId = requests.Select(r => r.Id).FirstOrDefault();

        switch (targetItemId)
        {
            case ItemId.Branch:
                return ResourceEntityType.Tree;
            case ItemId.Berry:
                return ResourceEntityType.Bush;
            case ItemId.Stone:
                return ResourceEntityType.Rock;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}