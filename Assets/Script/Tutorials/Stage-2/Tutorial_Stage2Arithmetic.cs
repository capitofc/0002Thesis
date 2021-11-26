using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CMF;


public class Tutorial_Stage2Arithmetic : MonoBehaviour
{
    public string value = "";
    public GameObject pickUpBtn;
    AdvancedWalkerController pickUp;
    private GameObject arithObj;
    // Start is called before the first frame update


    void Start()
    {
        //pickUpBtn = GameObject.FindGameObjectWithTag("PickUp").GetComponent<Button>();
        //pickUpBtn = GameObject.Find("PickUpBtn");
        // pickUpBtn.onClick = () => { Debug.Log("Hello there!"); };
        //pickUpBtn.GetComponent<Button>().onClick.AddListener((delegate { this.pickUpMethod(value); }));
        pickUp = GetComponent<AdvancedWalkerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Add") || other.gameObject.tag.Equals("Sub") || other.gameObject.tag.Equals("Div")|| other.gameObject.tag.Equals("Mod")|| other.gameObject.tag.Equals("Mul"))
        {
            pickUpBtn.SetActive(true);
            value = other.gameObject.tag;
            arithObj = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Add") || other.gameObject.tag.Equals("Sub") || other.gameObject.tag.Equals("Div") || other.gameObject.tag.Equals("Mod") || other.gameObject.tag.Equals("Mul"))
        {
            pickUpBtn.SetActive(false);
            value = "";
            arithObj = null;
        }
    }

    public void pickUpMethod()
    {
        // trigger anim
        arithObj.SetActive(false);
        pickUp.pickUp();
        string answer ="";

        switch (value)
        {
            case "Add":
                answer = "+";
                break;
            case "Sub":
                answer = "-";
                break;
            case "Div":
                answer = "/";
                break;
            case "Mod":
                answer = "%";
                break;
            case "Mul":
                answer = "*";
                break;
        }

        Debug.Log(answer);
        GameObject Stage2Main = GameObject.Find("Tutorial_stage2_Handler");
        Stage2Main.GetComponent<Tutorial_Stage2Manager>().UpdateGivenText(answer);
    }
}
