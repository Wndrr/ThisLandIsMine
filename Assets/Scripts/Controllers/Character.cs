﻿using System;
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
    private bool _isPlayerControlEnabled;
    public GameObject throwable;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _inventory = GetComponent<Inventory>();

        Events.current.OnToggleCraftingOverlay += TogglePlayerControl;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _isPlayerControlEnabled = true;
    }

    private void TogglePlayerControl()
    {
        _isPlayerControlEnabled = !_isPlayerControlEnabled;
    }

    void Update()
    {
        PlayerMovement();
        CameraMovement();
        HandleLeftClick();

        if (Input.GetMouseButtonDown(1))
        {
            var transformPosition = transform.position;
            transformPosition += transform.forward;
            transformPosition.y += transform.lossyScale.y;
            var transformRotation = Camera.main.transform.rotation;
            Instantiate(throwable, transformPosition, transformRotation);
        }

        if (Input.GetKeyDown(KeyCode.I))
            Events.current.TriggerToggleCraftingOverlay();
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
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Resource"))
                    {
                        var obtainedItems = hit.collider.gameObject.GetComponent<Resource>().Harvest();

                        _inventory.Add(obtainedItems);
                        break;
                    }
                    else if (hit.collider.gameObject.CompareTag("ItemEntity"))
                    {
                        var obtainedItems = hit.collider.gameObject.GetComponent<ItemEntity>().Harvest();

                        _inventory.Add(obtainedItems);
                        break;
                    }
                }
                
            }
        }
    }
    
    private void CameraMovement()
    {
        if (!_isPlayerControlEnabled)
            return;
        var main = Camera.main;
        var tt = (-transform.forward + transform.up) * 4;
        var targetPost = transform.position + tt;

        var newPos = new Vector3(targetPost.x, main.transform.position.y, targetPost.z);
        var mouseYOffset = -Input.GetAxis("Mouse Y");
        newPos += main.transform.up * mouseYOffset;

        main.transform.position = newPos;
        main.transform.LookAt(transform);
    }

    private void PlayerMovement()
    {
        if (!_isPlayerControlEnabled)
            return;

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

    private void OnDestroy()
    {
        Events.current.OnToggleCraftingOverlay -= TogglePlayerControl;
    }
}