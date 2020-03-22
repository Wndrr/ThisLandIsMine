using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(CharacterController))]
public class MobAi : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _targetPosition;
    private int speed = 6;
    private bool _isTargetPositionReached = false;

    private int _targetPositionMaxDistance = 20;
    // Start is called before the first frame update
    void Start()
    {
        var random = new System.Random();
        _controller = GetComponent<CharacterController>();
        InvokeRepeating(nameof(ChoseNewTargetPosition), 1f, random.Next(3, 15));
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isTargetPositionReached)
            Move();
    }
    private void Move()
    {
        if (_targetPosition.x == transform.position.x && _targetPosition.z == transform.position.z)
        {
            _isTargetPositionReached = true;
        }

        var movementOffset = _targetPosition - transform.position;
        
        _controller.SimpleMove(movementOffset.normalized * speed);
    }

    private void ChoseNewTargetPosition()
    {
        var rand = new Random();

        var xOffset = rand.Next(-_targetPositionMaxDistance, _targetPositionMaxDistance);
        var zOffset = rand.Next(-_targetPositionMaxDistance, _targetPositionMaxDistance);

        var targetPosition = transform.position;
        targetPosition.x += xOffset;
        targetPosition.z += zOffset;
        targetPosition.y = 0;

        _targetPosition = targetPosition;
        _isTargetPositionReached = false;
    }
}
