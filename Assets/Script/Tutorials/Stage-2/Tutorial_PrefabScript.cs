using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_PrefabScript : MonoBehaviour
{

    public int cooldown = 5;
    public void Start()
    {
        ChangeValue();
    }
    public void ChangeValue()
    {
        ChangeCooldown();

        GameObject []main = GetComponent<Tutorial_Stage2Manager>().arithmeticPrefab;
        
        int random = Random.Range(0, main.Length);
        main[random].SetActive(true);
        this.gameObject.SetActive(false);
        StartCoroutine(changeValueTimer());
    }

    public void ChangeCooldown()
    {
        int cdRan = Random.Range(5, 20);
        cooldown = cdRan;
    }
    IEnumerator changeValueTimer()
    {
        while (cooldown > 0)
        {
            if (cooldown == 1)
            {
                StopAllCoroutines();
                ChangeValue();
            }
            cooldown--;
            yield return new WaitForSeconds(1f);
        }
    }
}
