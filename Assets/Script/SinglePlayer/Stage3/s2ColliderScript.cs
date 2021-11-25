using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class s2ColliderScript : MonoBehaviour
{
    [Header("Text Holder Game Object")]
    [SerializeField] GameObject TextHolderGameObject;

    [Header("Main Handler")]
    [SerializeField] GameObject Main;

    public float force = 3000f;
    private Vector3 hitDir;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Trix") || collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("Maze") || collision.gameObject.tag.Equals("Zilch"))
        {
            GameObject Stage3Main = GameObject.Find("Stage3MainHandler");
            if (!Stage3Main.GetComponent<Stage3ScriptHandler>().correctAnswer.Equals(TextHolderGameObject.GetComponent<TextHolderScript>().startValue))
            {
                SkillControls sc = collision.gameObject.GetComponent<SkillControls>();
                Skills skill = sc.GetSkills();
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(-transform.forward * force, ForceMode.Acceleration);
                skill.GetComponent<Skills>().stun(collision.gameObject);
            }
            else
            {
                //If correct
                Main.GetComponent<Stage3ScriptHandler>().GenerateGiven();
                GetComponent<BoxCollider>().isTrigger = true;
                Invoke(nameof(resetDefault), 1f);
            }
        }
    }

    public void resetDefault()
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }


}
