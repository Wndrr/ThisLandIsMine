using Data.Models;
using UnityEngine;

namespace Controllers.Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class ItemEntity : MonoBehaviour
    {
        public ItemId id;

        public ItemQuantity Harvest()
        {
            Destroy(gameObject);
            return new ItemQuantity(id, 1);
        }
    }
}
