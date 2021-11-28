using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using CMF;

public class LanPowerUpsCollider : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Maze") || other.tag.Equals("Trix") || other.tag.Equals("Zilch"))
        {
            if (gameObject.tag.Equals("SpeedUp"))
            {
                other.gameObject.GetComponent<PlayerLanExtension>().CmdSpeedUp();
            }
            else if (gameObject.tag.Equals("CDReset"))
            {
                other.gameObject.GetComponent<PlayerLanExtension>().CmdResetCdSkill();
            }
            else if (gameObject.tag.Equals("UltiPoint"))
            {
                other.gameObject.GetComponent<PlayerLanExtension>().CmdUltiPoint();
            }
        }
        NetworkServer.Destroy(gameObject);
    }

}
