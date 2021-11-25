using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class Skills : MonoBehaviour
{
    // Start is called before the first frame update
    public float skillDuration;
    Rigidbody rb;
    AdvancedWalkerController simp;
    [SerializeField] GameObject[] particles;



    //first skills
    public float stunSlowDuration()
    {
        skillDuration = 3f;
        return skillDuration;
    }

    public float flashDuration()
    {
        skillDuration = 1.25f;
        return skillDuration;
    }

    public void slow(GameObject target)
    {
        Animator anim = target.GetComponent<Animator>();
        simp = target.GetComponent<AdvancedWalkerController>();
        SoundManager sound = target.GetComponent<SoundManager>();
        Skills skillFX = target.GetComponentInChildren<Skills>();

        //float pitch = sound.adSrc.pitch;
        float speed = simp.getMovementSpeed();
        float jumpSpeed = simp.getJumpSpeed();
        sound.adSrc.pitch = .2f;
        simp.setMovementSpeed(1f);
        simp.setJumpSpeed(1f);

        float normSpeed = anim.speed;
        anim.speed = .2f;
        skillFX.particles[4].SetActive(true);

        StartCoroutine(skillTime(stunSlowDuration(), target, 3, normSpeed));
        StartCoroutine(disableParticle(4, stunSlowDuration(), skillFX));

    }

    public void stun(GameObject target)
    {
        simp = target.GetComponent<AdvancedWalkerController>();
        TuxAnimations anim = target.GetComponent<TuxAnimations>();
        SoundManager sound = target.GetComponent<SoundManager>();
        Skills skillFX = target.GetComponentInChildren<Skills>();

        sound.adSrc.pitch = .2f;
        anim.playStun();
        float speed = simp.getMovementSpeed();
        skillFX.particles[2].SetActive(true);
        simp.setMovementSpeed(0f);


        StartCoroutine(skillTime(stunSlowDuration(), target, 1, 0));
        StartCoroutine(disableParticle(2, 1f, skillFX));
    }

    public void dash(GameObject target)
    {
        TuxAnimations anim = target.GetComponent<TuxAnimations>();
        Skills skillFX = target.GetComponentInChildren<Skills>();

        rb = target.GetComponent<Rigidbody>();
        Vector3 moveDirection = transform.InverseTransformDirection(rb.velocity);
        float dashForce = 600f;
        anim.playDash();
        skillFX.particles[0].SetActive(true);
        rb.AddForce(moveDirection * dashForce, ForceMode.Impulse);
        StartCoroutine(disableParticle(0, 1f, skillFX));
    }

    public void flash(GameObject target)
    {
        simp = target.GetComponent<AdvancedWalkerController>();
        Animator anim = target.GetComponent<Animator>();
        Skills skillFX = target.GetComponentInChildren<Skills>();
        float normSpeed = anim.speed;
        float speed = simp.getMovementSpeed();
        simp.setMovementSpeed(25f);
        anim.speed = 3.5f;
        skillFX.particles[3].SetActive(true);


        StartCoroutine(skillTime(flashDuration(), target, 4, normSpeed));
        StartCoroutine(disableParticle(3, flashDuration() + .3f, skillFX));
    }

    public void flyPlayer(GameObject target) //parameter must be the player
    {
        Mover mover = target.GetComponent<Mover>();
        TuxAnimations anim = target.GetComponent<TuxAnimations>();
        simp = target.GetComponent<AdvancedWalkerController>();
        rb = target.GetComponent<Rigidbody>();
        Skills skillFX = target.GetComponentInChildren<Skills>();

        if (simp.IsGrounded())
        {
            simp.setJumpSpeed(15f);
            simp.jump();
            skillFX.particles[1].SetActive(true);
        }

        simp.setJumpSpeed(15);
        simp.jumpNow();

        skillFX.particles[1].SetActive(true);
        anim.playFly();

        StartCoroutine(skillTime(2f, target, 2, 0));
        StartCoroutine(disableParticle(1, 2.5f, skillFX));
    }

    public void psychosis(GameObject target)
    {
        Camera cam = target.GetComponent<TuxAnimations>().cam;
        StartCoroutine(startPsychosis(cam));
    }

    IEnumerator skillTime(float time, GameObject target, int choice, float animSpeed)
    {
        yield return new WaitForSeconds(time);

        if (choice == 1)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            target.GetComponent<SoundManager>().adSrc.pitch = 1f;
        }


        else if (choice == 2)
        {
            //target.GetComponent<AdvancedWalkerController>().setGravity(47f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
        }

        else if (choice == 3)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            target.GetComponent<SoundManager>().adSrc.pitch = 1f;
            Animator anim = target.GetComponent<Animator>();
            anim.speed = animSpeed;
        }

        else if (choice == 4)
        {
            target.GetComponent<AdvancedWalkerController>().setMovementSpeed(7f);
            target.GetComponent<AdvancedWalkerController>().setJumpSpeed(6f);
            Animator anim = target.GetComponent<Animator>();
            anim.speed = animSpeed;
        }
    }

    IEnumerator startPsychosis(Camera cam)
    {
        StartCoroutine(rotate(cam));
        yield return new WaitForSeconds(3f);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        StopAllCoroutines();
    }

    IEnumerator rotate(Camera cam)
    {
        float rotation = 180f;
        while (true)
        {
            rotation += 5;
            cam.transform.rotation = Quaternion.Euler(new Vector3(rotation, rotation, rotation)); // rotate on y axis
            yield return new WaitForSeconds(0.001f);
        }

    }

    IEnumerator disableParticle(int i, float time, Skills skillFX)
    {
        yield return new WaitForSeconds(time);

        if (skillFX.particles[i].GetComponent<DemoReactivator>() != null)
            skillFX.particles[i].GetComponent<DemoReactivator>().CancelInvoke();

        skillFX.particles[i].gameObject.SetActive(false);
    }
}
