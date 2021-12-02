using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    public string playerName = null;
    public int playerMoney = 0;
    public int playerLevel = 1;
    public float playerCurrentExp = 0;
    public float playerLevelCapacity = 10;
    public bool[] UnlockedStages = { true, false, false, false, false };
    public bool[] UnlockCharacter = { true, false, false, true, false, false, true, false, false };
    public int UsedCharacter = 1;
    public static Database instance;

    private void Start()
    {
        instance = this;
    }

}

