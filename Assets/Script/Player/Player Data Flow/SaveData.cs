using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveData
{
    private static List<PlayerData> PlayerDataList = new List<PlayerData>();
    private static string path = Application.persistentDataPath + "/GameData.Thesis";

    public static string GetPath()
    {
        return path;
    }

    public static List<PlayerData> GetPlayerDataList()
    {
        return PlayerDataList;
    }

    public static void LoadData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            PlayerDataList = formatter.Deserialize(stream) as List<PlayerData>;
            stream.Close();
        }
    }

    public static void SaveDataProgress(Database localPlayer)
    {
        LoadData();
        Debug.Log($"Saving progress of {localPlayer.playerName}");
        for(int i = 0; i < PlayerDataList.Count; i++)
        {
            Debug.Log($"For Loop Info {i} PlayerDataList: {PlayerDataList[i].playerName} -- Our name : {Database.instance.playerName}");
            if (PlayerDataList[i].playerName.Equals(Database.instance.playerName))
            {
                PlayerDataList[i] = new PlayerData(Database.instance);
                Debug.Log($"Data instance : {Database.instance.UsedCharacter} --- PlayerDataList :{PlayerDataList[i].UsedCharacter}");
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                formatter.Serialize(stream, PlayerDataList);
                stream.Close();
                LoadData();
                break;
            }
        }
    }

    
}
