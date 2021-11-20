using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpCalculator : MonoBehaviour
{
    public static PlayerExpCalculator instance;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdatePlayerLevel()
    {
        if(Database.instance.playerCurrentExp > Database.instance.playerLevelCapacity)
        {
            float newCurrentExp = Database.instance.playerCurrentExp - Database.instance.playerLevelCapacity;
            float newPlayerLevelCapacity = Database.instance.playerLevelCapacity + (Database.instance.playerLevelCapacity * 0.5f);
            int newPlayerLevel = Database.instance.playerLevel + 1;
            SaveProgress.instance.UpdatePlayerData(Database.instance.playerName,
                                                    Database.instance.playerMoney,
                                                    newPlayerLevel, newCurrentExp, newPlayerLevelCapacity,
                                                    Database.instance.UnlockedStages, Database.instance.UnlockCharacter, Database.instance.UsedCharacter);
            Database.instance.playerCurrentExp = newCurrentExp;
            Database.instance.playerLevelCapacity = newPlayerLevelCapacity;
            Database.instance.playerLevel = newPlayerLevel;
        }
    }
}
