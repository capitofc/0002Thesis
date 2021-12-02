using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class s4ColliderScript : MonoBehaviour
{
    [Header("Text Holder Game Object")]
    [SerializeField] GameObject TextHolderGameObject;

    [Header("Main Handler")]
    [SerializeField] GameObject Main;

    [Header("Environment Dependency")]
    [SerializeField] GameObject Platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
        {
            GameObject Stage3Main = GameObject.Find("Stage3MainHandler");
            if (Stage3Main.GetComponent<Stage3ScriptHandler>().correctAnswer.Equals(TextHolderGameObject.GetComponent<TextHolderScript>().startValue))
            {
                Main.GetComponent<Stage3ScriptHandler>().GenerateGiven();
                Platform.SetActive(false);
            }
        }
    }

    public void resetDefault()
    {
        Platform.SetActive(true);
    }
}
