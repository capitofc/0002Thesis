using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class LanThrower : NetworkBehaviour
{
    [SerializeField] GameObject snowBall;
    [SerializeField] public GameObject player;

    private void Start()
    {
        player = gameObject;
    }

    public void Fire()
    {
        CmdFire();
    }

    private void Update()
    {
        try
        {
            if (player.GetComponent<PlayerLanExtension>().isLocalPlayer)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Fire();
                }
            }
        }
        catch (Exception e) { }
    }

    [Command(requiresAuthority = false)]
    void CmdFire()
    {
        GameObject snow = Instantiate(snowBall, new Vector3(player.GetComponent<PlayerLanExtension>().ballSpointPoint.transform.position.x, player.GetComponent<PlayerLanExtension>().ballSpointPoint.transform.position.y, player.GetComponent<PlayerLanExtension>().ballSpointPoint.transform.position.z), player.GetComponent<PlayerLanExtension>().ballSpointPoint.transform.rotation);
        NetworkServer.Spawn(snow, player.GetComponent<NetworkIdentity>().connectionToClient);
        //RpcFire(player.GetComponent<NetworkIdentity>().connectionToClient);
    }
}
