using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterMovementController))]
[RequireComponent(typeof(PlayerMouseController))]
[RequireComponent(typeof(PlayerInventoryController))]
public class PlayerController : MonoBehaviour
{
    public float speedCoeficient = .3f;
    private CharacterMovementController _movementController;
    private PlayerMouseController _mouseController;
    private PlayerInventoryController _inventoryController;
    private Transform _playerUi;

    public GameObject WallPrefab;
    public GameObject KnifePrefab;

    // Start is called before the first frame update
    void Start()
    {
        _movementController = gameObject.GetComponent<CharacterMovementController>();
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


        if (Input.GetMouseButtonDown(1))
        {
            if (_inventoryController.HasItem())
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).ToVector2();
                var playerPosition = gameObject.transform.position.ToVector2();
                var throwDirection = (mousePosition - playerPosition).normalized;
                var throwStartingPosition = playerPosition + throwDirection;
                var knifeRotation = Quaternion.LookRotation(throwDirection);
                var tt = Instantiate(KnifePrefab, throwStartingPosition, knifeRotation);
                var thrownKnife = tt.GetComponent<ThrowableWeapon>();
                thrownKnife.Direction = throwDirection;
                _inventoryController.RemoveItem();
            }
        }

        _mouseController.HandleLeftClick();
    }
}