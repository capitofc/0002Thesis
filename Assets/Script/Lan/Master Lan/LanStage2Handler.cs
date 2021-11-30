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

    [SerializeField] public GameObject arithPrefab;

    public string playerCurrentAnswer = "";
    public bool canPick = false;

    [SyncVar(hook =nameof(s2ReadyTimerIntHook))]
    public int s2ReadyTimerInt = 4;
    [SyncVar(hook =nameof(s2GivenTextStrHook))]
    public string s2GivenTextStr = "";
    [SyncVar(hook =nameof(s2CorrectAnswerStrHook))]
    public string s2CorrectAnswerStr = "";
    [SyncVar(hook =nameof(GivenTimerTextIntHook))]
    public int GivenTimerTextInt = 0;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(isServer && Input.GetKeyDown(KeyCode.Alpha8))
        {
            CmdSpawnArith();
        }
    }

    #region Environment Initialization
    public void GetAllUiGo()
    {
        S2GoStoreScript Lan2SceneStore = GameObject.Find("S2GoStorage").GetComponent<S2GoStoreScript>();
        GivenTextGo = Lan2SceneStore.GivenTextGo;
        GivenTimerTextGo = Lan2SceneStore.GivenTimerTextGo;
        CorrectAnswerTextGo = Lan2SceneStore.CorrectAnswerTextGo;
        ReadyGivenTimerGo = Lan2SceneStore.ReadyGivenTimerGo;
        QuestionNumberTextGo = Lan2SceneStore.QuestionNumberTextGo;
        PickUpButton = Lan2SceneStore.PickUpButton;
    }

    public void GetAllArithmeticSpawn()
    {
        S2GoStoreScript Lan2SceneStore = GameObject.Find("S2GoStorage").GetComponent<S2GoStoreScript>();
        for(int i = 0; i < Lan2SceneStore.PickupPointsGo.Length; i++)
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
    //Initialize default OnStart
    public void InitDefault()
    {
        //Disable UI's
        SetActiveUi(false);
        //Set Default values
        //
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

    [ClientRpc(includeOwner = false)]
    public void RpcSpawnArith()
    {
        
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
        PickUpButton.SetActive(status);
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
    }

    public void s2GivenTextStrHook(string oldValue, string newValue)
    {
        s2GivenTextStr = newValue;
        GivenTextGo.GetComponent<TextMeshProUGUI>().text = newValue;
    }

    public void s2CorrectAnswerStrHook(string oldValue, string newValue)
    {
        s2CorrectAnswerStr = newValue;
        CorrectAnswerTextGo.GetComponent<TextMeshProUGUI>().text = newValue;
    }

    public void GivenTimerTextIntHook(int oldValue, int newValue)
    {
        GivenTimerTextInt = newValue;
        GivenTimerTextGo.GetComponent<TextMeshProUGUI>().text = newValue.ToString();
    }
    #endregion
}
