using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1DeadZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Maze" || other.gameObject.tag == "Zilch" || other.gameObject.tag == "Trix")
        {
            GameObject.Find("SinglePlayerHandler").GetComponent<Stage1ScriptHandler>().isDead = true;
        }
    }
}
