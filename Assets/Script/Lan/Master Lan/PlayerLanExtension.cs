using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using CMF;

public class PlayerLanExtension : NetworkBehaviour
{
    [Header("Player Cam")]
    [SerializeField] GameObject playerCam;
    SyncList<GameObject> players;
    public int posIndex = -1;

    Animator anim;
    Mover mover;


    private void Start()
    {
        if (isLocalPlayer)
        {
            if (isServer && isClient) 
            {
                Debug.Log("Is Server");
                CmdAddReadyPlayer();
                CmdAddPlayerTotal();
            }
            if (isClient && !isServer)
            {
                Debug.Log("Is Client");
                CmdAddPlayerTotal();
            }
            GameObject buttonHandler = GameObject.Find("Button Handler");
            buttonHandler.GetComponent<ButtonsHandler>().player = gameObject;
            buttonHandler.GetComponent<ButtonsHandler>().cast = gameObject.GetComponent<SkillControls>();
            playerCam.SetActive(true);
            anim = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            CmdAddToPlayerList(gameObject);
            StageLocalPlayerReference();
        }
    }

    void StageLocalPlayerReference()
    {
        if (GameObject.Find("PlayerPositionHandler"))
        {
            GameObject.Find("PlayerPositionHandler").GetComponent<PositionHandler>().myPlayer = gameObject;
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().Player = gameObject;
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().GetAllText();
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().GetAllPlatforms();
            GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>().CmdToAddAlivePlayer();
            posIndex = GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PositionPos;
            CmdAddPositionPos();
        }
    }

    [Command]
    void CmdAddPositionPos()
    {
        RpcAddPositionPos();
    }

    [ClientRpc]
    void RpcAddPositionPos()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PositionPos++;
    }

    [Command]
    void CmdAddPlayerTotal()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerTotal++;
    }

    [Command]
    void CmdAddReadyPlayer()
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().PlayerReady++;
    }

    [Command]
    void CmdAddToPlayerList(GameObject me)
    {
        RpcAddToPlayerList(me);
        Debug.Log("Added player to Storage!");
    }

    [ClientRpc]
    void RpcAddToPlayerList(GameObject me)
    {
        GameObject.Find("NetworkStorage").GetComponent<NetworkStorage>().playerLan.Add(me);
    }


    [ClientRpc]
    public void SetMyPosition(GameObject pos)
    {
        gameObject.transform.position = pos.transform.position;
    }


    [Command]
    public void ResetMyPosition(GameObject pos)
    {
        gameObject.transform.position = pos.transform.position;
    }

}
