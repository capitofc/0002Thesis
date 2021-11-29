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
        if (myPlayer.GetComponent<PlayerLanExtension>().isServer)
        {
            CmdResetAllPlayersSpawn();
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdResetAllPlayersSpawn()
    {
        RpcResetAllPlayersSpawn();
    }

    [ClientRpc]
    public void RpcResetAllPlayersSpawn()
    {
        myPlayer.transform.position = spawnPoint[myPlayer.GetComponent<PlayerLanExtension>().posIndex].transform.position;
    }



}
