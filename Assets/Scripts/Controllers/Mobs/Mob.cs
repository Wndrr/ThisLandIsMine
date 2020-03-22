using System;
using System.Collections.Generic;
using Controllers.Items;
using Data.Models;
using UnityEngine;

namespace Controllers.Mobs
{
    [RequireComponent(typeof(MobAi))]
    public class Mob : MonoBehaviour
    {
        private int Health = 3;
        public MobType type;
        public GameObject LootPrefab;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Hit()
        {
            Health--;

            if (Health <= 0)
            {
                Destroy(this.gameObject);

                SpawnLoot();
            }
        }

        private void SpawnLoot()
        {
            var lootToSpawn = GetLootToSpawn();

            foreach (var item in lootToSpawn)
            {
                SpawnItem(item);
            }
        }

        private void SpawnItem(ItemQuantity item)
        {
            for (int i = 0; i < item.Quantity; i++)
            {
                var spawnedItem = Instantiate(LootPrefab, transform.position + (transform.up * 2), Quaternion.identity);
                spawnedItem.GetComponent<ItemEntity>().id = item.Id;
            }        
        }

        private IEnumerable<ItemQuantity> GetLootToSpawn()
        {
            switch (type)
            {
                case MobType.DefaultMob:
                    return new List<ItemQuantity>()
                    {
                        new ItemQuantity(ItemId.MobSkin, 1),
                        new ItemQuantity(ItemId.MobMeat, 1),
                        new ItemQuantity(ItemId.MobBone, 1),
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}