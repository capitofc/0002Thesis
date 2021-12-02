using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Trix")
        {
            Debug.Log("Its Colliding!");
        }
    }
}
