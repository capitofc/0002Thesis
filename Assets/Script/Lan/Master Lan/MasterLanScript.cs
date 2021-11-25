using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;

public class MasterLanScript : NetworkManager
{
    [Header("MASTER LAN SCRIPT")]
    [SerializeField] GameObject SelectedPlayer;

    public override void Start()
    {
        base.Start();
        //Get the current player that is selected by the user. For now lez just do predefine value
        singleton.playerPrefab = SelectedPlayer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetComponent<NetworkDiscoveryHUD>().StartHostLan();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<NetworkDiscoveryHUD>().JoinAsClient();
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }
}
