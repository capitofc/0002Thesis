using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControls : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    string penguinType;
    bool isCooldown = false;
    float skillCooldown = 5f;
    [SerializeField] Skills skill;

    bool isUltiPoint = false;



    Text firstSkillBtnText;
    Button firstSkillButton;

    Text ultiSkillBtnText;
    Button ultiSkillButton;

    void Awake()
    {
        penguinType = gameObject.tag;
        loadButtons();

    }

    void loadButtons()
    {
        firstSkillButton = GameObject.Find("First Skill").GetComponent<Button>();
        firstSkillBtnText = GameObject.Find("First Skill").GetComponentInChildren<Text>();
        ultiSkillButton = GameObject.Find("Ultimate").GetComponent<Button>();
        ultiSkillBtnText = GameObject.Find("Ultimate").GetComponentInChildren<Text>();
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

        if (isUltiPoint == false)
        {
            ultiSkillButton.interactable = false;
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

    public void fly(GameObject target)
    {
        skill.GetComponent<Skills>().flyPlayer(target);
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
            Debug.Log("Skill in cooldown, wait for: " + countdown + " seconds.");


    }

    public void castUltimate()
    {
        if (isUltiPoint == true)
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

            ultiSkillButton.interactable = false;
            setUltiPoint(false);
        }

        else
            Debug.Log("Collect UltiPoint to Cast Ultimate Skill!");

    }

    void startCooldown()
    {
        isCooldown = true;
        firstSkillButton.interactable = false;
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        while (skillCooldown >= 0)
        {
            skillCooldown--;
            if (skillCooldown == 0)
            {
                firstSkillCDDone();
            }
            else if (skillCooldown >= 1)
            {
                updateButtonCDTxt();
                displayCD(skillCooldown);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void firstSkillCDDone()
    {
        StopAllCoroutines();
        isCooldown = false;
        firstSkillBtnText.text = "THROW SNOW BALL!";
        firstSkillButton.interactable = true;
        skillCooldown = 5f;
    }

    private void updateButtonCDTxt()
    {
        firstSkillBtnText.text = skillCooldown.ToString();
    }

    float countdown;
    void displayCD(float val)
    {
        countdown = val;
    }

    public void coolDownPowerUp()
    {
        StopAllCoroutines();
        skillCooldown = 5f;
        isCooldown = false;
        firstSkillBtnText.text = "THROW SNOW BALL!";
        firstSkillButton.interactable = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("CoolDownPowerUp"))
        {
            coolDownPowerUp();
        }
        else if (collision.gameObject.tag.Equals("UltiPoint"))
        {
            isUltiPoint = true;
            ultiSkillButton.interactable = true;
        }
    }

    public void resetCD()
    {
        isCooldown = false;
        firstSkillCDDone();
    }

    public void setUltiPoint(bool val)
    {
        isUltiPoint = val;
    }
    public Skills GetSkills()
    {
        return skill;
    }
}
