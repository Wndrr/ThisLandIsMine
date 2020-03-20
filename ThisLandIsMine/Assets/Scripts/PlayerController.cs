using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerMouseController))]
[RequireComponent(typeof(PlayerInventoryController))]
public class PlayerController : MonoBehaviour
{
    public float speedCoeficient = .3f;
    private PlayerMovementController _movementController;
    private PlayerMouseController _mouseController;
    private PlayerInventoryController _inventoryController;
    private Transform _playerUi;

    public GameObject WallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _movementController = gameObject.GetComponent<PlayerMovementController>();
        _mouseController = gameObject.GetComponent<PlayerMouseController>();
        _mouseController.Inventory = _mouseController.GetComponent<PlayerInventoryController>();
        _inventoryController = gameObject.GetComponent<PlayerInventoryController>();
        _playerUi = gameObject.transform.Find("Player UI");
        _inventoryController.Ui = _playerUi.GetComponent<PlayerUiController>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var targetPosition = new Vector2(x, y) * speedCoeficient;
        _movementController.Move(targetPosition);

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            var spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;
            Instantiate(WallPrefab, spawnPosition, Quaternion.identity);
        }

        _mouseController.HandleLeftClick();

    }
}
