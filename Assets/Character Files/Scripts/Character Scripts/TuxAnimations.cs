using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class TuxAnimations : MonoBehaviour
{
    Mover mover;
    AdvancedWalkerController controller;
    public Camera cam;
    Animator anim;
    SoundManager sm;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<AdvancedWalkerController>();
        anim = GetComponentInChildren<Animator>();
        sm = gameObject.GetComponent<SoundManager>();
        mover = gameObject.GetComponent<Mover>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = gameObject.GetComponent<AdvancedWalkerController>().GetMovementVelocity().normalized;

        if (Input.GetKeyDown(KeyCode.Space))
            jump();

        bool isMoving = false;

        anim.SetBool("inAir", !mover.IsGrounded());

        if (mover.IsGrounded().Equals(true))
        {
            anim.SetBool("Grounded", mover.IsGrounded());
            if (direction != Vector3.zero)
            {
                isMoving = true;
                if (!sm.adSrc.isPlaying && mover.IsGrounded())
                    sm.PlayMusic(0);
            }

            else
            {
                isMoving = false;
            }

            anim.SetBool("isMoving", isMoving);
            anim.SetBool("inAir", !mover.IsGrounded());
        }

        anim.SetBool("Grounded", mover.IsGrounded());
    }

    public void playDash()
    {
        anim.Play("Dash");
    }

    public void playStun()
    {
        anim.Play("Stun");
    }

    public void playBump()
    {
        anim.Play("Bump");
        anim.SetBool("isBumped", true);
        if (!mover.IsGrounded())
            anim.SetBool("inAir", true);
    }

    public void playFly()
    {
        anim.Play("Jump");
        anim.SetBool("JumpOnly", false);
    }

    public void jump()
    {
        if (mover.IsGrounded())
        {
            anim.Play("Jump");
            anim.SetBool("JumpOnly", true);

            if (!sm.adSrc.isPlaying)
            {
                sm.adSrc.Stop();
                sm.PlayMusic(1);
            }
        }

    }
}
