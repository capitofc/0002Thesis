using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class Skills : MonoBehaviour
{
    // Start is called before the first frame update
    public float skillDuration;
    public float pushDistance;
    Rigidbody rb;
    SimpleWalkerController simp;


    //first skills
    public float stunSlowDuration()
    {
        skillDuration = 1.5f;
        return skillDuration;
    }

    public float pushDashDistance()
    {
        pushDistance = 50f;
        return pushDistance;
    }

    public float flashDuration()
    {
        skillDuration = .5f;
        return skillDuration;
    }

    public float flyDistance()
    {
        pushDistance = 500f;
        return pushDistance;
    }

    public void slow(GameObject target)
    {
        Debug.Log("slow");
        Animator anim = target.GetComponentInChildren<Animator>();
        simp = target.GetComponent<SimpleWalkerController>();
        SoundManager sound = target.GetComponent<SoundManager>();

        //float pitch = sound.adSrc.pitch;
        float speed = simp.getMovementSpeed();
        float jumpSpeed = simp.getJumpSpeed();
        sound.adSrc.pitch = .2f;
        simp.setMovementSpeed(1f);
        simp.setJumpSpeed(1f);

        float normSpeed = anim.speed;
        anim.speed = .2f;

        StartCoroutine(skillTime(stunSlowDuration(), target, 3, normSpeed));

    }

    public void stun(GameObject target)
    {
        Debug.Log("stun");
        simp = target.GetComponent<SimpleWalkerController>();
        TuxAnimations anim = target.GetComponent<TuxAnimations>();
        SoundManager sound = target.GetComponent<SoundManager>();

        sound.adSrc.pitch = .2f;
        anim.playStun();
        float speed = simp.getMovementSpeed();
        simp.setMovementSpeed(0f);


        StartCoroutine(skillTime(stunSlowDuration(), target, 1, 0));
    }

    public void push(GameObject target)
    {
        Debug.Log("push");
        Rigidbody rb = target.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-25, 0, -80), ForceMode.Impulse);

        target.transform.position += new Vector3(25f * Time.deltaTime, 0, -5f * Time.deltaTime);
    }

    public void dash(GameObject target)
    {
        TuxAnimations anim = target.GetComponent<TuxAnimations>();
        rb = target.GetComponent<Rigidbody>();

        Vector3 moveDirection = transform.InverseTransformDirection(rb.velocity);
        float dashForce = 600f;
        anim.playDash();
        rb.AddForce(moveDirection * dashForce, ForceMode.Impulse);

    }

    public void flash(GameObject target)
    {
        simp = target.GetComponent<SimpleWalkerController>();
        Animator anim = target.GetComponentInChildren<Animator>();
        float normSpeed = anim.speed;
        float speed = simp.getMovementSpeed();
        simp.setMovementSpeed(25f);
        anim.speed = 3.5f;

        StartCoroutine(skillTime(flashDuration(), target, 4, normSpeed));
    }

    public void flyPlayer(GameObject target) //parameter must be the player
    {
        simp = target.GetComponent<SimpleWalkerController>();
        rb = target.GetComponent<Rigidbody>();
        simp.setJumpSpeed(25f);

        StartCoroutine(skillTime(2f, target, 2, 0));
    }

    IEnumerator skillTime(float time, GameObject target, int choice, float animSpeed)
    {
        yield return new WaitForSeconds(time);

        if (choice == 1)
        {
            target.GetComponent<SimpleWalkerController>().setMovementSpeed(7f);
            target.GetComponent<SimpleWalkerController>().setJumpSpeed(10f);
            target.GetComponent<SoundManager>().adSrc.pitch = 1f;
        }


        else if (choice == 2)
        {
            //target.GetComponent<SimpleWalkerController>().setGravity(47f);
            target.GetComponent<SimpleWalkerController>().setJumpSpeed(10f);
        }

        else if (choice == 3)
        {
            target.GetComponent<SimpleWalkerController>().setMovementSpeed(7f);
            target.GetComponent<SimpleWalkerController>().setJumpSpeed(10f);
            target.GetComponent<SoundManager>().adSrc.pitch = 1f;
            Animator anim = target.GetComponentInChildren<Animator>();
            anim.speed = animSpeed;
        }

        else if (choice == 4)
        {
            target.GetComponent<SimpleWalkerController>().setMovementSpeed(7f);
            target.GetComponent<SimpleWalkerController>().setJumpSpeed(10f);
            Animator anim = target.GetComponentInChildren<Animator>();
            anim.speed = animSpeed;
        }
    }
}
