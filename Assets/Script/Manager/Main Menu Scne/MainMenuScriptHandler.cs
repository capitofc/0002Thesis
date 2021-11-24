using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuScriptHandler : MonoBehaviour
{
    [Header("Player UI Info")]
    [SerializeField] GameObject PlayerNameText;
    [SerializeField] GameObject PlayerMoneyText;
    [SerializeField] GameObject PlayerLevelText;
    [SerializeField] GameObject PlayerProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNameText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerName;
        PlayerMoneyText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerMoney.ToString();
        PlayerLevelText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerLevel.ToString();
    }

    public void initPenguin(int method)
    {
        if(method == 1)
        {
            CharacterSelection.instance.InitPenguin(Database.instance.UsedCharacter);
        }
        else
        {
            CharacterSelection.instance.InitPenguin(-1);
        }
    }
}
