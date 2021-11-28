using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LanThrower : NetworkBehaviour
{
    [SerializeField] GameObject snowBall;
    [SerializeField] public GameObject player;

    public void Fire()
    {
        CmdFire();
    }

    private void Update()
    {
        if (player.GetComponent<PlayerLanExtension>().isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Fire();
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdFire()
    {
        RpcFire(player.GetComponent<NetworkIdentity>().connectionToClient);
    }

    [TargetRpc]
    void RpcFire(NetworkConnection play)
    {
        GameObject snow = Instantiate(snowBall, new Vector3(player.GetComponent<PlayerLanExtension>().ballSpointPoint.transform.position.x, player.GetComponent<PlayerLanExtension>().ballSpointPoint.transform.position.y, player.GetComponent<PlayerLanExtension>().ballSpointPoint.transform.position.z), Quaternion.identity);
        NetworkServer.Spawn(snow);
    }
}
