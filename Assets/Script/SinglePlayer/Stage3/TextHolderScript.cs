using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHolderScript : MonoBehaviour
{
    [Header("Text Holder")]
    [SerializeField] public string startValue = "";

    [Header("Stage GameObject")]
    [SerializeField] GameObject StageGameObject;
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<TextMeshPro>().text = startValue;
    }

    public void setValue(string value)
    {
        startValue = value;
        GetComponent<TextMeshPro>().text = startValue;
    }

    public GameObject GetStageGameObject()
    {
        return StageGameObject;
    }
}
