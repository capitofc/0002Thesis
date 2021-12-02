using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cam;
    void Awake()
    {
        cam = GameObject.Find("Camerea Controls");
    }

    public void activate()
    {
        cam.SetActive(false);
    }
}
