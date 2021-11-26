using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;

    [Header("Models")]
    [SerializeField] GameObject[] Models;

    [Header("Controllers")]
    [SerializeField] GameObject buttonDescription;
    [SerializeField] GameObject actionButton;

    [Header("UI Objects")]
    [SerializeField] GameObject moneyUIText;
    [SerializeField] GameObject modelName;
    [SerializeField] GameObject purchase_Error;
    [SerializeField] GameObject purchase_Success;

    private int PenguinReferencePointer = 0;
    private Dictionary<int, int> PenguinPrice = new Dictionary<int, int>();
    private Dictionary<int, string> PenguinName = new Dictionary<int, string>();

  

    private void Start()
    {
        InitPenguinName();
        InitPenguinPrice();
        UpdateUiObjectsValue();
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Database.instance.playerMoney += 100;
            UpdateUiObjectsValue();
        }
    }

    public void ButtonPenguinScript(int pengiunRef)
    {
        if (Database.instance.UnlockCharacter[pengiunRef])
        {
            if (pengiunRef == Database.instance.UsedCharacter)
            {
                buttonDescription.GetComponent<TextMeshProUGUI>().text = "USED";
                actionButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                buttonDescription.GetComponent<TextMeshProUGUI>().text = "SELECT";
                actionButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            buttonDescription.GetComponent<TextMeshProUGUI>().text = "$ " + PenguinPrice[pengiunRef];
            actionButton.GetComponent<Button>().interactable = true;
        }
        PenguinReferencePointer = pengiunRef;
        InitPenguin(PenguinReferencePointer);
        UpdateUiObjectsValue();
    }

    private void InitPenguinPrice()
    {
        PenguinPrice.Add(1, 100);
        PenguinPrice.Add(2, 250);
        PenguinPrice.Add(4, 150);
        PenguinPrice.Add(5, 300);
        PenguinPrice.Add(7, 200);
        PenguinPrice.Add(8, 400);
    }
    private void InitPenguinName()
    {
        PenguinName.Add(0, "ZilchBase");
        PenguinName.Add(1, "Zilch2Model");
        PenguinName.Add(2, "Zilch3Model");
        PenguinName.Add(3, "TrixBase");
        PenguinName.Add(4, "Trix1Model");
        PenguinName.Add(5, "Trix2Model");
        PenguinName.Add(6, "MazeBase");
        PenguinName.Add(7, "Maze1Model");
        PenguinName.Add(8, "Maze2Model");
    }

    public void InitPenguin(int _index)
    {
        for(int i = 0; i < Models.Length; i++)
        {
            if(i == _index)
            {
                Models[i].SetActive(true);
            }
            else
            {
                Models[i].SetActive(false);
            }
        }
    }

   public void ActionButtonHander()
    {
        if (buttonDescription.GetComponent<TextMeshProUGUI>().text.Equals("SELECT"))
        {
            Database.instance.UsedCharacter = PenguinReferencePointer;
            InitPenguin(Database.instance.UsedCharacter);
            ButtonPenguinScript(PenguinReferencePointer);
            Debug.Log($"Saving new selected penguin -- {PenguinName[PenguinReferencePointer]}");
            SaveData.SaveDataProgress(Database.instance);
        }
        else
        {
            if(Database.instance.playerMoney >= PenguinPrice[PenguinReferencePointer])
            {
                SoundScript.instance.playSuccessfulFx();
                Database.instance.playerMoney -= PenguinPrice[PenguinReferencePointer];
                Database.instance.UnlockCharacter[PenguinReferencePointer] = true;
                ButtonPenguinScript(PenguinReferencePointer);
                UpdateUiObjectsValue();
                purchase_Success.SetActive(true);
                Debug.Log($"Saving new unlocked penguin -- {PenguinName[PenguinReferencePointer]}");
                SaveData.SaveDataProgress(Database.instance);
                //SAVE DATA ULET
            }
            else
            {
                purchase_Error.SetActive(true);
            }
        }
    }

    private void UpdateUiObjectsValue()
    {
        moneyUIText.GetComponent<TextMeshProUGUI>().text = Database.instance.playerMoney.ToString();
        modelName.GetComponent<TextMeshProUGUI>().text = PenguinName[PenguinReferencePointer];
    }

    public string GetPenguinName(int index)
    {
        return PenguinName[index];
    }
}
