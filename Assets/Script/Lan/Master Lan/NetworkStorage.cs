using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class NetworkStorage : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject MapNameTxt;
    [SerializeField] GameObject PlayerReadyTxt;
    [SerializeField] GameObject PlayerTotalTxt;
    [SerializeField] GameObject PlayerReadyButton;

    [Header("Prefabs")]
    [SerializeField] public GameObject PowerUpUlti;
    [SerializeField] public GameObject PowerUpSpeed;
    [SerializeField] public GameObject PowerUpResetCd;


    [Header("Variables")]
    private string[] mapNames = {"Stage1", "Stage2", "Stage3"};
    
    [SyncVar]
    public int mapNameInt = 0;

    public SyncList<GameObject> playerLan = new SyncList<GameObject>();

    [SyncVar(hook =nameof(MapNameHook))]
    public string MapName = "Stage1";

    [SyncVar(hook = nameof(PlayerReadyHook))]
    public int PlayerReady = 0;

    [SyncVar(hook = nameof(PlayerTotalHook))]
    public int PlayerTotal = 0;

    [SyncVar(hook =nameof(PositionPostHook))]
    public int PositionPos = 0;

    public void PositionPostHook(int oldValue, int newValue)
    {
        PositionPos = newValue;
        if (GameObject.Find("PlayerPositionHandler"))
        {
            GameObject.Find("PlayerPositionHandler").GetComponent<PositionHandler>().pos = newValue;
        }
    }

    public void PlayerTotalHook(int oldValue, int newValue)
    {
        PlayerTotal = newValue;
        PlayerTotalTxt.GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }

    public void PlayerReadyHook(int oldValue, int newValue)
    {
        PlayerReady = newValue;
        PlayerReadyTxt.GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }

    public void MapNameHook(string oldValue, string newValue)
    {
        MapName = newValue;
        MapNameTxt.GetComponent<TextMeshProUGUI>().text = newValue;
    }


    private void Start()
    {
        playerLan = new SyncList<GameObject>();
        DontDestroyOnLoad(this);
    }

    public void RpcHandleReady()
    {
        if (PlayerReadyButton.GetComponent<TextMeshProUGUI>().text.Equals("Ready"))
        {
            PlayerReadyButton.GetComponent<TextMeshProUGUI>().text = "Unready";
            CmdAddReady();
        }
        else if(PlayerReadyButton.GetComponent<TextMeshProUGUI>().text.Equals("Unready"))
        {
            PlayerReadyButton.GetComponent<TextMeshProUGUI>().text = "Ready";
            CmdSubReady();
        }
    }

    [Command(requiresAuthority =false)]
    private void CmdAddReady()
    {
        PlayerReady++;
    }

    [Command(requiresAuthority = false)]
    private void CmdSubReady()
    {
        PlayerReady--;
    }

    public void CmdChangeMap()
    {
        MapName = mapNames[mapNameInt];
        mapNameInt++;
        if(mapNameInt >= 3)
        {
            mapNameInt = 0;
        }
    }


}
