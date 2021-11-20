using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckpoint : MonoBehaviour
{
    [Header("Stage2ScriptHandler")]
    [SerializeField] GameObject stage2Main;

    [Header("CheckPoint Location")]
    [SerializeField] GameObject checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
        {
            stage2Main.GetComponent<Stage2ScriptHandler>().CheckPointGo = checkpoint;
            Debug.Log($"Checkpoint changed to {checkpoint.gameObject.name} -- Main checkpoint : {stage2Main.GetComponent<Stage2ScriptHandler>().CheckPointGo.name}");
        }
    }

}
