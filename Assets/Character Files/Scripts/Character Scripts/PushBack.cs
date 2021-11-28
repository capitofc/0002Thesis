using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class PushBack : MonoBehaviour
{
    public float force = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 pushDirection = other.transform.position - transform.position;

            pushDirection = -pushDirection.normalized;
            GetComponent<AdvancedWalkerController>().groundFriction = 0.01f;
            GetComponent<AdvancedWalkerController>().AddMomentum(pushDirection * 5f * force);
            GetComponent<TuxAnimations>().playBump();
        }

        else if (other.gameObject.tag == "Tramp")
        {
            Vector3 pushDirection = other.transform.position - transform.position;

            pushDirection = -pushDirection.normalized;
            GetComponent<AdvancedWalkerController>().groundFriction = 0.01f;
            GetComponent<AdvancedWalkerController>().AddMomentum(new Vector3(0, 2, 0) * 5f * force);
            GetComponent<TuxAnimations>().playBump();
        }

        // else if (other.gameObject.tag == "Pipe")
        // {
        //     // Vector3 pushDirection = other.transform.position - transform.position;

        //     // pushDirection = -pushDirection.normalized;
        //     GetComponent<AdvancedWalkerController>().groundFriction = 0.01f;
        //     // GetComponent<AdvancedWalkerController>().AddMomentum(new Vector3(0, 2, 0) * 5f * force);
        //     GetComponent<TuxAnimations>().playBump();
        // }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Pipe")
        {
            // Vector3 pushDirection = other.transform.position - transform.position;

            // pushDirection = -pushDirection.normalized;
            GetComponent<AdvancedWalkerController>().groundFriction = 0.01f;
            // GetComponent<AdvancedWalkerController>().AddMomentum(new Vector3(0, 2, 0) * 5f * force);
            GetComponent<TuxAnimations>().playBump();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(decrease());
    }


    IEnumerator decrease()
    {
        while (GetComponent<AdvancedWalkerController>().groundFriction < 100)
        {
            if (GetComponent<AdvancedWalkerController>().groundFriction == 99f)
            {
                StopAllCoroutines();
            }
            GetComponent<AdvancedWalkerController>().groundFriction += 1;
            yield return new WaitForSeconds(0.002f);
        }
    }
}