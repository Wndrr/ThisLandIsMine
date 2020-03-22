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

            return new ItemQuantity(ItemId.Branch, harvestSpeed);
        }
    }
}