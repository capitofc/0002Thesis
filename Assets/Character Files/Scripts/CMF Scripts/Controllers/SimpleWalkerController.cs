using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
    //A very simplified controller script;
    //This script is an example of a very simple walker controller that covers only the basics of character movement;
    public class SimpleWalkerController : Controller
    {
        [SerializeField] GameObject joystick;
        private Mover mover;
        float currentVerticalSpeed = 0f;
        bool isGrounded;
        float movementSpeed;
        float jumpSpeed;
        private float gravity;

        Vector3 lastVelocity = Vector3.zero;

        public Transform cameraTransform;
        CharacterInput characterInput;
        Transform tr;

        Vector3 _velocity;

        Joystick myJs;
        // Use this for initialization
        void Start()
        {
            tr = transform;
            mover = GetComponent<Mover>();
            characterInput = GetComponent<CharacterInput>();
            movementSpeed = 7f;
            gravity = 47f;
            jumpSpeed = 10f;
            myJs = joystick.GetComponent<Joystick>();
        }

        public void setMovementSpeed(float newSpeed)
        {
            movementSpeed = newSpeed;
        }

        public float getMovementSpeed()
        {
            return movementSpeed;
        }

        public void setGravity(float newGravity)
        {
            gravity = newGravity;
        }

        public float getGravity()
        {
            return gravity;
        }

        public void setJumpSpeed(float newJumpSpeed)
        {
            jumpSpeed = newJumpSpeed;
        }

        public float getJumpSpeed()
        {
            return jumpSpeed;
        }

        public Vector3 get_velocity()
        {
            return _velocity;
        }

        void FixedUpdate()
        {
            //Run initial mover ground check;
            mover.CheckForGround();

            //If character was not grounded int the last frame and is now grounded, call 'OnGroundContactRegained' function;
            if (isGrounded == false && mover.IsGrounded() == true)
                OnGroundContactRegained(lastVelocity);

            //Check whether the character is grounded and store result;
            isGrounded = mover.IsGrounded();

            _velocity = Vector3.zero;

            //Add player movement to velocity;
            _velocity += CalculateMovementDirection() * movementSpeed;

            //Handle gravity;
            if (!isGrounded)
            {
                currentVerticalSpeed -= gravity * Time.deltaTime;
            }
            else
            {
                if (currentVerticalSpeed <= 0f)
                    currentVerticalSpeed = 0f;
            }

            //Handle jumping;
            if ((characterInput != null) && isGrounded && characterInput.IsJumpKeyPressed())
            {
                jump();
            }

            jumpnext(_velocity);
        }

        public void jump()
        {
            if ((characterInput != null) && isGrounded)
            {
                OnJumpStart();
                currentVerticalSpeed = jumpSpeed;
                isGrounded = false;
                gameObject.GetComponent<TuxAnimations>().jump();
            }

        }

        public void jumpnext(Vector3 _velocity)
        {
            //Add vertical velocity;
            _velocity += tr.up * currentVerticalSpeed;

            //Save current velocity for next frame;
            lastVelocity = _velocity;

            mover.SetExtendSensorRange(isGrounded);
            mover.SetVelocity(_velocity);

        }

        public Vector3 CalculateMovementDirection()
        {
            //If no character input script is attached to this object, return no input;
            if (characterInput == null)
                return Vector3.zero;

            Vector3 _direction = Vector3.zero;

            //If no camera transform has been assigned, use the character's transform axes to calculate the movement direction;
            if (cameraTransform == null)
            {
                _direction += tr.right * myJs.GetHorizontal();
                _direction += tr.forward * myJs.GetVertical();

                _direction += tr.right * characterInput.GetHorizontalMovementInput();
                _direction += tr.forward * characterInput.GetVerticalMovementInput();
            }
            else
            {
                //If a camera transform has been assigned, use the assigned transform's axes for movement direction;
                //Project movement direction so movement stays parallel to the ground;
                //Joystick
                _direction += Vector3.ProjectOnPlane(cameraTransform.right, tr.up).normalized * myJs.GetHorizontal();
                _direction += Vector3.ProjectOnPlane(cameraTransform.forward, tr.up).normalized * myJs.GetVertical();

                //Keyboard
                _direction += Vector3.ProjectOnPlane(cameraTransform.right, tr.up).normalized * characterInput.GetHorizontalMovementInput();
                _direction += Vector3.ProjectOnPlane(cameraTransform.forward, tr.up).normalized * characterInput.GetVerticalMovementInput();
            }

            //If necessary, clamp movement vector to magnitude of 1f;
            if (_direction.magnitude > 1f)
                _direction.Normalize();

            return _direction;
        }

        //This function is called when the controller has landed on a surface after being in the air;
        void OnGroundContactRegained(Vector3 _collisionVelocity)
        {
            //Call 'OnLand' delegate function;
            if (OnLand != null)
                OnLand(_collisionVelocity);
        }

        //This function is called when the controller has started a jump;
        public void OnJumpStart()
        {
            //Call 'OnJump' delegate function;
            if (OnJump != null)
                OnJump(lastVelocity);
        }

        //Return the current velocity of the character;
        public override Vector3 GetVelocity()
        {
            return lastVelocity;
        }

        //Return only the current movement velocity (without any vertical velocity);
        public override Vector3 GetMovementVelocity()
        {
            return lastVelocity;
        }

        //Return whether the character is currently grounded;
        public override bool IsGrounded()
        {
            return isGrounded;
        }

    }

}

