using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class EnemyController : MonoBehaviour
{
    public int MaxHealth = 10;
    private int _currentHealth = 10;
    private int _damageRate = 1;
    private TextMesh _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = gameObject.GetComponent<TextMesh>();
        _text.characterSize = .4f;
        _text.text = $"{_currentHealth}/{MaxHealth}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int Hit()
    {
        _currentHealth -= _damageRate;
        _text.text = _currentHealth.ToString();
        
        if(_currentHealth <= 0)
            Destroy(gameObject);

        return _currentHealth;
    }
}
