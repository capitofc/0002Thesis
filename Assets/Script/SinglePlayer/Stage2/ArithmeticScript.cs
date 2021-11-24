using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArithmeticScript : MonoBehaviour
{
    public string value = "/";
    public string[] arithmetic = {"/", "*", "+", "-"};
    public int cooldown = 15;

    private void Start()
    {
        ChangeValue();
    }

    public void ChangeValue()
    {
        ChangeCooldown();
        int ran = Random.Range(0, 4);
        value = arithmetic[ran];
        gameObject.GetComponent<TextMeshPro>().text = value;
        StartCoroutine(changeValueTimer());
    }

    public void ChangeCooldown()
    {
        int cdRan = Random.Range(5, 20);
        cooldown = cdRan;
    }

    IEnumerator changeValueTimer()
    {
        while(cooldown > 0)
        {
            if(cooldown == 1)
            {
                StopAllCoroutines();
                ChangeValue();
            }
            cooldown--;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Maze") || other.gameObject.tag.Equals("Trix") || other.gameObject.tag.Equals("Zilch"))
        {
            GameObject Stage2Main = GameObject.Find("SinglePlayerHandler");
            if (Stage2Main.GetComponent<Stage2ScriptHandler>().onCollect)
            {
                Stage2Main.GetComponent<Stage2ScriptHandler>().UpdateGivenText(value);
                gameObject.SetActive(false);
                Invoke(nameof(VisibleMe), Random.Range(10, 15));
            }
        }
    }

    public void VisibleMe()
    {
        gameObject.SetActive(true);
    }
}
