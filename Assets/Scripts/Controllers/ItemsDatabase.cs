using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDatabase : MonoBehaviour
{
    public static List<Item> Items;
    // Start is called before the first frame update
    void Start()
    {
        SeedDatabase();
    }

    private void SeedDatabase()
    {
        Items = new List<Item>
        {
            new Item(ItemId.Branch, "Branch")
        };
    }
}