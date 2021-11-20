using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataPrefab : MonoBehaviour
{
    [SerializeField] GameObject PrefabPlayerName;
    [SerializeField] GameObject LoadProgressObj;

    public void SetPrefabNameText(string name)
    {
        PrefabPlayerName.GetComponent<TextMeshProUGUI>().text = name;
    }

    public void InitializePlayerPrefab()
    {
        LoadProgressObj.GetComponent<LoadProgress>().InstantiatePlayerData(PrefabPlayerName.GetComponent<TextMeshProUGUI>().text);
        SceneManager.LoadScene("Main_Menu_Scene");
    }
}
