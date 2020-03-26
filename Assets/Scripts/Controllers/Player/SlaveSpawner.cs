using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Models;
using UnityEngine;

public class SlaveSpawner : MonoBehaviour
{
    public int BerryCountNeededToSpawnSlave = 2;
    public GameObject SlavePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnSlave), 1, 5);
    }

    private void SpawnSlave()
    {
        var resourceStorages = GameObject.FindGameObjectsWithTag("Construction").Select(r => r.GetComponent<ResourceStorageController>()).Where(r => r != null);

        var totalBerries = resourceStorages.Sum(r => r._inventory.Items.Where(s => s.Id == ItemId.Berry).Select(s => s.Quantity).SingleOrDefault());

        if (totalBerries >= BerryCountNeededToSpawnSlave)
        {
            Instantiate(SlavePrefab, transform.position + (Vector3.up * 3), Quaternion.identity);
        }
    }
}