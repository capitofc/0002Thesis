using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    [Header("Dead UI")]
    [SerializeField] GameObject DeadUi;

    [Header("Stage1Handler")]
    [SerializeField] GameObject Stage1Handler;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided : {collision.gameObject.tag}");
        if ((collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Zilch")) && !Stage1Handler.GetComponent<Stage1ScriptHandler>().isGameEnded())
        {
            Stage1Handler.GetComponent<Stage1ScriptHandler>().isDead = true;
            Stage1Handler.GetComponent<Stage1ScriptHandler>().GameDefault();
            Stage1Handler.GetComponent<Stage1ScriptHandler>().PlayerUi.SetActive(false);
            DeadUi.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided : {other.gameObject.tag}");
        if ((other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch")) && !Stage1Handler.GetComponent<Stage1ScriptHandler>().isGameEnded())
        {
            Stage1Handler.GetComponent<Stage1ScriptHandler>().isDead = true;
            Stage1Handler.GetComponent<Stage1ScriptHandler>().GameDefault();
            Stage1Handler.GetComponent<Stage1ScriptHandler>().PlayerUi.SetActive(false);
            DeadUi.SetActive(true);
        }
    }
}
