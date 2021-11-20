using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class TuxAnimations : MonoBehaviour
{
    Mover mover;
    AdvancedWalkerController controller;
    Animator anim;
    SoundManager sm;
    bool isCharJumping;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<AdvancedWalkerController>();
        anim = GetComponentInChildren<Animator>();
        sm = gameObject.GetComponent<SoundManager>();
        mover = gameObject.GetComponent<Mover>();
        isCharJumping = false;
        //audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
            jump();

        bool isMoving = false;

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
    }

    public void playDash()
    {
        anim.Play("Dash");
        anim.SetBool("Grounded", true);
    }

    public void playStun()
    {
        anim.Play("Stun");
        anim.SetBool("Grounded", true);
    }

    public void jump()
    {
        anim.Play("Jump");
        anim.SetBool("Grounded", true);
        isCharJumping = true;

        if (!sm.adSrc.isPlaying)
        {
            sm.adSrc.Stop();
            sm.PlayMusic(1);
        }
    }
}
