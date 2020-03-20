using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(CharacterMovementController))]
public class EnemyController : MonoBehaviour
{
    public int MaxHealth = 10;
    private int _currentHealth = 10;
    private int _damageRate = 1;
    private TextMesh _text;

    private CharacterMovementController _movementController;
    private GameObject CurrentTarget; 
    
    // Start is called before the first frame update
    void Start()
    {
        _movementController = gameObject.GetComponent<CharacterMovementController>();
        CurrentTarget = GameObject.Find("Player");
        _text = gameObject.GetComponent<TextMesh>();
        _text.characterSize = .3f;
        UpdateOverlayText();
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = CurrentTarget.transform.position.ToVector2();
        var currentPosition = gameObject.transform.position.ToVector2();
        var desiredNewPosition = targetPosition - currentPosition;
        _movementController.Move(desiredNewPosition * Time.deltaTime);
    }
    
    public int Hit()
    {
        _currentHealth -= _damageRate;
        UpdateOverlayText();
        
        if(_currentHealth <= 0)
            Destroy(gameObject);

        return _currentHealth;
    }

    private void UpdateOverlayText()
    {
        _text.text = $"{_currentHealth}/{MaxHealth}";
    }
}
