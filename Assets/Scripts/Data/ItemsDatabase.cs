using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public static ItemsDatabase Instance;
    public List<Item> items;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        SeedDatabase();
        DontDestroyOnLoad(this);
    }

    private void SeedDatabase()
    {
        items = new List<Item>
        {
            new Item(ItemId.Branch, "Branch"),
            new Item(ItemId.ThrowableStick, "Throwable stick", new List<ItemQuantity>()
            {
                new ItemQuantity(ItemId.Branch, 2)
            })
        };
    }
}