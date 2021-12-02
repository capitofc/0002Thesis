using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSnowball : MonoBehaviour
{
    public GameObject snowball;
    public GameObject cam;
    public GameObject spawner;
    public string penguinType;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }


    public void throwSnow()
    {
        snowball.GetComponent<Snow>().setPenguinType(penguinType);
        snowball.GetComponent<Snow>().setRotPos(cam, spawner);
        snowball.SetActive(true);
    }

    public void setPenguinType(string penguinType)
    {
        this.penguinType = penguinType;
    }
}
