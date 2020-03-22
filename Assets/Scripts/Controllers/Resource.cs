using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int resourceCount;
    public int harvestSpeed;
    public ResourceEntityType type;

    public ItemQuantity Harvest()
    {
        resourceCount -= harvestSpeed;

        if (resourceCount <= 0)
        {
            Destroy(gameObject);
        }
        
        
        return new ItemQuantity(ItemId.Branch, harvestSpeed);
    }
}

public class ItemQuantity
{
    public ItemQuantity(ItemId id, int quantity)
    {
        Id = id;
        Quantity = quantity;
    }

    public ItemId Id { get; set; }
    public int Quantity { get; set; }

    public override string ToString()
    {
        return $"{Id.ToString()} - {Quantity}";
    }
}

public enum ResourceEntityType
{
    Bush
}
