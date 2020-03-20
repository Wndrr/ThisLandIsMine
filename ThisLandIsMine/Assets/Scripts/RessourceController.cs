using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class RessourceController : MonoBehaviour
{
    public int RessourceCount = 10;
    public int HarvestSpeed = 1;
    public RessourceType RessourceType;
    private TextMesh _text;
    
    // Start is called before the first frame update
    void Start()
    {
        _text = gameObject.GetComponent<TextMesh>();
    }

    public int Harvest()
    {
        RessourceCount -= HarvestSpeed;
        _text.text = RessourceCount.ToString();
        
        if(RessourceCount <= 0)
            Destroy(gameObject);
        
        return HarvestSpeed;
    }
}

public enum RessourceType
{
    Wood
}
