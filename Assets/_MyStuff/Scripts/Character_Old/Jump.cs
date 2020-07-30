using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using UnityEngine.Events;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug;

public class Jump : MonoBehaviour {

    public Rigidbody chestBody;

    public Vector3 inputDirection;
    public CharacterInput input;
    public CharacterFaceDirection faceDirection;
    public CharacterFaceDirection hipFaceDirection;
    protected Vector3 currentFacing = Vector3.zero;


    public float jumpCounter = 0;
    public float jumpDelay = 0.2f;
    public float bendKneeDelay = 0.4f;
    public float airTimeDelay = 0.55f;
    protected bool jumpAnticipation = false;
    public bool inAir = false;
    public float jumpForce = 200;
    public float jumpForwardForce = 150;
    public float facePlantForce = 30;

    public bool spin = true;
    public float spinTorque = 360;
    protected float facePlantM = 1;
    protected float getUpCounter = 0;
    public bool canJump = true;
    public bool cameraBasedMovement = true;
    public bool applyDrag = true;


    public float dragMultiplier = 20f;
    public float dragAdded = 10f;
    public float reduceDragValue = 1f;


    public CharacterLegsSimple1 legs;
    //public CharacterUpright1 chestUpright;
    public CharacterMaintainHeight maintainHeight;
    //public CharacterFaceDirection faceDirection;

    public float maintainHeightStanding = 1.2f;
    public float maintainHeightCrouching = 0.6f;
    public Rigidbody[] feetBodies = new Rigidbody[2];

    public PlayerController1 pContrl;
    public CharacterThinker character;
    public QuadraticDrag qDrag;


    public Rigidbody[] thighsBody = new Rigidbody[2];
    public ConfigurableJoint[] thighsJoint = new ConfigurableJoint[2];

    public Rigidbody[] legsBody = new Rigidbody[2];
    public ConfigurableJoint[] legsJoint = new ConfigurableJoint[2];

    public Rigidbody[] feetBody = new Rigidbody[2];
    public ConfigurableJoint[] feetJoint = new ConfigurableJoint[2];

    public Kicking kick;

    public bool kickedInAir = false;

    public CharacterLimpSetting limpSetting;

    public UnityEvent OnJumpAnticipation;
    public UnityEvent OnJumpOver;

    public Vector3 jumpTowards;

    public string jumpButton;

    
    // Use this for initialization
    void Start () {
        pContrl = GetComponent<PlayerController1>();

        //qDrag = GetComponent<QuadraticDrag>();

        character = GetComponent<CharacterThinker>();
        //kick = GetComponent<Kicking>();
        chestBody = character.bpHolder.BodyPartsName[BodyPartNames.chestName].bodyPartRb;

        jumpTowards = Vector3.zero;
    }

    void CheckJumpInput()
    {
        if (character.player.GetButtonDown(jumpButton) && canJump)
        {
            StartJumpAnticipation();
        }



    }

    public void JumpNow(Vector3 target)
    {
        jumpTowards = target;
        if (canJump)
        {
            StartJumpAnticipation();
        }
    }

    private void GetUpFromJump()
    {
        // ***********************  STAND UP AFTER BEING A RAGDOLL *******
        //
        /* foreach (CharacterMaintainHeight h in otherMaintainHeight)
         {
             h.enabled = true;
             h.desiredHeight = -3; // **** START ARMS ON GROUND AND THEN LERP THIS VALUE TO NORMAL HEIGHT ****
         }
         foreach (CharacterUpright1 h in otherUprights)
         {
             h.enabled = true;
         }*/
        //
        
        getUpCounter = 3; // ** JUST USED TO SETTLE THE ARMS ***
        //
        //
        // **** NEXT: REACTIVATE ALL THE OTHER COMPONENTS THAT MOVE THE LIMBS AND TORSO ****
        //
        
        
        
        /*if (chestUpright != null)
            chestUpright.enabled = true;*/
        //
        // *** DO A SMALL HOP UPWARD TO START GETTING UP ***
        //
        chestBody.AddForceAtPosition((chestBody.transform.forward * -1 + Vector3.up) * 20, chestBody.transform.TransformPoint(Vector3.up * 0.2f), ForceMode.Impulse);

        kickedInAir = false;
        
        
        canJump = true;
        inAir = false;
    }
    private void Jump2()
    {
        
        qDrag.enabled = false;
        // ***********************  ACTUALLY JUMP - Launch into the air *******
        //
        //
        // **** DISABLE SOME CONTROLLING COMPONENTS (the height maintaining script on the torso and upright forces on feet) ****
        //
        /*foreach (CharacterMaintainHeight h in otherMaintainHeight)
        {
            h.enabled = false;
        }
        foreach (CharacterUpright1 h in otherUprights)
        {
            h.enabled = false;
        }*/
            // **** LAUNCH INTO AIR HERE :
            //
            //Use the following to spin in the direction the user presses
            character.speed = jumpForwardForce;
        chestBody.AddForce(Vector3.up * jumpForce + character.inputDirection * jumpForwardForce, ForceMode.Impulse);

        //chestBody.AddForce(Vector3.up * jumpForce + chestBody.transform.forward * jumpForwardForce, ForceMode.Impulse);
        //
        // **** NEXT: DISABLE ALL THE OTHER CONTROLLING COMPONENTS AND ESSENTIALLY BECOME A RAGDOLL ****
        //
        
        maintainHeight.enabled = false;
        jumpCounter = 0;
        jumpAnticipation = false;
        inAir = true;
        legs.enabled = false;
       /* if (chestUpright != null)
            chestUpright.enabled = false;*/
        //faceDirection.enabled = false;
        //
        // ****  SOMETIMES THE FACEPLANT IS GOING TO HAVE MORE FORCE ON IT, BECAUSE RANDOM STRENGTH FACEPLANTS ARE COOL ***
        //
        facePlantM = 0.9f + Random.value * 0.4f;

        OnJumpAnticipation.Invoke();

        EffectsController.Instance.PlayWooshSound(chestBody.transform.position, (Random.Range(0f,1f)) * 100, "jump");
        EffectsController.Instance.PlayYellSound(chestBody.transform.position, (Random.Range(0f, 1f)) * 100, "jump");


    }

    //
    private void StartJumpAnticipation()
    {
       
        // ***********************  CROUCH A BIT UNTIL THE ACTUAL JUMP *******
        canJump = false;
        legs.StopWalking();
        jumpAnticipation = true;
        maintainHeight.desiredHeight = maintainHeightCrouching;
        jumpCounter = 0;
        EffectsController.Instance.PlayWindupSound(chestBody.transform.position, 100, "jump");
        EffectsController.Instance.PlayPlayerWindupSound(chestBody.transform.position, 100, "jump");






    }

    private void KneeBend()
    {

        for (int i = 0; i < 2; i++)
        {
            JointDrive x = thighsJoint[i].slerpDrive;
            x.positionDamper = 100;
            x.positionSpring = 1000;
            x.maximumForce = 1000;
            thighsJoint[i].slerpDrive = x;

            Vector3 jointValue = new Vector3(10, 0f, 0f);

            thighsJoint[i].targetAngularVelocity = jointValue;

            JointDrive x1 = legsJoint[i].slerpDrive;
            x1.positionDamper = 100;
            x1.positionSpring = 1000;
            x1.maximumForce = 1000;
            legsJoint[i].slerpDrive = x1;

            Vector3 jointValue1 = new Vector3(-40, 0f, 0f);

            legsJoint[i].targetAngularVelocity = jointValue1;
        }




    }

    void Update()
    {
        CheckJumpInput();
        if (getUpCounter > 0)
        {
            getUpCounter -= Time.deltaTime;
            //
            // *****************  LIFT ARMS OFF OF THE GROUND SLOWLY WHEN GETTING UP ************
            //
            /*foreach (CharacterMaintainHeight h in otherMaintainHeight)
            {
                h.desiredHeight = Mathf.Lerp(h.desiredHeight, 0.2f, Time.deltaTime * 3);
            }*/
        }
        if (jumpAnticipation)
        {
            //***********************************  CROUCHING BEFORE JUMP **********************
            //
            jumpCounter += Time.deltaTime;
            if (jumpCounter >= jumpDelay)
            {
                Jump2();
            }
            
        }
        else if (inAir)
        {
            //input.timerValue = 0.01f;
            //***********************************  AIR BORNE **********************
            //
            jumpCounter += Time.deltaTime;

            //if(kick != null)
            //{
            //    if (kick.leftKick || kick.rightKick)
            //    {
            //        kickedInAir = true;
            //    }
            //}

            
            
            
            if (jumpCounter >= bendKneeDelay && jumpCounter < airTimeDelay)
            {
                //if(!kick.leftKick && !kick.rightKick && !kick.leftWindUp && !kick.rightWindUp)
                if(!kickedInAir)
                    KneeBend();
            }

            if (jumpCounter >= airTimeDelay )
            {
                //OnJumpOver.Invoke();
                maintainHeight.enabled = true;
                maintainHeight.desiredHeight = maintainHeightStanding;
                faceDirection.enabled = true;
                legs.enabled = true;
                qDrag.enabled = true;
                character.speed = character.normalSpeed;
                //GetUpFromJump();

                for (int i = 0; i < 2; i++)
                {
                    /* JointDrive x = thighsJoint[i].slerpDrive;
                     x.positionDamper = 0;
                     x.positionSpring = 0;
                     x.maximumForce = 0;
                     thighsJoint[i].slerpDrive = x;

                     Vector3 jointValue = new Vector3(0, 0f, 0f);

                     thighsJoint[i].targetAngularVelocity = jointValue;

                     JointDrive x1 = legsJoint[i].slerpDrive;
                     x1.positionDamper = 0;
                     x1.positionSpring = 0;
                     x1.maximumForce = 0;
                     legsJoint[i].slerpDrive = x1;

                     Vector3 jointValue1 = new Vector3(-0, 0f, 0f);

                     legsJoint[i].targetAngularVelocity = jointValue1;
                     */
                    JointDrive x = thighsJoint[i].slerpDrive;
                    x.positionDamper = 10;
                    x.positionSpring = 100;
                    x.maximumForce = 100;
                    thighsJoint[i].slerpDrive = x;

                    Vector3 jointValue = new Vector3(0, 0f, 0f);

                    thighsJoint[i].targetAngularVelocity = jointValue;

                    JointDrive x1 = legsJoint[i].slerpDrive;
                    x1.positionDamper = 0.6f;
                    x1.positionSpring = 50;
                    x1.maximumForce = 50;
                    legsJoint[i].slerpDrive = x1;

                    Vector3 jointValue1 = new Vector3(-0, 0f, 0f);

                    legsJoint[i].targetAngularVelocity = jointValue1;
                }
            }

            if (jumpCounter >= airTimeDelay && character.grounded)
            {
                OnJumpOver.Invoke();
                GetUpFromJump();
            }
            //
        }
    }
        // Update is called once per frame
        void FixedUpdate() {

        

        if (!inAir && character.grounded)
        {
            //input.timerValue = 0.17f;
            character.enableDrag = true;
           /* JointDrive x = thighsJoint[1].slerpDrive;
            x.positionDamper = 0;
            x.positionSpring = 0;
            x.maximumForce = 0;
            thighsJoint[1].slerpDrive = x;

            Vector3 jointValue = new Vector3(0, 0, 0);

            thighsJoint[1].targetAngularVelocity = jointValue;

            JointDrive x1 = legsJoint[1].slerpDrive;
            x1.positionDamper = 0;
            x1.positionSpring = 0;
            x1.maximumForce = 0;
            legsJoint[1].slerpDrive = x1;

            Vector3 jointValue1 = new Vector3(0, 0, 0);

            legsJoint[1].targetAngularVelocity = jointValue1;
            */

           

            // ****  APPLY DRAGS ****
            //
            /* if (applyDrag)
                 ApplyStandingAndWalkingDrag();*/
            //
             if (!jumpAnticipation)
             {
                 if (inputDirection != Vector3.zero)
                 {
                     // *********************  MOVE CHEST IN THE INPUT DIRECTION *******
                     //
                     // *** (THIS IS ZERO IN THE PROJECT BY DEFAULT, I PREFER HAVING THE LEGS PULL THE BODY FORWARD ***
                     //
                     //chestBody.AddForceAtPosition(force * inputDirection * Time.deltaTime, chestBody.transform.TransformDirection(Vector3.forward * 2), ForceMode.Impulse);
                     //                   
                     //                    
                 }
             }

        }
        else if (inAir || !character.grounded)
        {
            chestBody.maxAngularVelocity = Mathf.Infinity;
            if (character.windUp)
            {
                chestBody.velocity *= -30f * Time.deltaTime;
                chestBody.angularVelocity *= -30f * Time.deltaTime;
            }

            character.enableDrag = false;
            //
            // *******************************************  TOWARDS END OF JUMP, FORCE A FACEPLANT *****
            //
            if (jumpCounter > airTimeDelay * 0.1f && jumpCounter < airTimeDelay * 0.8f)
            {
                //var forwardDir = chestBody.transform.TransformDirection(inputDirection) + Vector3.down;
                 chestBody.AddForceAtPosition((chestBody.transform.forward + Vector3.down) * facePlantForce * facePlantM * Time.deltaTime, chestBody.transform.TransformPoint(Vector3.up * 2), ForceMode.Impulse);
                //chestBody.AddTorque(chestBody.transform.forward);
                //chestBody.AddForceAtPosition((forwardDir) * facePlantForce * facePlantM * Time.deltaTime, chestBody.transform.TransformPoint(Vector3.up * 2), ForceMode.Impulse);


                if(spin)
                {
                    
                    inputDirection = Camera.main.transform.TransformDirection(character.inputDirection);
                    inputDirection.y = 0.0f;
                    Vector3 middleFinger = Vector3.Cross(Vector3.up, inputDirection);
                    Debug.DrawRay(chestBody.transform.position, middleFinger * 3, Color.yellow);
                    chestBody.AddTorque(middleFinger * spinTorque);

                    Debug.Log("angular velocity : " + chestBody.angularVelocity.magnitude);

                }
               

                //Debug.DrawRay(chestBody.transform.TransformPoint(Vector3.up * 2), chestBody.transform.forward + Vector3.down, Color.yellow);
                foreach (Rigidbody f in feetBodies)
                {
                    f.AddForce(Vector3.up * 10 * Time.deltaTime, ForceMode.Impulse);
                }
            }
        }
    }

    private void ApplyStandingAndWalkingDrag()
    {
        // ***********  APPLY DRAGS! **
        //
        // THIS, along with the powerful facing direction forces, ACTUALLY MAKES THE CHARACTERS LESS INTERACTIBLE, BECAUSE THEY CAN'T PUSH EACH OTHER MUCH *****
        // SOFTER FORCES CAN BE BETTER, BUT THOSE NEED MORE TWEEKING, IDEALLY JUST ENOUGH FORCE TO ACHIEVE THE EFFECT WITHOUT BECOMING LOCKED INTO THAT POSITION OR DIRECTION ***
        //
        if (character.inputDirection == Vector3.zero)
        {
            // ***** WHEN STANDING STILL, APPLY A DRAG BASED ON HOW FAST THE TORSO IS TRAVELLING ***
            //
            Vector3 horizontalVelocity = chestBody.velocity;
            horizontalVelocity.y = 0;
            //
            float speed = horizontalVelocity.magnitude;
            //
            chestBody.velocity *= (1 - (Mathf.Clamp(speed * dragMultiplier + dragAdded, 0, 50)) * reduceDragValue * Time.fixedDeltaTime);
        }
        else
        {
            // ***** APPLY A POWERFUL DRAG FORCE IF THE TORSO ISN'T TRAVELLING IN THE INPUT DIRECITON, ALLOWS FOR TIGHT TURNS ***
            //
            Vector3 horizontalVelocity = chestBody.velocity;
            horizontalVelocity.y = 0;
            //
            // print(horizontalVelocity);
            float m = 1 - (1 + Vector3.Dot(horizontalVelocity.normalized, character.inputDirection)) / 2f;
            chestBody.velocity *= (1 - (m * 100) * Time.fixedDeltaTime);
        }
        //
    }
}
