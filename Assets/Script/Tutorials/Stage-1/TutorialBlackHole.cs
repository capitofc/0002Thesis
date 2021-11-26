using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlackHole : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] GameObject spawnPoint; //Lagay mo dito ung game object kung san mo sya gusto amg spawn pag nahulog

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.transform.position = spawnPoint.transform.position;
        }
    }
}
