using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LanSkillAnimation : NetworkBehaviour
{
    [SerializeField] public GameObject[] skillsEffect;
    [SerializeField] public GameObject skillSpawnPoint;

    public void SpawnEffect(int pos, bool status)
    {
        CmdSpawnParticle(pos, status);
    }


    [Command(requiresAuthority = false)]
    public void CmdSpawnParticle(int pos, bool status)
    {
        RpcSpawnParticle(gameObject, pos, status);
    }

    [ClientRpc]
    public void RpcSpawnParticle(GameObject play, int pos, bool status)
    {
        play.GetComponent<LanSkillAnimation>().skillsEffect[pos].SetActive(status);
        if (status)
        {
            play.GetComponent<LanSkillAnimation>().StartCoroutine(StopParticle(pos));
        }
    }

    IEnumerator StopParticle(int pos)
    {
        yield return new WaitForSeconds(3f);
        CmdSpawnParticle(pos, false);
    }

}
