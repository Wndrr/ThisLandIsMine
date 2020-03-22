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
        private int _health = 3;
        public MobType type;
        public GameObject lootPrefab;

        public void Hit()
        {
            _health--;

            if (_health <= 0)
            {
                Destroy(gameObject);

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
            for (var i = 0; i < item.Quantity; i++)
            {
                var spawnedItem = Instantiate(lootPrefab, transform.position + (transform.up * 2), Quaternion.identity);
                spawnedItem.GetComponent<ItemEntity>().id = item.Id;
            }        
        }

        private IEnumerable<ItemQuantity> GetLootToSpawn()
        {
            switch (type)
            {
                case MobType.DefaultMob:
                    return new List<ItemQuantity>
                    {
                        new ItemQuantity(ItemId.MobSkin, 1),
                        new ItemQuantity(ItemId.MobMeat, 1),
                        new ItemQuantity(ItemId.MobBone, 1)
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}