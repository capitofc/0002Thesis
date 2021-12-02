using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanArithOperation : MonoBehaviour
{
    [SerializeField] public GameObject parent;

    

    private void OnTriggerStay(Collider other)
    {
        if ((other.tag.Equals("Maze") || other.tag.Equals("Raze") || other.tag.Equals("Zilch")) && GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().playerCorrectAnswer < 3)
        {
            GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().PickUpButton.SetActive(true);
            if (GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().canPick)
            {
                switch (gameObject.tag)
                {
                    case "Add":
                        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().playerCurrentAnswer += "+";
                        break;
                    case "Div":
                        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().playerCurrentAnswer += "/";
                        break;
                    case "Mod":
                        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().playerCurrentAnswer += "%";
                        break;
                    case "Mul":
                        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().playerCurrentAnswer += "*";
                        break;
                    case "Sub":
                        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().playerCurrentAnswer += "-";
                        break;
                }
                GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().CheckMyAnswer();
                GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().canPick = false;
                parent.GetComponent<LanArithmetic>().CmdDestroy();
            }
            //parent.GetComponent<LanArithmetic>().CmdDestroy();
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        GameObject.Find("Stage2Handler").GetComponent<LanStage2Handler>().PickUpButton.SetActive(false);
    }
}
