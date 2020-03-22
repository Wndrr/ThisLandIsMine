using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Inventory))]
public class Character : MonoBehaviour
{
    private CharacterController controller;
    private Inventory _inventory;
    public float turnSpeed = 10;
    public float speed = 1;
    private bool shouldCameraMove;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _inventory = GetComponent<Inventory>();
        

        Cursor.visible = false;
        shouldCameraMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleLeftAltKey();
        PlayerMovement();
        CameraMovement();
        HandleLeftClick();
    }

    private void HandleLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var rayOrigin = transform.position;
            var rayDirection = Camera.main.transform.forward;
            var hits = Physics.RaycastAll(rayOrigin, rayDirection, 5);

            if (hits.Any())
            {
                var resource = hits.FirstOrDefault();
                if (resource.collider.gameObject.CompareTag("Resource"))
                {
                    var obtainedItems = resource.collider.gameObject.GetComponent<Resource>().Harvest();
                    
                    _inventory.Add(obtainedItems);
                }
            }
        }
    }

    private void OnInventoryUpdateChange()
    {
        
    }

    private void CameraMovement()
    {
        var main = Camera.main;
        var tt = (-transform.forward + transform.up) * 4;
        var targetPost = transform.position + tt;

        if (shouldCameraMove)
        {
            var newPos = new Vector3(targetPost.x, main.transform.position.y, targetPost.z);
            var mouseYOffset = -Input.GetAxis("Mouse Y");
            newPos += main.transform.up * mouseYOffset;

            main.transform.position = newPos;
            main.transform.LookAt(transform);
        }
    }

    private void PlayerMovement()
    {
        var mouseXOffset = Input.GetAxis("Mouse X");

        transform.Rotate(0, mouseXOffset * turnSpeed, 0);

        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.Z))
            direction = transform.forward;
        if (Input.GetKey(KeyCode.S))
            direction = -transform.forward;

        if (Input.GetKey(KeyCode.D))
            direction += transform.right;
        if (Input.GetKey(KeyCode.Q))
            direction += -transform.right;

        controller.SimpleMove(direction * speed);
    }

    private void HandleLeftAltKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            shouldCameraMove = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            shouldCameraMove = true;
        }
    }
}