using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using CMF;

public class PlayerLanExtension : NetworkBehaviour
{
    [Header("Player Cam")]
    [SerializeField] GameObject playerCam;

    Animator anim;
    Mover mover;
    private void Start()
    {
        if (isLocalPlayer)
        {
            GameObject buttonHandler = GameObject.Find("Button Handler");
            buttonHandler.GetComponent<ButtonsHandler>().player = gameObject;
            buttonHandler.GetComponent<ButtonsHandler>().cast = gameObject.GetComponent<SkillControls>();
            playerCam.SetActive(true);
            // anim = GetComponentInChildren<Animator>();
            anim = GetComponent<Animator>();
            mover = GetComponent<Mover>();
        }
    }

    // [Command]
    // public void CmdSetAir()
    // {
    //     anim.SetBool("inAir", !mover.IsGrounded());
    // }

    // [Command]
    // public void CmdGrounded()
    // {
    //     anim.SetBool("Grounded", mover.IsGrounded());
    // }

    // [Command]
    // public void CmdMoving(bool isMoving)
    // {
    //     anim.SetBool("isMoving", isMoving);
    // }

    // [TargetRpc]
    // public void RpcSetAir()
    // {
    //     anim.SetBool("inAir", !mover.IsGrounded());
    // }

    // [TargetRpc]
    // public void RpcGrounded()
    // {
    //     anim.SetBool("Grounded", mover.IsGrounded());
    // }

    // [TargetRpc]
    // public void RpcMoving(bool isMoving)
    // {
    //     anim.SetBool("isMoving", isMoving);
    // }

}
