using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PositionHandler : NetworkBehaviour
{

    [Header("Spawn Points")]
    [SerializeField] public GameObject[] spawnPoint;

    [SerializeField] public GameObject myPlayer;

    public GameObject NetworkStorage;
    public int pos = 0;

    void Start()
    {
        Invoke(nameof(Execute), 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }

    void Execute()
    {
        CmdResetAllPlayersSpawn();
    }

    [Command(requiresAuthority = false)]
    public void CmdResetAllPlayersSpawn()
    {
        RpcResetAllPlayersSpawn();
        //RpcResetAllPlayersSpawn();
    }

    [ClientRpc]
    public void RpcResetAllPlayersSpawn()
    {
        //myPlayer.transform.position = spawnPoint[myPlayer.GetComponent<PlayerLanExtension>().posIndex].transform.position;
        for (int i = 0; i < myPlayer.GetComponent<PlayerLanExtension>().players.Count; i++)
        {
            if (myPlayer.GetComponent<PlayerLanExtension>().players[i].GetComponent<PlayerLanExtension>().isLocalPlayer)
            {
                myPlayer.transform.position = spawnPoint[i].transform.position;
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdRequestPos(GameObject go, int pos)
    {
        RpcRequestPost(go, pos);
    }

    [ClientRpc]
    void RpcRequestPost(GameObject go, int pos)
    {
        if (go.GetComponent<PlayerLanExtension>().isLocalPlayer)
        {
            myPlayer.transform.position = spawnPoint[pos].transform.position;
        }
    }



}
