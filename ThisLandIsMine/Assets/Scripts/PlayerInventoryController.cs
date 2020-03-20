using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{    
    private Dictionary<ResourceType, int> Ressources { get; set; } = new Dictionary<ResourceType, int>();
    public PlayerUiController Ui { get; set; }

    public int AddToResource(ResourceValue resourceValue)
    {
        return AddToResource(resourceValue.Type, resourceValue.Value);
    }
    
    public int AddToResource(ResourceType type, int numberToAdd)
    {
        if (Ressources.ContainsKey(type))
        {
            Ressources[type] += numberToAdd;
        }
        else
        {
            Ressources.Add(type, numberToAdd);
        }

        Ui.UpdateResources(Ressources);
        return Ressources[type];
    }
}
