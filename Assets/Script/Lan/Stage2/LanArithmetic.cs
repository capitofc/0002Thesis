using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LanArithmetic : NetworkBehaviour
{
    [SerializeField] GameObject[] ArithmeticPrefabs;
    List<GameObject> arits = new List<GameObject>();
    float y = 0;
    int ran = 0;

    private void Start()
    {
        y = gameObject.transform.position.y;
        for(int i = 0; i < ArithmeticPrefabs.Length; i++)
        {
            GameObject obj = Instantiate(ArithmeticPrefabs[i], gameObject.transform);
            obj.GetComponent<LanArithOperation>().parent = gameObject;
            arits.Add(obj);
            //NetworkServer.Spawn(obj);
        }
        ran = Random.Range(0, ArithmeticPrefabs.Length);
        CmdChangeArith(ran);
    }

    IEnumerator SetActiveArith()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        int rand = Random.Range(0, ArithmeticPrefabs.Length);
        CmdChangeArith(rand);
    }

    [Command(requiresAuthority = false)]
    void CmdChangeArith(int rand)
    {
        RpcChangeArith(rand);
    }

    [ClientRpc(includeOwner = false)]
    void RpcChangeArith(int rand)
    {
        StopAllCoroutines();
        for (int i = 0; i < arits.Count; i++)
        {
            if (i == rand)
            {
                arits[i].SetActive(true);
            }
            else
            {
                arits[i].SetActive(false);
            }
        }
        StartCoroutine(SetActiveArith());
    }

    [Command(requiresAuthority = false)]
    public void CmdDestroy()
    {
        RpcDestroy();
    }

    [ClientRpc(includeOwner =false)]
    public void RpcDestroy()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1000f, gameObject.transform.position.z);
        Invoke(nameof(CmdReplace), Random.Range(8f, 14f));
    }

    IEnumerator SetActiveParent()
    {
        yield return new WaitForSeconds(Random.Range(8f, 14f));
        CmdReplace();
        StopAllCoroutines();
    }

    [Command(requiresAuthority = false)]
    public void CmdReplace()
    {
        RpcReplace();
    }

    [ClientRpc(includeOwner = false)]
    public void RpcReplace()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);
    }

}
