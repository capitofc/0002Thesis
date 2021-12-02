using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanFinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Zilch") || other.gameObject.tag.Equals("Trix")) && GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().playerCorrectAnswer == 3)
        {
            GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().CmdSetWinner(true);
            //Show congrats shit
        }
    }
}
