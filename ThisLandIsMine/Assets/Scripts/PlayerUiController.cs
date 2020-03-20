using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiController : MonoBehaviour
{
    private Text ResourcesText;
    private Text InventoryText;

    public void Start()
    {
        ResourcesText = gameObject.transform.Find("ResourceCount").GetComponent<Text>();
        InventoryText = gameObject.transform.Find("Inventory").GetComponent<Text>();
    }

    public void UpdateInventory(List<Item> inventory, List<Item> craftableItems, Dictionary<ResourceType, int> ressources)
    {
        var inventoryAndCrafts = new List<Item>(craftableItems);
        inventoryAndCrafts.ForEach(c => { c.Count = inventory.SingleOrDefault(s => s.Id == c.Id)?.Count ?? 0; });
        
        var stringBuilder = new StringBuilder();
        foreach (var item in inventoryAndCrafts)
        {
            stringBuilder.AppendLine($"{item.Name} - {item.Count} - {Item.GetMaxNumberOfCraftsPossible(ressources)}");
        }

        InventoryText.text = stringBuilder.ToString();
    }

    public void UpdateResources(Dictionary<ResourceType, int> resources)
    {
        ResourcesText.text = GetResourcesText(resources);
    }

    public string GetResourcesText(Dictionary<ResourceType, int> resources)
    {
        var resourceTypes = Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>();
        var stringBuilder = new StringBuilder();
        foreach (ResourceType resourceType in resourceTypes)
        {
            var hasResource = resources.TryGetValue(resourceType, out var resourceCount);
            if (hasResource)
                stringBuilder.AppendLine($"{resourceType.ToString()}: {resourceCount}");
        }

        return stringBuilder.ToString();
    }
}


public class WoodenThrowingKnife : Item
{
    public WoodenThrowingKnife(int count = 0)
    {
        Id = 1;
        Name = "Throwing knife";
        ResourcesNeededToCraft = new Dictionary<ResourceType, int>()
        {
            {ResourceType.Wood, 1}
        };
        Count = count;
    }
}

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public static Dictionary<ResourceType, int> ResourcesNeededToCraft { get; set; }
    public int Damage { get; set; } = 1;
    public int Count { get; set; }

    public static int GetMaxNumberOfCraftsPossible(Dictionary<ResourceType, int> availableResources)
    {
        var maxCraftPerResource = new List<int>();
        foreach (var resource in ResourcesNeededToCraft)
        {
            // If at least one of the needed resources is not available, we know 0 items can be crafted
            if (!availableResources.ContainsKey(resource.Key))
                return 0;

            maxCraftPerResource.Add(availableResources[resource.Key] / resource.Value);
        }

        var lowestPossibleCraftCount = maxCraftPerResource.Min();
        return lowestPossibleCraftCount;
    }
    
    public static int Craft(Dictionary<ResourceType, int> resources)
    {
        foreach (var requirement in ResourcesNeededToCraft)
        {
            if(!resources.ContainsKey(requirement.Key))
                throw new InvalidOperationException("Cant craft");

            if(resources[requirement.Key] < requirement.Value)
                throw new InvalidOperationException("Cant craft");
            
            resources[requirement.Key] -= requirement.Value;
        }
        
        return 1;
    }
}