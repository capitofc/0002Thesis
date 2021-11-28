using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LanSnowBall : NetworkBehaviour
{
    public void Start()
    {
        Test();
    }

    public void Test()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        StartCoroutine(Destroyer(gameObject));
    }

    IEnumerator Destroyer(GameObject me)
    {
        yield return new WaitForSeconds(3f);
        NetworkServer.Destroy(me);
    }
}
