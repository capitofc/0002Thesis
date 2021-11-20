using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float force = 3000f; //Force 10000f
    public float stunTime = 0.5f;
    private Vector3 hitDir;

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze")|| collision.gameObject.tag.Equals("Zilch"))
            {
                hitDir = contact.normal;
                //collision.gameObject.GetComponent<ThirdPersonMovement>().HitPlayer(-hitDir * force, stunTime);
                //collision.gameObject.GetComponent<ThirdPersonMovement>().bumpAnim();

                collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up.normalized * force, ForceMode.Impulse);

                //push backs the target
               // force = 350; //Hard
                //Vector3 dir = collision.contacts[0].point - transform.position;
               // dir = -dir.normalized;
                //GetComponent<Rigidbody>().AddForce(dir * force);
                return;
            }
        }
    }
}
