using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(CharacterMovementController))]
public class ThrowableWeapon : MonoBehaviour
{
    private CharacterMovementController _movementController;
    private CircleCollider2D _collider;
    public float Speed = 5;
    public Vector2 Direction;
    
    // Start is called before the first frame update
    void Start()
    {
        _movementController = gameObject.GetComponent<CharacterMovementController>();
        _collider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementController.Move(Direction * (Speed * Time.deltaTime));
    }
    
    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("enemy"))
        {
            var enemy = col.gameObject.GetComponent<EnemyController>();
            enemy.Hit();
        }
    }
}
