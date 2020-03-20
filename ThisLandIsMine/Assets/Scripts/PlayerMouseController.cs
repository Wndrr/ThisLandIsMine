using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMouseController : MonoBehaviour
{
    public PlayerInventoryController Inventory { get; set; }
    
    public void HandleLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                if (hit.transform.CompareTag("collectable"))
                {
                    var ressource = hit.collider.GetComponentInParent<RessourceController>();
                    var harvestedRessource = ressource.Harvest();
                    Inventory.AddToResource(harvestedRessource);
                }
                else if(hit.transform.CompareTag("enemy"))
                {
                    var enemy = hit.collider.GetComponent<EnemyController>();
                    enemy.Hit();
                }
            }
        }
    }

}