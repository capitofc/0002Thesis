using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class LanStage2Handler : NetworkBehaviour
{
    [SerializeField] public GameObject Player;

    [Header("Player UI")] //GameObject that needs to be declare here from GoStoreScript
    [SerializeField] public GameObject GivenTextGo;
    [SerializeField] public GameObject GivenTimerTextGo;
    [SerializeField] public GameObject CorrectAnswerTextGo;
    [SerializeField] public GameObject ReadyGivenTimerGo;
    [SerializeField] public GameObject QuestionNumberTextGo;
    [SerializeField] public List<GameObject> PickupPointsGo;
    [SerializeField] public List<GameObject> PowerUpsGo;
    [SerializeField] public GameObject PickUpButton;

    //Parent
    [SerializeField] public GameObject GivenPanelParent;
    [SerializeField] public GameObject TimerPanelParent;
    [SerializeField] public GameObject CorrectAnswerPanelParent;

    [SerializeField] public GameObject arithPrefab;

    public string playerCurrentAnswer = "";
    public int playerCorrectAnswer = 0;
    public bool canPick = false;
    public Dictionary<int, string> given;

    [SyncVar(hook = nameof(s2ReadyTimerIntHook))]
    public int s2ReadyTimerInt = 4;
    [SyncVar(hook = nameof(s2GivenTextStrHook))]
    public string s2GivenTextStr = "";
    [SyncVar(hook = nameof(s2CorrectAnswerStrHook))]
    public string s2CorrectAnswerStr = "";
    [SyncVar(hook = nameof(GivenTimerTextIntHook))]
    public int GivenTimerTextInt = 0;
    [SyncVar]
    public bool isWinner = false;

    private void Start()
    {
        DontDestroyOnLoad(this);

    }

    private void Update()
    {
        if (isServer && Input.GetKeyDown(KeyCode.Alpha8))
        {
            StartGame();
        }
    }

    #region Environment Initialization
    public void CheckMyAnswer()
    {
        if (playerCurrentAnswer.Equals(s2CorrectAnswerStr))
        {
            Debug.Log("Correct!");
            playerCorrectAnswer++;
            CorrectAnswerTextGo.GetComponent<TextMeshProUGUI>().text = $"Correct Answer: {playerCorrectAnswer}/3";
            if(playerCorrectAnswer == 3)
            {
                
            }
            GenerateGiven();
            //Change the given.
            //Add score to me
        }
        playerCurrentAnswer = "";
    }

    public void GetAllUiGo()
    {
        S2GoStoreScript Lan2SceneStore = GameObject.Find("S2GoStorage").GetComponent<S2GoStoreScript>();
        GivenTextGo = Lan2SceneStore.GivenTextGo;
        GivenTimerTextGo = Lan2SceneStore.GivenTimerTextGo;
        CorrectAnswerTextGo = Lan2SceneStore.CorrectAnswerTextGo;
        ReadyGivenTimerGo = Lan2SceneStore.ReadyGivenTimerGo;
        QuestionNumberTextGo = Lan2SceneStore.QuestionNumberTextGo;
        PickUpButton = Lan2SceneStore.PickUpButton;
        TimerPanelParent = Lan2SceneStore.TimerPanelParent;
        GivenPanelParent = Lan2SceneStore.GivenPanelParent;
        CorrectAnswerPanelParent = Lan2SceneStore.CorrectAnswerPanelParent;
    }

    public void GetAllArithmeticSpawn()
    {
        S2GoStoreScript Lan2SceneStore = GameObject.Find("S2GoStorage").GetComponent<S2GoStoreScript>();
        for (int i = 0; i < Lan2SceneStore.PickupPointsGo.Length; i++)
        {
            PickupPointsGo.Add(Lan2SceneStore.PickupPointsGo[i]);
        }
    }

    public void GetAllPowerUps()
    {
        NetworkStorage NetworkStore = GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>();
        PowerUpsGo.Add(NetworkStore.PowerUpResetCd);
        PowerUpsGo.Add(NetworkStore.PowerUpSpeed);
        PowerUpsGo.Add(NetworkStore.PowerUpUlti);
    }
    #endregion


    #region Game Flow
    public void StartGame()
    {
        if (isServer)
        {
            InitDefault();
            CmdSetUi(true);
            CmdStartCoroutine();
            CmdSpawnArith();
        }
    }

    //Initialize default OnStart
    public void InitDefault()
    {
        //Disable UI's
        SetActiveUi(false);
        CorrectAnswerTextGo.GetComponent<TextMeshProUGUI>().text = $"Correct Answer: {playerCorrectAnswer}/3";
        CmdSetWinner(false);
        CmdSetGiven("Generating...");
        //Set Default values
        //
    }

    [Command(requiresAuthority = false)]
    public void CmdSetWinner(bool status)
    {
        isWinner = status;
    }

    IEnumerator StartTimerGiven()
    {
        while (!isWinner)
        {
            if (isWinner)
            {
                if(playerCorrectAnswer != 3)
                {
                    //Display shit
                }
                if(playerCorrectAnswer == 3)
                {
                    //Display shit again
                }
                StopAllCoroutines();

            }
            CmdIncTimer();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator BackToLobby()
    {
        yield return new WaitForSeconds(3f);
        CmdBackToLobby();
        StopAllCoroutines();
    }

    [Command(requiresAuthority = false)]
    public void CmdBackToLobby()
    {
        if (isServer)
        {
            GameObject.Find("NetworkManager").GetComponent<MasterLanScript>().ChangeServerScene("MasterLanLobby");
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdIncTimer()
    {
        GivenTimerTextInt++;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetUi(bool status)
    {
        RpcSetUi(status);
    }

    [ClientRpc(includeOwner = false)]
    public void RpcSetUi(bool status)
    {
        SetActiveUi(status);
        if (true)
        {
            GivenTimerTextGo.GetComponent<TextMeshProUGUI>().text = GivenTimerTextInt.ToString();
        }
    }

    public void GenerateGiven()
    {
        given = new Dictionary<int, string>
        {
            { 7, "6 + 6 - (2 * 5)  / 2" }, //"6 + 6 - (2 * 5)  / 2"
            { 8, "1 - 2 * (2 * 2) + 1" },
            { 20, "5 + 5 - (5 * 5) + 5" },
            { 16, "1 * 2 * 3 + (1 * 10)" },
            { 9, "((10 / 2) * (8 / 4) + 5" },
            { 2, "1 - 6 + 5 / (2 + 3)" },
            { 5, "(9/3) * 3 + 1 - 5" }
        };
        int[] keys = { 7, 8, 20, 16, 9, 2, 5 };
        int randomRef = Random.Range(0, keys.Length);
        // int randomRef = 0;
        // KeyTotalAnswer = keys[0];
        string tempGiven = given[keys[randomRef]];
        string toPassGiven = "";
        int del = Random.Range(0, 4);
        int arithFunc = 0;
        for (int i = 0; i < tempGiven.Length; i++)
        {
            if (tempGiven[i].ToString().Equals("/") || tempGiven[i].ToString().Equals("*") || tempGiven[i].ToString().Equals("+") || tempGiven[i].ToString().Equals("-"))
            {
                if (arithFunc == del)
                {
                    CmdSetCorrectAnswer(tempGiven[i].ToString());
                    toPassGiven += "_";
                }
                else
                {
                    toPassGiven += tempGiven[i];
                }
                arithFunc++;
            }
            else
            {
                toPassGiven += tempGiven[i];
            }
        }
        CmdSetGiven(toPassGiven + " = " + keys[randomRef]);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetGiven(string answer)
    {
        s2GivenTextStr = answer;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetCorrectAnswer(string answer)
    {
        s2CorrectAnswerStr = answer;
    }


    [Command(requiresAuthority = false)]
    public void CmdDecReadyTimer()
    {
        s2ReadyTimerInt--;
    }

    IEnumerator DecreaseTimer()
    {
        Debug.Log("IENumerator Decrease timer ----");
        ReadyGivenTimerGo.SetActive(true);
        while (s2ReadyTimerInt > 0)
        {
            if (s2ReadyTimerInt == 1)
            {
                ReadyGivenTimerGo.SetActive(false);
                GenerateGiven();
                StopAllCoroutines();
                StartCoroutine(StartTimerGiven());
            }
            CmdDecReadyTimer();
            yield return new WaitForSeconds(1f);
        }
    }

    [Command(requiresAuthority = false)]
    void CmdStartCoroutine()
    {
        if (isServer)
        {
            StartCoroutine(DecreaseTimer());
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnArith()
    {
        if (isServer)
        {
            for (int i = 0; i < 17; i++)
            {
                GameObject pos = PickupPointsGo[Random.Range(0, PickupPointsGo.Count)];
                PickupPointsGo.Remove(pos);
                GameObject arit = Instantiate(arithPrefab, pos.transform.position, Quaternion.identity);
                NetworkServer.Spawn(arit);
            }
        }
    }


    #endregion

    #region Game Methods
    public void SetActiveUi(bool status)
    {
        GivenTextGo.SetActive(status);
        GivenTimerTextGo.SetActive(status);
        CorrectAnswerTextGo.SetActive(status);
        ReadyGivenTimerGo.SetActive(status);
        QuestionNumberTextGo.SetActive(status);
        PickUpButton.SetActive(false);
        GivenPanelParent.SetActive(status);
        CorrectAnswerPanelParent.SetActive(status);
        TimerPanelParent.SetActive(status);
    }


    #endregion

    #region SyncVar Reset Default
    [Command(requiresAuthority = false)]
    public void CmdResetSyncVar()
    {
        s2GivenTextStr = "";
        s2CorrectAnswerStr = "";
    }
    #endregion

    #region SyncVar Hooks
    public void s2ReadyTimerIntHook(int oldValue, int newValue)
    {
        s2ReadyTimerInt = newValue;
        ReadyGivenTimerGo.GetComponent<TextMeshProUGUI>().text = s2ReadyTimerInt.ToString();
        if (s2ReadyTimerInt == 0)
        {
            ReadyGivenTimerGo.SetActive(false);
        }
    }

    public void s2GivenTextStrHook(string oldValue, string newValue)
    {
        s2GivenTextStr = newValue;
        if(playerCorrectAnswer >= 3)
        {
            GivenTextGo.GetComponent<TextMeshProUGUI>().text = "Run to the finish line!";
        }
        else
        {
            GivenTextGo.GetComponent<TextMeshProUGUI>().text = newValue;
        }
    }

    public void s2CorrectAnswerStrHook(string oldValue, string newValue)
    {
        s2CorrectAnswerStr = newValue;
    }

    public void GivenTimerTextIntHook(int oldValue, int newValue)
    {
        GivenTimerTextInt = newValue;
        GivenTimerTextGo.GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }
    #endregion
}
