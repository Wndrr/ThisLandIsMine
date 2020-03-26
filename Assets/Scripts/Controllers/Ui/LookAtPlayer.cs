using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        if (player == null)
            return;
        transform.rotation = Camera.main.transform.rotation;
    }
}
