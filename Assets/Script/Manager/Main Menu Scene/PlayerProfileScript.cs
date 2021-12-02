using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfileScript : MonoBehaviour
{
    [Header("Player UI Info")]
    [SerializeField] GameObject PlayerUsedAgent;
    [SerializeField] GameObject PlayerMoneyText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMoneyText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerMoney.ToString();
        PlayerUsedAgent.GetComponent<TextMeshProUGUI>().text = CharacterSelection.instance.GetPenguinName(Database.instance.UsedCharacter);
    }

    public void ChangeAccount()
    {
        SceneManager.LoadScene("Opening_Game_Scene");
    }
}
