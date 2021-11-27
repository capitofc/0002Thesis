using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Bounce : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            //AdvancedWalkerController simp = collision.gameObject.GetComponent<AdvancedWalkerController>();
            // Skills skillFX = collision.gameObject.GetComponentInChildren<Skills>();
            // skillFX.flyPlayer(collision.gameObject);
            //simp.jump();
            //collision.gameObject.GetComponent<SkillControls>().fly(collision.gameObject);
        }
    }
}
