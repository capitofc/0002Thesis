using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;
    [SerializeField]
    GameObject skills;
    public string penguinType;

    void OnEnable()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        StartCoroutine(returnPos());
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject;

            if (penguinType == "Trix")
            {
                skills.GetComponent<Skills>().stun(target);
            }

            else if (penguinType == "Maze")
            {
                skills.GetComponent<Skills>().slow(target);
            }

            else if (penguinType == "Zilch")
            {
                skills.GetComponent<Skills>().push(target);
            }

            Debug.Log(target.tag);
            gameObject.SetActive(false);
        }

    }

    public void setPenguinType(string type) //sets the string type to player's tag
    {
        penguinType = type;
    }

    public void setRotPos(GameObject rota, GameObject spawner)
    {
        gameObject.transform.rotation = rota.transform.rotation;
        gameObject.transform.position = spawner.transform.position;
    }

    IEnumerator returnPos() //return snowball to orig position of no player has been hit
    {
        Debug.Log(penguinType);
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

}
