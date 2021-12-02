using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
    //This character movement input class is an example of how to get input from a keyboard to control the character;
    public class CharacterKeyboardInput : CharacterInput
    {
        public string horizontalInputAxis = "Horizontal";
        public string verticalInputAxis = "Vertical";
        public KeyCode jumpKey = KeyCode.Space;
        bool jump;

        //If this is enabled, Unity's internal input smoothing is bypassed;
        public bool useRawInput = true;

        private void Start()
        {
            background = null;
            handle = null;
        }

        public override float GetHorizontalMovementInput()
        {
            if (useRawInput)
                return Input.GetAxisRaw(horizontalInputAxis);
            else
                return Input.GetAxis(horizontalInputAxis);
        }

        public override float GetVerticalMovementInput()
        {
            if (useRawInput)
                return Input.GetAxisRaw(verticalInputAxis);
            else
                return Input.GetAxis(verticalInputAxis);
        }

        public void jumpButton()
        {
            jump = true;
            GetComponent<TuxAnimations>().jump();
            StartCoroutine(resetJump());

        }

        public override bool IsJumpKeyPressed()
        {
            //uncomment if u want to use keyboard
            // return Input.GetKey(jumpKey);
            //uncomment if u want to use joystick
            return jump;
        }

        IEnumerator resetJump()
        {
            yield return new WaitForSeconds(.2f);
            jump = false;
        }
    }
}
