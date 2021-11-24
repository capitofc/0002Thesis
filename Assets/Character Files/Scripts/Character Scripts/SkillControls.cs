using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillControls : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    string penguinType;
    bool isCooldown = false;
    float skillCooldown = 5f;
    [SerializeField] Skills skill;

    void Start()
    {
        penguinType = gameObject.tag;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            castSkill();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            castUltimate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            skill.GetComponent<Skills>().slow(gameObject);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            skill.GetComponent<Skills>().stun(gameObject);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            skill.GetComponent<Skills>().psychosis(gameObject);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            skill.GetComponent<Skills>().dash(gameObject);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            skill.GetComponent<Skills>().flyPlayer(gameObject);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            skill.GetComponent<Skills>().flash(gameObject);
    }

    public void castSkill()
    {

        if (!isCooldown)
        {
            gameObject.GetComponent<ThrowSnowball>().setPenguinType(gameObject.tag);
            gameObject.GetComponent<ThrowSnowball>().throwSnow();
            startCooldown();
        }

        else
            Debug.Log("Skill in cooldown");


    }

    public void castUltimate()
    {
        if (penguinType == "Trix")
        {
            skill.GetComponent<Skills>().dash(gameObject);
        }

        else if (penguinType == "Maze")
        {
            skill.GetComponent<Skills>().flyPlayer(gameObject);
        }

        else if (penguinType == "Zilch")
        {
            skill.GetComponent<Skills>().flash(gameObject);
        }
    }

    void startCooldown()
    {
        isCooldown = true;
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(skillCooldown);
        isCooldown = false;
    }
}
