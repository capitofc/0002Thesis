using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class CreatePlayerData
{
    public static string path = SaveData.GetPath();
    public static List<PlayerData> PlayerDataList = SaveData.GetPlayerDataList();

    private static bool isExist(string playerName)
    {
        SaveData.LoadData();
        bool status = false;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            List<PlayerData> PlayerDataList = SaveData.GetPlayerDataList();
            foreach (PlayerData data in PlayerDataList)
            {
                if (data.playerName.Equals(playerName))
                {
                    return true;
                }
            }
            stream.Close();
        }
        return status;
    }

    public static void CreatePlayer(Database player)
    {
        Debug.Log($"Creating player .... ");
        if (isExist(player.playerName))
        {
            Debug.Log("Error: Player already exist");
        }
        else
        {
            SaveData.LoadData();
            List<PlayerData> dataList = SaveData.GetPlayerDataList();
            if (dataList.Count == 0)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                dataList.Add(new PlayerData(player));
                formatter.Serialize(stream, dataList);
                stream.Close();
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                dataList.Add(new PlayerData(player));
                formatter.Serialize(stream, dataList);
                stream.Close();
            }
            SceneManager.LoadScene("Main_Menu_Scene");
        }
        
    }
}
