using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSymbol : MonoBehaviour
{
    // Start is called before the first frame update
    bool isasd = true;
    void Start()
    {
        StartCoroutine(changeAnak());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator changeAnak()
    {
        while (isasd)
        {
            int _index = Random.Range(0, 5);
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(i == _index);
            yield return new WaitForSeconds(3f);
        }

    }
}
