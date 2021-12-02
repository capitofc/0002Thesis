using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    private Text txt;
    void Awake()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "₱" + Database.instance.playerMoney;
    }
}
