using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Stage2TriggerCheckPoint : MonoBehaviour
{
    [Header("Tutorial_Stage2Manager ")]
    [SerializeField] GameObject stage2Main;

    [Header("CheckPoint Location")]
    [SerializeField] GameObject checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
        {
            stage2Main.GetComponent<Tutorial_Stage2Manager>().CheckPointGo = checkpoint;
            Debug.Log($"Checkpoint changed to {checkpoint.gameObject.name} -- Main checkpoint : {stage2Main.GetComponent<Tutorial_Stage2Manager>().CheckPointGo.name}");
        }
    }
}
