using System.Collections.Generic;
using Data.Models;
using UnityEngine;

namespace Data
{
    public class ItemsDatabase : MonoBehaviour
    {
        public static ItemsDatabase Instance;
        public List<Item> items;
        // Start is called before the first frame update
        private void Awake()
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
                new Item(ItemId.ThrowableStick, "Throwable stick", new List<ItemQuantity>
                {
                    new ItemQuantity(ItemId.Branch, 2)
                }),
                new Item(ItemId.ResourceManager, "Resource manager",new List<ItemQuantity>()
                {
                    new ItemQuantity(ItemId.Branch, 5),
                    new ItemQuantity(ItemId.Stone, 5)
                })
            };
        }
    }
}