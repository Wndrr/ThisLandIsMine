using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public static List<Item> Items;
    // Start is called before the first frame update
    void Awake()
    {
        SeedDatabase();
        DontDestroyOnLoad(this);
    }

    private void SeedDatabase()
    {
        Items = new List<Item>
        {
            new Item(ItemId.Branch, "Branch"),
            new Item(ItemId.ThrowableStick, "Throwable stick", new List<ItemQuantity>()
            {
                new ItemQuantity(ItemId.Branch, 2)
            })
        };
    }
}