using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Spawnee;
    public bool StopSpawning = false;
    public float SpawnTime;
    public float SpawnDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), SpawnTime, SpawnDelay);
    }

    public void SpawnObject()
    {
        Instantiate(Spawnee, transform.position, transform.rotation);
        if (StopSpawning)
        {
            CancelInvoke(nameof(SpawnObject));
        }
    }
}
