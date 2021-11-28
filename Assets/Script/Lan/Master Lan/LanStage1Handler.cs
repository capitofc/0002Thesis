using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class LanStage1Handler : NetworkBehaviour
{
    public static List<string> float_given = new List<string>();
    public static List<string> int_given = new List<string>();
    public static List<string> bool_given = new List<string>();
    public static List<string> string_given = new List<string>();
    public static List<string> char_given = new List<string>();
    
    [SerializeField] GameObject NetworkStorage;
    [SerializeField] public GameObject Player;

    [Header("Stage 1 UI")]
    [SerializeField] GameObject ReadyTimerText;
    [SerializeField] GameObject ToAnswerTimerText;
    [SerializeField] List<GameObject> GivenText;
    [SerializeField] List<GameObject> platforms;

    [SyncVar(hook =nameof(ReadyTimerHook))]
    public int ReadyTimerInt = 4;

    [SyncVar(hook = nameof(ToAnswerTimerHook))]
    public int ToAnswerTimerInt = 11;

    [SyncVar(hook =nameof(GivenTextHookStr))]
    public string GivenTextStr = "";

    [SyncVar(hook = nameof(CorrectAnswerStrHook))]
    public string CorrectAnswerStr = "";

    [SyncVar]
    public int AlivePlayer = 0;

    [SyncVar]
    public int ResetCooldownInt = 5;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GamePreLoad();
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            CmdToLessAlivePlayer();
        }
    }

    public void GamePreLoad()
    {
        if (Player.GetComponent<PlayerLanExtension>().isServer)
        {
            CmdRunDefaults();
        }
    }

    public void GetAllText()
    {
        GameObject TvStage1 = GameObject.Find("TvText");
        for(int i = 0; i < TvStage1.GetComponent<TvTextHandler>().TvText.Length; i++)
        {
            GivenText.Add(TvStage1.GetComponent<TvTextHandler>().TvText[i]);
        }
    }

    public void GetAllPlatforms()
    {
        GameObject TvStage1 = GameObject.Find("TvText");
        for (int i = 0; i < TvStage1.GetComponent<TvTextHandler>().Platforms.Length; i++)
        {
            platforms.Add(TvStage1.GetComponent<TvTextHandler>().Platforms[i]);
        }
    }

    public void ResetAllPlatforms()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].SetActive(true);
        }
    }

    public void InitGiven()
    {
        float_given.Add("The price of my favorite game 'Tux: Coding Penguin', which is $5.25.");
        float_given.Add("A percentage value.");
        int_given.Add("Number of lives in a game.");
        int_given.Add("Pages in a Book");
        bool_given.Add("((True && False) || True)");
        bool_given.Add("!True");
        string_given.Add("Name of a student");
        string_given.Add("Name of a city");
        char_given.Add("Your middle initial");
        char_given.Add("String is composed of what data type of array?");
    }

    public void StartGame()
    {
        //Start Counter for ReadyTimerText
        if (Player.GetComponent<PlayerLanExtension>().isServer)
        {
            InitGiven();
            Debug.Log("Start Game Function");
            StartCoroutine(StartReadyTimer());
        }
    }

    public void RestartGame()
    {
        GamePreLoad();
    }

    public void GoToLobby()
    {
        
    }

    IEnumerator StartReadyTimer()
    {
        RpcShowReadyTimerText(true);
        while(ReadyTimerInt > 0)
        {
            if(ReadyTimerInt == 1)
            {
                StopAllCoroutines();
                RpcShowReadyTimerText(false);
                //Start Timer To answer and generate given there
                StartCoroutine(StartToAnswerTimer());
            }
            CmdDecReadyTimerInt();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator StartToAnswerTimer()
    {
        RpcShowToAnswerText(true);
        CmdChangeGiven();
        while(ToAnswerTimerInt > 0)
        {
            if(ToAnswerTimerInt == 1)
            {
                StopAllCoroutines();
                RpcShowToAnswerText(false);
                RpcSetWrongAnswer();
                CmdResetTimersDefault();
                //Cooldown for StartReadyTimer
                StartCoroutine(StartCooldownTimer());
            }
            CmdDecToAnswerTimerInt();
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator StartCooldownTimer()
    {
        while (ResetCooldownInt > 0)
        {
            if (ResetCooldownInt == 1)
            {
                StopAllCoroutines();
                RpcResetAllPlatforms();
                StartCoroutine(StartReadyTimer());
            }
            CmdDecCooldownInt();
            yield return new WaitForSeconds(1f);
        }
    }


    #region CMD Functions
    [Command(requiresAuthority = false)]
    public void CmdDecCooldownInt()
    {
        ResetCooldownInt--;
    }

    [Command(requiresAuthority = false)]
    public void CmdAddCooldownToReset()
    {
        ResetCooldownInt++;
    }

    [Command(requiresAuthority =false)]
    public void CmdResetTimersDefault()
    {
        ReadyTimerInt = 4;
        ToAnswerTimerInt = 11;
        ResetCooldownInt = 5;
    }

    [Command(requiresAuthority = false)]
    public void CmdDecReadyTimerInt()
    {
        ReadyTimerInt--;
    }

    [Command(requiresAuthority = false)]
    public void CmdDecToAnswerTimerInt()
    {
        ToAnswerTimerInt--;
    }

    [Command(requiresAuthority = false)]
    public void CmdToAddAlivePlayer()
    {
        AlivePlayer++;
    }

    [Command(requiresAuthority = false)]
    public void CmdToLessAlivePlayer()
    {
        AlivePlayer--;
    }

    [Command(requiresAuthority = false)]
    public void ChangeSceneName()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().MapName = "Lobby";
    }


    [Command(requiresAuthority = false)]
    public void CmdChangeGiven()
    {
        //FIBSC
        int typeGiven = Random.Range(1, 5);
        switch (typeGiven)
        {
            case 1:
                int floatGivenIndex = Random.Range(0, float_given.Count);
                GivenTextStr = float_given[floatGivenIndex];
                CorrectAnswerStr = "float";
                break;
            case 2:
                int intGivenIndex = Random.Range(0, int_given.Count);
                GivenTextStr = int_given[intGivenIndex];
                CorrectAnswerStr = "int";
                break;
            case 3:
                int boolGivenIndex = Random.Range(0, bool_given.Count);
                GivenTextStr = bool_given[boolGivenIndex];
                CorrectAnswerStr = "bool";
                break;
            case 4:
                int stringGivenIndex = Random.Range(0, string_given.Count);
                GivenTextStr = string_given[stringGivenIndex];
                CorrectAnswerStr = "string";
                break;
            case 5:
                int charGivenIndex = Random.Range(0, char_given.Count);
                GivenTextStr = char_given[charGivenIndex];
                CorrectAnswerStr = "char";
                break;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdRunDefaults()
    {
        RpcRunDefaults();
    }

    #endregion

    #region RPC FUNCTIONS
    
    [ClientRpc]
    public void RpcRunDefaults()
    {
        ReadyTimerInt = 3;
        ToAnswerTimerInt = 10;
        ResetAllPlatforms();
        ReadyTimerText.SetActive(false);
        ToAnswerTimerText.SetActive(false);
    }

    [ClientRpc]
    void RpcShowReadyTimerText(bool status)
    {
        ReadyTimerText.GetComponent<TextMeshProUGUI>().text = ReadyTimerInt.ToString();
        ReadyTimerText.SetActive(status);
    }

    [ClientRpc]
    void RpcShowToAnswerText(bool status)
    {
        ToAnswerTimerText.SetActive(status);
    }

    [ClientRpc]
    void RpcSetWrongAnswer()
    {
        Debug.Log("I am here");
        for(int i = 0; i < platforms.Count; i++)
        {
            Debug.Log($"Platform - {platforms[i].GetComponent<PlatformScript>().GetValue().Equals(CorrectAnswerStr)}");
            if (!platforms[i].GetComponent<PlatformScript>().GetValue().Equals(CorrectAnswerStr))
            {
                platforms[i].SetActive(false);
            }
        }
    }

    [ClientRpc]
    void RpcResetAllPlatforms()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].SetActive(true);
        }
    }


    #endregion

    #region SYNC VAR HOOKS
    void ToAnswerTimerHook(int oldValue, int newValue)
    {
        ToAnswerTimerInt = newValue;
        ToAnswerTimerText.GetComponent<TextMeshProUGUI>().text = ToAnswerTimerInt.ToString();
    }

    void ReadyTimerHook(int oldValue, int newValue)
    {
        ReadyTimerInt = newValue;
        ReadyTimerText.GetComponent<TextMeshProUGUI>().text = ReadyTimerInt.ToString();
    }

    void GivenTextHookStr(string oldValue, string newValue)
    {
        GivenTextStr = newValue;
        for(int i = 0; i < GivenText.Count; i++)
        {
            GivenText[i].GetComponent<TextMeshPro>().text = GivenTextStr;
        }
    }

    void CorrectAnswerStrHook(string oldValue, string newValue)
    {
        CorrectAnswerStr = newValue;
    }

    #endregion
}
