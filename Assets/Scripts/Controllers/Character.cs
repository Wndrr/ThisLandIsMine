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

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        _inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        //  transform.Rotate(0,  turnSpeed * Time.deltaTime, 0);

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


        var main = Camera.main;
        var tt = (-transform.forward + transform.up) * 4;
        var targetPost = transform.position + tt;


        var mouseYOffset = -Input.GetAxis("Mouse Y");
        var newPos = new Vector3(targetPost.x, main.transform.position.y, targetPost.z);
        newPos += main.transform.up * mouseYOffset;

        main.transform.position = newPos;
        main.transform.LookAt(transform);


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
}