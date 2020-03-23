using System;
using Data.Models;
using UnityEngine;

namespace Controllers.Misc
{
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

            return GetHarvestResult();
        }

        public ItemQuantity GetHarvestResult()
        {
            switch (type)
            {
                case ResourceEntityType.Bush:
                    return new ItemQuantity(ItemId.Berry, 1);
                case ResourceEntityType.Rock:
                    return new ItemQuantity(ItemId.Stone, 1);
                case ResourceEntityType.Tree:
                    return new ItemQuantity(ItemId.Branch, 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}