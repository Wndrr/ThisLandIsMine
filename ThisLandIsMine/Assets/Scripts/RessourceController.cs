using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(CircleCollider2D))]
public class RessourceController : MonoBehaviour
{
    public int ResourceCount = 10;
    public int HarvestSpeed = 1;
    public ResourceType ResourceType;
    private TextMesh _text;
    
    // Start is called before the first frame update
    void Start()
    {
        _text = gameObject.GetComponent<TextMesh>();
    }

    public ResourceValue Harvest()
    {
        ResourceCount -= HarvestSpeed;
        _text.text = ResourceCount.ToString();
        
        if(ResourceCount <= 0)
            Destroy(gameObject);

        return new ResourceValue(ResourceType, HarvestSpeed);
    }
}

public enum ResourceType
{
    Wood
}

public class ResourceValue
{
    public ResourceValue(ResourceType type, int value)
    {
        Type = type;
        Value = value;
    }

    public ResourceType Type { get; set; }
    public int Value { get; set; }
}
