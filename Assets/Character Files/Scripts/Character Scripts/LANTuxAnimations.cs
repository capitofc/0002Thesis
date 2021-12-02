using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class LANTuxAnimations : MonoBehaviour
{
    Mover mover;
    AdvancedWalkerController controller;
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
        if (GetComponent<PlayerLanExtension>().isLocalPlayer && GetComponent<PlayerLanExtension>().isClient)
        {
            Vector3 direction = gameObject.GetComponent<AdvancedWalkerController>().GetMovementVelocity().normalized;

            if (Input.GetKeyDown(KeyCode.Space))
                jump();

            bool isMoving = false;
            // GetComponent<PlayerLanExtension>().CmdGrounded();
            anim.SetBool("inAir", !mover.IsGrounded());

            if (mover.IsGrounded().Equals(true))
            {
                // GetComponent<PlayerLanExtension>().CmdGrounded();
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

                // GetComponent<PlayerLanExtension>().CmdMoving(isMoving);
                anim.SetBool("isMoving", isMoving);
                // GetComponent<PlayerLanExtension>().CmdSetAir();
                anim.SetBool("inAir", !mover.IsGrounded());
            }
            // GetComponent<PlayerLanExtension>().CmdGrounded();
            anim.SetBool("Grounded", mover.IsGrounded());
        }
        else if (GetComponent<PlayerLanExtension>().isLocalPlayer)
        {
            Debug.Log("I am here");
            Vector3 direction = gameObject.GetComponent<AdvancedWalkerController>().GetMovementVelocity().normalized;

            if (Input.GetKeyDown(KeyCode.Space))
                jump();

            bool isMoving = false;

            // GetComponent<PlayerLanExtension>().RpcGrounded();
            anim.SetBool("inAir", !mover.IsGrounded());

            if (mover.IsGrounded().Equals(true))
            {
                // GetComponent<PlayerLanExtension>().RpcGrounded();
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

                // GetComponent<PlayerLanExtension>().RpcMoving(isMoving);
                anim.SetBool("isMoving", isMoving);
                // GetComponent<PlayerLanExtension>().RpcSetAir();
                anim.SetBool("inAir", !mover.IsGrounded());
            }
            // GetComponent<PlayerLanExtension>().RpcGrounded();
            anim.SetBool("Grounded", mover.IsGrounded());
        }
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
    public void pickUp()
    {
        if (mover.IsGrounded())
        {
            anim.Play("Pick");
        }
    }
}
