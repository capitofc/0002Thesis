using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using CMF;

public class LanBlackHole : NetworkBehaviour
{
    [SerializeField] GameObject deadSpawnPoint;
    private void OnTriggerEnter(Collider collision)
    {
        LanStage1Handler LanStage1 = GameObject.Find("Stage1Handler").GetComponent<LanStage1Handler>();
        CmdCheckCheckStarting(LanStage1.IsGameStarting);
        if (LanStage1.IsGameStarting)
        {
            CmdCheckCollision(collision.gameObject.tag);
            if (collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Zilch"))
            {
                CmdCheckFall();
                PlayerLanExtension ple = collision.gameObject.GetComponent<PlayerLanExtension>();
                ple.CmdSendImDead();
                //Display disconnection
                ple.SpectatePlayer();
                collision.gameObject.transform.position = deadSpawnPoint.transform.position;
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdCheckFall()
    {
        Debug.Log("We have a hit");
    }

    [Command(requiresAuthority = false)]
    void CmdCheckCheckStarting(bool status)
    {
        Debug.Log($"Game is starting : {status}");
    }

    [Command(requiresAuthority = false)]
    void CmdCheckCollision(string name)
    {
        Debug.Log($"Collided to {name}");
    }


}
