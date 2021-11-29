using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowSpawnerCamera : MonoBehaviour
{
    [SerializeField] public GameObject cameraRot;

    private void FixedUpdate()
    {
        try
        {
            gameObject.transform.rotation = cameraRot.transform.rotation;
        }
        catch(Exception e)
        {

        }
    }
}
