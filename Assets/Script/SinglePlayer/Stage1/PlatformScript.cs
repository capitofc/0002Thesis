using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] string value = null;
    
    public string GetValue()
    {
        return value;
    }

}
