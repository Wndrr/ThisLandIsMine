using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float SpeedCoeficient;
    private CharacterController _characterController;
    public Canvas PlayerUi;
    private int TotalRessource;

    public GameObject WallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var targetPosition = new Vector2(x, y) * SpeedCoeficient;
        _characterController.Move(targetPosition);

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            var spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;
            Instantiate(WallPrefab, spawnPosition, Quaternion.identity);
        }

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
                    var ressourceCount = PlayerUi.GetComponentsInChildren<Text>().First(t => t.name == "RessourceCount");
                    TotalRessource += harvestedRessource;
                    ressourceCount.text = $"Wood: {TotalRessource}";
                }
            }
        }
    }
}
