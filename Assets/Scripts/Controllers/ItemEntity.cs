using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
