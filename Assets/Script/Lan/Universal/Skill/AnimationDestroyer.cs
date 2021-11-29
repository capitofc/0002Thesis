using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AnimationDestroyer : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CmdDestroy();
    }

    [Command(requiresAuthority = false)]
    void CmdDestroy()
    {
        DestroyMe();
    }

    IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(4f);
        NetworkServer.Destroy(gameObject);
    }
}
