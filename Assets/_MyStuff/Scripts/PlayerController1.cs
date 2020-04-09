﻿using UnityEngine;
using System.Collections;

public class PlayerController1 : MonoBehaviour
{
    // Set all Layers of the Character to a Layer Called "Character"
    // Set the Sphere to a Layer Called "Ball" or whataver you like
    // in the Physics Manager (Edit>ProjectSettings>Physics) make sure that the "Character" layer is not colldiging witht the "Ball" Layer
    //

    // some things to look at:

    // the Joint and Sphere gameobject inside the character, its got a config. joint - pay attention to its settings and limits
    // this script is placed on the sphere
    // on the head and chest is a constant force up component
    // the sphere got a pretty high mass of 50
    // the sphere and joint got a drag value of 1
    public Rigidbody chestBody;

    public Vector3 inputDirection;
    public CharacterInput input;
    public CharacterFaceDirection faceDirection;
    public CharacterFaceDirection hipFaceDirection;
    public CharacterFaceDirection headFaceDirection;
    protected Vector3 currentFacing = Vector3.zero;



    public float speed;
    public float walkspeed = 140f;
    public float startSpeed = 500f;
    public float jumpForce = 500f;
    private Rigidbody rb;

    public Vector3 target;

    public bool targetting = false;

    public Vector3 targetDirection = Vector3.zero;
    public bool cameraBasedMovement = true;

    public CharacterLegsSimple legs;

    public bool justStarted = false;
    private float moveButtonClickTimer = 0f;
    private float moveRightButtonClickTimer = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<CharacterInput>();
        legs = GetComponent<CharacterLegsSimple>();
    }

    public bool dash;

    public bool enableDrag;
    //public bool jump;
    private void Update()
    {



        /* //HEad But and bending
         if (input.PressFire())
         {
             // print("Just Pressed Key");

             headbuttonClickTimer = 0f;
             leanBack = false;

         }

         else if (input.HoldFire())
         {
             //print("Key Held");
             if (headbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
             {
                 headbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
             }
             if (headbuttonClickTimer > 0.2f && leanBack == false)    // if the button is pressed for more than 0.2 seconds grab
             {
                 leanBack = true;


             }
             else    // else ready the arm, pull back for punch / grab
             {
                 //call punch action readying
             }

         }

         else if (input.ReleaseFire())
         {
             //print("Key Released");
             if (headbuttonClickTimer <= 0.2f && !headButt) // as long as key is held down increase time, this records how long the key is held down
             {
                 headButt = true;
                 headbuttonClickTimer = 0f;
             }
             else
             {
                 leanBack = false;
             }
         }*/

        targetDirection = target - transform.position;
        targetDirection.Normalize();

        //Targetting code
        /*if (input.HoldFire())
        {
            //print("Key Held");
            targetting = true;

        }
        else
        {
            targetting = false;
        }*/
        //targetting = false;

        if (input.PressFire())
        {
            dash = true;
            // print("Just Pressed Key");

            // rb.AddForce(inputDirection * speed);
            


        }


        inputDirection = Vector3.zero;
        if (input.PressRight())
        {
            moveButtonClickTimer = 0f;
            //print("Right Just Pressed Key");
            justStarted = true;
        }
        if (input.HoldRight())
        {
            inputDirection += Vector3.right;
            moveButtonClickTimer += Time.deltaTime;
            //justStarted = false;

        }
        if (input.PressLeft())
        {
            moveButtonClickTimer = 0f;
            justStarted = true;
        }
        if (input.HoldLeft())
        {
           
            inputDirection += Vector3.left;
            moveButtonClickTimer += Time.deltaTime;
            //justStarted = false;
        }
        if (input.PressUp())
        {
            moveButtonClickTimer = 0f;
            justStarted = true;
        }
        if (input.HoldUp())
        {
            inputDirection += Vector3.forward;
            moveButtonClickTimer += Time.deltaTime;
            //justStarted = false;
        }
        if (input.PressDown())
        {
            moveButtonClickTimer = 0f;
            justStarted = true;
        }
        if (input.HoldDown())
        {
            inputDirection += Vector3.back;
            moveButtonClickTimer += Time.deltaTime;
            //justStarted = false;
        }
        if (inputDirection != Vector3.zero)
        {

            if (moveButtonClickTimer > 0.15f && justStarted == true)    // if the button is pressed for more than 0.2 seconds grab
            {
                justStarted = false;


            }
            //justStarted = true;

            inputDirection.Normalize();
            if (cameraBasedMovement)
            {
                inputDirection = Camera.main.transform.TransformDirection(inputDirection);
                inputDirection.y = 0.0f;
            }

            currentFacing = chestBody.transform.forward;
            currentFacing.y = 0;
            currentFacing.Normalize();
            if (legs)
            {
                if (!legs.walking)
                {
                    legs.StartWalking();
                }
            }
            if (!targetting)
            {
                
                faceDirection.facingDirection = inputDirection;
                hipFaceDirection.facingDirection = inputDirection;
                headFaceDirection.facingDirection = inputDirection;
            }
            else
            {
                faceDirection.facingDirection = targetDirection;
                hipFaceDirection.facingDirection = targetDirection;
                headFaceDirection.facingDirection = targetDirection;
            }



        }
        else
        {
            if (legs)
            {
                if (legs.walking)
                {
                    legs.StopWalking();
                }
            }

            if (!targetting)
            {
                
                faceDirection.facingDirection = currentFacing;
                hipFaceDirection.facingDirection = currentFacing;
                headFaceDirection.facingDirection = currentFacing;
            }
            else
            {
                faceDirection.facingDirection = targetDirection;
                hipFaceDirection.facingDirection = targetDirection;
                headFaceDirection.facingDirection = targetDirection;
            }
            justStarted = false;


        }
    }
    void FixedUpdate()
    {
        /*float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");*/
        //Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        if(justStarted)
        {
            rb.AddForce(inputDirection * startSpeed);
        }
        else
        {
            rb.AddForce(inputDirection * speed);
        }

        DoActions();
        if (enableDrag)
            ApplyStandingAndWalkingDrag();

        
        ConvertMoveInputAndPassItToAnimator(inputDirection);
       // chestBody.velocity *= -30f * Time.deltaTime;
    }

    void ConvertMoveInputAndPassItToAnimator(Vector3 moveInput)
    {
        //Convert the move input from world positions to local positions so that they have the correct values
        //depending on where we look
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        localMove.Normalize();
        float turnAmount = localMove.y;
        float forwardAmount = localMove.z;

       /* if (turnAmount != 0)
            turnAmount *= 2;

        */
       // print("Forward : "+ forwardAmount);
       // print("Sideways : "+ turnAmount);

    }

    public void DoActions()
    {

        /*if(dash)
        {
            rb.AddForce(inputDirection * jumpForce, ForceMode.VelocityChange);

            dash = false;
        }*/

       /* if (dash)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

            dash = false;
        }*/
        /*if (leanBack)
        {


            JointDrive x = chestJoint.slerpDrive;
            x.positionDamper = 100f;
            x.positionSpring = 1000f;
            x.maximumForce = 1000f;
            chestJoint.slerpDrive = x;

            Vector3 jointValue = new Vector3(-20f, 0f, 0f);

            chestJoint.targetAngularVelocity = jointValue;

            JointDrive x1 = headJoint.slerpDrive;
            x1.positionDamper = 100f;
            x1.positionSpring = 1000f;
            x1.maximumForce = 1000f;
            headJoint.slerpDrive = x1;

            Vector3 jointValue1 = new Vector3(-20f, 0f, 0f);

            headJoint.targetAngularVelocity = jointValue1;


        }
        else
        {
            JointDrive x = chestJoint.slerpDrive;
            x.positionDamper = 100f;
            x.positionSpring = 1000f;
            x.maximumForce = 10f;
            chestJoint.slerpDrive = x;

            Vector3 jointValue = new Vector3(0f, 0f, 0f);

            chestJoint.targetAngularVelocity = jointValue;

            JointDrive x1 = headJoint.slerpDrive;
            x1.positionDamper = 100f;
            x1.positionSpring = 1000f;
            x1.maximumForce = 10f;
            headJoint.slerpDrive = x1;

            Vector3 jointValue1 = new Vector3(0f, 0f, 0f);

            headJoint.targetAngularVelocity = jointValue1;
        }
        */
       /* if (headButt)
        {
            headButtTimer += Time.deltaTime;
            if (headButtTimer < 0.1f)
            {

                

            }
            if (headButtTimer >= 0.2f && headButtTimer < 0.3f)
            {

                

            }
            if (headButtTimer >= 0.3f && headButtTimer < 0.5f)
            {

            }
            if (headButtTimer >= 0.5f)
            {
                headButt = false;
                headButtTimer = 0f;

            }
        }*/
    }

    private void ApplyStandingAndWalkingDrag()
    {
        // ***********  APPLY DRAGS! **
        //
        // THIS, along with the powerful facing direction forces, ACTUALLY MAKES THE CHARACTERS LESS INTERACTIBLE, BECAUSE THEY CAN'T PUSH EACH OTHER MUCH *****
        // SOFTER FORCES CAN BE BETTER, BUT THOSE NEED MORE TWEEKING, IDEALLY JUST ENOUGH FORCE TO ACHIEVE THE EFFECT WITHOUT BECOMING LOCKED INTO THAT POSITION OR DIRECTION ***
        //
        if (inputDirection == Vector3.zero)
        {
            // ***** WHEN STANDING STILL, APPLY A DRAG BASED ON HOW FAST THE TORSO IS TRAVELLING ***
            //
            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;
            //
            float speed = horizontalVelocity.magnitude;
            //
            rb.velocity *= (1 - Mathf.Clamp(speed * 20f + 10, 0, 50) * Time.fixedDeltaTime);
        }
        else
        {
            // ***** APPLY A POWERFUL DRAG FORCE IF THE TORSO ISN'T TRAVELLING IN THE INPUT DIRECITON, ALLOWS FOR TIGHT TURNS ***
            //
            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;
            float m = 1 - (1 + Vector3.Dot(horizontalVelocity.normalized, inputDirection)) / 2f;
            rb.velocity *= (1 - (m * 30) * Time.fixedDeltaTime);
            //faceDirection.facingDirection = targetDirection;


            //float m = 1 - (1 + Vector3.Dot(horizontalVelocity.normalized, inputDirection)) / 2f;

        }
        //
    }
}