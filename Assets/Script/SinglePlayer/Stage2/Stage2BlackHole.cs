using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BlackHole : MonoBehaviour
{
    [Header("Stage2MainHandler")]
    [SerializeField] GameObject main;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = main.GetComponent<Stage2ScriptHandler>().CheckPointGo.transform.position;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Zilch"))
        {
            collision.gameObject.transform.position = main.GetComponent<Stage2ScriptHandler>().CheckPointGo.transform.position;
        }
    }
}
