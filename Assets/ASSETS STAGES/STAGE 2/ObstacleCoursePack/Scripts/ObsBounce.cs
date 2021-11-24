using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class ObsBounce : MonoBehaviour
{
    public float force = 10f; //Force 10000f
    public float stunTime = 0.5f;
    private Vector3 hitDir;

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (collision.gameObject.tag == "Maze")
            {
                hitDir = contact.normal;
                collision.gameObject.GetComponent<Rigidbody>().AddForce(-hitDir.normalized * force, ForceMode.Acceleration);
                collision.gameObject.GetComponent<TuxAnimations>().playBump();
                return;
            }
        }
    }
}
