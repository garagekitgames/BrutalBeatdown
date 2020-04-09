using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
public class Kicking : MonoBehaviour {

    public Rigidbody chestBody;

    public PlayerController1 pContrl;

    public CharacterThinker character;
    public Rigidbody[] thighsBody = new Rigidbody[2];
    public ConfigurableJoint[] thighsJoint = new ConfigurableJoint[2];

    public Rigidbody[] legsBody = new Rigidbody[2];
    public ConfigurableJoint[] legsJoint = new ConfigurableJoint[2];

    public Rigidbody[] feetBody = new Rigidbody[2];
    public ConfigurableJoint[] feetJoint = new ConfigurableJoint[2];

    public CharacterInput input;

    private float leftbuttonClickTimer = 0f;
    private float rightbuttonClickTimer = 0f;

   // public bool leftBlock = false;
    public bool leftKick = false;
   // public bool leftThrow = false;
    public bool leftWindUp = false;


    //public bool rightBlock = false;
    public bool rightKick = false;
    //public bool rightThrow = false;
    public bool rightWindUp = false;

    public float leftkickTimer = 0.0f;
    public float leftwindTimer = 0.0f;

    private float rightwindTimer = 0.0f;

    private float rightkickTimer = 0.0f;

    [Range(0, 1)]
    public float kickForceApplyPercent = 0.5f;// percentage of the punch duration the force is applied, shorter duration would mean less tracking
    [Range(0, 5)]
    public float kickPowerIncreaseRate = 2;//2 to 5 works good
    [Range(0f, 0.9f)]
    public float accuracyReduceFactor = 1f; //less value is better, percentage of 

    public float kickSpeed = 0.5f;
    public float kickPower = 1300f;
    public float startPunchPower = 0f;
    public float maxPunchPower = 3500;
    public float minPunchPower = 1300;

    public Vector3 target;

    public Vector3 leftKickTarget = Vector3.zero;
    public Vector3 rightKickTarget = Vector3.zero;

    public Jump jump;
    public bool windupDrag = true;
    public string attackButton = "SimpleSecondaryAttack";

    public bool pickSide = false;
    public int attackSide = 0;
    // Use this for initialization
    void Start () {

        startPunchPower = kickPower;
        input = GetComponent<CharacterInput>();
       // pContrl = GetComponent<PlayerController1>();
        character = GetComponent<CharacterThinker>();
        jump = GetComponent<Jump>();

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
	
	// Update is called once per frame
	void Update () {

        CheckInput();

        //target = pContrl.target;

        target = character.target;

    }

    private void CheckInput()
    {
        //LEft and Right Punch Grab and Throw 
        if (character.player.GetButtonDown(attackButton))
        {
            leftbuttonClickTimer = 0f;
            leftWindUp = false;
            character.windUp = false;
            pickSide = !pickSide; // toggles onoff at each click

            if (pickSide)
            {
                attackSide = 0;
            }
            else
            {
                attackSide = 1;
            }

        }

        else if (character.player.GetButton(attackButton))
        {
            leftbuttonClickTimer += Time.deltaTime;
            if (leftWindUp == false && !leftKick)    // if the button is pressed for more than 0.2 seconds grab
            {
                leftWindUp = true;
                character.windUp = true;

            }


        }

        else if (character.player.GetButtonUp(attackButton))
        {
            //print("Key Released");
            if (!leftKick) // as long as key is held down increase time, this records how long the key is held down
            {
                kickPower = Mathf.Clamp(startPunchPower * (1 + leftbuttonClickTimer * kickPowerIncreaseRate), minPunchPower, maxPunchPower);

                leftKick = true;
                character.attack = true;
                leftbuttonClickTimer = 0f;
            }

            leftWindUp = false;
            character.windUp = false;
            /*JointDrive x = thighsJoint[0].slerpDrive;
            x.positionDamper = 100f;
            x.positionSpring = 1000f;
            x.maximumForce = 1000f;
            thighsJoint[0].slerpDrive = x;

            Vector3 jointValue = new Vector3(-15f, -10f, 5f);

            thighsJoint[0].targetAngularVelocity = jointValue;

            JointDrive x1 = forethighsJoint[0].slerpDrive;
            x1.positionDamper = 100f;
            x1.positionSpring = 1000f;
            x1.maximumForce = 1000f;
            forethighsJoint[0].slerpDrive = x1;

            Vector3 jointValue1 = new Vector3(-40f, 4f, 6f);

            forethighsJoint[0].targetAngularVelocity = jointValue1;*/

        }



        //////////////////

        if (input.PressRightKick())
        {
            // print("Just Pressed Key");

            rightbuttonClickTimer = 0f;
            rightWindUp = false;

        }

        else if (input.HoldRightKick())
        {

            rightbuttonClickTimer += Time.deltaTime;
            if (rightWindUp == false && !rightKick)    // if the button is pressed for more than 0.2 seconds grab
            {
                rightWindUp = true;

            }


        }

        else if (input.ReleaseRightKick())
        {

            if (!rightKick) // as long as key is held down increase time, this records how long the key is held down
            {
                kickPower = Mathf.Clamp(startPunchPower * (1 + rightbuttonClickTimer * kickPowerIncreaseRate), minPunchPower, maxPunchPower);

                rightKick = true;
                rightbuttonClickTimer = 0f;
            }

            rightWindUp = false;

            /*
                        JointDrive x = armsJoint[1].slerpDrive;
                        x.positionDamper = 100f;
                        x.positionSpring = 1000f;
                        x.maximumForce = 1000f;
                        armsJoint[1].slerpDrive = x;

                        Vector3 jointValue = new Vector3(15f, 10f, 5f);

                        armsJoint[1].targetAngularVelocity = jointValue;

                        JointDrive x1 = forearmsJoint[1].slerpDrive;
                        x1.positionDamper = 100f;
                        x1.positionSpring = 1000f;
                        x1.maximumForce = 1000f;
                        forearmsJoint[1].slerpDrive = x1;

                        Vector3 jointValue1 = new Vector3(40f, -4f, 6f);

                        forearmsJoint[1].targetAngularVelocity = jointValue1;*/

        }

        //character.attackButtonClickTimer = leftbuttonClickTimer;
        //character.windUp = leftWindUp;
        //character.attack = leftKick;


    }

    private void FixedUpdate()
    {
        if (leftWindUp)
        {
            //character.windUp = true;
            leftwindTimer += Time.deltaTime;
            JointDrive x = thighsJoint[attackSide].slerpDrive;
            x.positionDamper = 100f;
            x.positionSpring = 1000f;
            x.maximumForce = 1000f;
            thighsJoint[attackSide].slerpDrive = x;

            Vector3 jointValue = new Vector3(40, 0f, 0f);

            thighsJoint[attackSide].targetAngularVelocity = jointValue;

            JointDrive x1 = legsJoint[attackSide].slerpDrive;
            x1.positionDamper = 100f;
            x1.positionSpring = 1000f;
            x1.maximumForce = 1000f;
            legsJoint[attackSide].slerpDrive = x1;

            Vector3 jointValue1 = new Vector3(-40, 0f, 0f);

            legsJoint[attackSide].targetAngularVelocity = jointValue1;

            if (windupDrag)
                chestBody.velocity *= -30f * Time.deltaTime;

            if (leftwindTimer < 0.2f && !jump.inAir)
            {
                chestBody.AddForce((Vector3.up * 800) * Time.deltaTime, ForceMode.VelocityChange);
            }
            


                //chestBody.AddForce((Vector3.up * 500 + pContrl.inputDirection * 500) * Time.deltaTime, ForceMode.VelocityChange);

            }

        if (leftKick)
        {
            //character.windUp = false;
            leftwindTimer = 0;
            //EffectsController.PlayPunchSound(feetBody[0].position, feetBody[0].velocity.sqrMagnitude, feetBody[0].name);
            leftkickTimer += Time.deltaTime;
            if (leftkickTimer < kickSpeed * kickForceApplyPercent)
            {
                //character.attack = true;
                if (leftkickTimer < kickSpeed * (kickForceApplyPercent - kickForceApplyPercent * accuracyReduceFactor))
                {
                    leftKickTarget = (target- feetBody[attackSide].transform.position).normalized;

                }
               /* JointDrive x = thighsJoint[0].slerpDrive;
                x.positionDamper = 0;
                x.positionSpring = 0;
                x.maximumForce = 0;
                thighsJoint[0].slerpDrive = x;

                Vector3 jointValue = new Vector3(20f, 0f, 0f);

                thighsJoint[0].targetAngularVelocity = jointValue;

                JointDrive x1 = legsJoint[0].slerpDrive;
                x1.positionDamper =0;
                x1.positionSpring = 0;
                x1.maximumForce = 0;
                legsJoint[0].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0f, 0f, 0f);

                legsJoint[0].targetAngularVelocity = jointValue1;
                */
                JointDrive x = thighsJoint[attackSide].slerpDrive;
                x.positionDamper = 10;
                x.positionSpring = 100;
                x.maximumForce = 100;
                thighsJoint[attackSide].slerpDrive = x;

                Vector3 jointValue = new Vector3(20f, 0f, 0f);

                thighsJoint[attackSide].targetAngularVelocity = jointValue;

                JointDrive x1 = legsJoint[attackSide].slerpDrive;
                x1.positionDamper =0.6f;
                x1.positionSpring = 50;
                x1.maximumForce = 50;
                legsJoint[attackSide].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0f, 0f, 0f);

                legsJoint[attackSide].targetAngularVelocity = jointValue1;
                //feetBody[0].AddForceAtPosition((feetBody[0].transform.forward ) * 10, feetBody[0].transform.TransformPoint(Vector3.back * 0.2f), ForceMode.Impulse);

                //feetBody[0].AddForce((chestBody.transform.position + chestBody.transform.forward + (chestBody.transform.forward * -1) / 2f - feetBody[0].transform.position).normalized * 1000 * Time.deltaTime, ForceMode.VelocityChange);

                if (target != null)
                {


                    /*Vector3 a = (target.transform.position - feetBody[0].transform.position).normalized;
                    thighsBody[0].AddForce(-(a * punchPower * 0.34f) * Time.deltaTime, ForceMode.VelocityChange);
                    feetBody[0].AddForce(a * punchPower * 1f * Time.deltaTime, ForceMode.VelocityChange);
                    */

                //leftPunchTarget

                thighsBody[attackSide].AddForce((leftKickTarget * kickPower * -0.5f) * Time.deltaTime, ForceMode.VelocityChange);
                   // legsBody[0].AddForce(-(leftKickTarget * kickPower * 0.5f) * Time.deltaTime, ForceMode.Impulse);
                    feetBody[attackSide].AddForce(leftKickTarget * kickPower * 1f * Time.deltaTime, ForceMode.VelocityChange);

                    //Debug.Log("attackTarget : " + leftKickTarget);
                    //Debug.Log("attackPower : " + kickPower);
                    //Debug.Log("forceRatio.forceRatio : " + "-0.5f");
                    // chestBody.AddForce(leftKickTarget * kickPower * 0.1f * Time.deltaTime, ForceMode.VelocityChange);
                    /* thighsBody[0].AddForce(-(a * 20f), ForceMode.VelocityChange);
                     feetBody[0].AddForce(a * 30f , ForceMode.VelocityChange);*/
                    if (leftkickTimer >= kickSpeed * (kickForceApplyPercent - 0.01f) && leftkickTimer < kickSpeed * kickForceApplyPercent)
                    {
                        //print("Left Hand : feetBody[0].velocity.sqrMagnitude : " + feetBody[0].velocity.sqrMagnitude);
                        //Debug.Log("kickPower : " + kickPower);
                        //Debug.Log("leftbuttonClickTimer : " + leftbuttonClickTimer);
                        // EffectsController.PlayPunchSound(feetBody[0].position, feetBody[0].velocity.sqrMagnitude, feetBody[0].name);
                        //EffectsController.Shake(0.03f, 0.1f);
                    }

                }
                //if (leftpunch || rightpunch)
                // {
                chestBody.velocity *= -100f * Time.deltaTime;
                //}

            }
            /*if (leftkickTimer >= punchSpeed * 0.2f && leftkickTimer < punchSpeed * 0.4f)
            {
                


            }
            if (leftkickTimer >= punchSpeed * 0.4f && leftkickTimer < punchSpeed * 0.6f)
            {
                

            }
            if (leftkickTimer >= punchSpeed * 0.6f && leftkickTimer < punchSpeed * 1f)
            {
                

            }*/
            if (leftkickTimer >= kickSpeed * 1f)
            {
               /* JointDrive x = thighsJoint[0].slerpDrive;
                x.positionDamper = 0;
                x.positionSpring = 0;
                x.maximumForce = 0;
                thighsJoint[0].slerpDrive = x;

                Vector3 jointValue = new Vector3(0, 0, 0);

                thighsJoint[0].targetAngularVelocity = jointValue;

                JointDrive x1 = legsJoint[0].slerpDrive;
                x1.positionDamper = 0;
                x1.positionSpring = 0;
                x1.maximumForce = 0;
                legsJoint[0].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0, 0, 0);

                legsJoint[0].targetAngularVelocity = jointValue1;
                */
                
                JointDrive x = thighsJoint[attackSide].slerpDrive;
                x.positionDamper = 10;
                x.positionSpring = 100;
                x.maximumForce = 100;
                thighsJoint[attackSide].slerpDrive = x;

                Vector3 jointValue = new Vector3(0, 0, 0);

                thighsJoint[attackSide].targetAngularVelocity = jointValue;

                JointDrive x1 = legsJoint[attackSide].slerpDrive;
                x1.positionDamper = 0.6f;
                x1.positionSpring = 50;
                x1.maximumForce = 50;
                legsJoint[attackSide].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0, 0, 0);

                legsJoint[attackSide].targetAngularVelocity = jointValue1;
                leftKick = false;
                leftkickTimer = 0f;


                //character.attack = false;
            }
        }

        if (rightWindUp)
        {
            rightwindTimer += Time.deltaTime;
            JointDrive x = thighsJoint[1].slerpDrive;
            x.positionDamper = 100f;
            x.positionSpring = 1000f;
            x.maximumForce = 1000f;
            thighsJoint[1].slerpDrive = x;

            Vector3 jointValue = new Vector3(40, 0f, 0f);

            thighsJoint[1].targetAngularVelocity = jointValue;

            JointDrive x1 = legsJoint[1].slerpDrive;
            x1.positionDamper = 100f;
            x1.positionSpring = 1000f;
            x1.maximumForce = 1000f;
            legsJoint[1].slerpDrive = x1;

            Vector3 jointValue1 = new Vector3(-40, 0f, 0f);

            legsJoint[1].targetAngularVelocity = jointValue1;

            if (windupDrag)
                chestBody.velocity *= -30f * Time.deltaTime;

            if (rightwindTimer < 0.2f && !jump.inAir)
            {
                chestBody.AddForce((Vector3.up * 800) * Time.deltaTime, ForceMode.VelocityChange);
            }
            //chestBody.AddForce((Vector3.up * 500 + pContrl.inputDirection * 500) * Time.deltaTime, ForceMode.VelocityChange);

        }

        if (rightKick)
        {
            rightwindTimer = 0;
            //EffectsController.PlayPunchSound(feetBody[0].position, feetBody[0].velocity.sqrMagnitude, feetBody[0].name);
            rightkickTimer += Time.deltaTime;
            if (rightkickTimer < kickSpeed * kickForceApplyPercent)
            {
                if (rightkickTimer < kickSpeed * (kickForceApplyPercent - kickForceApplyPercent * accuracyReduceFactor))
                {
                    rightKickTarget = (target - feetBody[1].transform.position).normalized;

                }
                /*JointDrive x = thighsJoint[1].slerpDrive;
                x.positionDamper = 0;
                x.positionSpring = 0;
                x.maximumForce = 0;
                thighsJoint[1].slerpDrive = x;

                Vector3 jointValue = new Vector3(20f, 0f, 0f);

                thighsJoint[1].targetAngularVelocity = jointValue;

                JointDrive x1 = legsJoint[1].slerpDrive;
                x1.positionDamper = 0;
                x1.positionSpring = 0;
                x1.maximumForce = 0;
                legsJoint[1].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0f, 0f, 0f);

                legsJoint[1].targetAngularVelocity = jointValue1;
                */
                
                JointDrive x = thighsJoint[1].slerpDrive;
                x.positionDamper = 10;
                x.positionSpring = 100;
                x.maximumForce = 100;
                thighsJoint[1].slerpDrive = x;

                Vector3 jointValue = new Vector3(20f, 0f, 0f);

                thighsJoint[1].targetAngularVelocity = jointValue;

                JointDrive x1 = legsJoint[1].slerpDrive;
                x1.positionDamper = 0.6f;
                x1.positionSpring = 50;
                x1.maximumForce = 50;
                legsJoint[1].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0f, 0f, 0f);

                legsJoint[1].targetAngularVelocity = jointValue1;
                //feetBody[0].AddForceAtPosition((feetBody[0].transform.forward ) * 10, feetBody[0].transform.TransformPoint(Vector3.back * 0.2f), ForceMode.Impulse);

                //feetBody[0].AddForce((chestBody.transform.position + chestBody.transform.forward + (chestBody.transform.forward * -1) / 2f - feetBody[0].transform.position).normalized * 1000 * Time.deltaTime, ForceMode.VelocityChange);

                if (target != null)
                {


                    /*Vector3 a = (target.transform.position - feetBody[0].transform.position).normalized;
                    thighsBody[0].AddForce(-(a * punchPower * 0.34f) * Time.deltaTime, ForceMode.VelocityChange);
                    feetBody[0].AddForce(a * punchPower * 1f * Time.deltaTime, ForceMode.VelocityChange);
                    */

                    //leftPunchTarget

                    thighsBody[1].AddForce((rightKickTarget * kickPower * -0.5f) * Time.deltaTime, ForceMode.VelocityChange);
                    feetBody[1].AddForce(rightKickTarget * kickPower * 1f * Time.deltaTime, ForceMode.VelocityChange);

                    /* thighsBody[0].AddForce(-(a * 20f), ForceMode.VelocityChange);
                     feetBody[0].AddForce(a * 30f , ForceMode.VelocityChange);*/
                    if (leftkickTimer >= kickSpeed * (kickForceApplyPercent - 0.01f) && leftkickTimer < kickSpeed * kickForceApplyPercent)
                    {
                        //print("Left Hand : feetBody[0].velocity.sqrMagnitude : " + feetBody[0].velocity.sqrMagnitude);

                        // EffectsController.PlayPunchSound(feetBody[0].position, feetBody[0].velocity.sqrMagnitude, feetBody[0].name);
                        //EffectsController.Shake(0.03f, 0.1f);
                    }

                }
                //if (leftpunch || rightpunch)
                // {
                chestBody.velocity *= -100 * Time.deltaTime;
                //}

            }
            /*if (leftkickTimer >= punchSpeed * 0.2f && leftkickTimer < punchSpeed * 0.4f)
            {
                


            }
            if (leftkickTimer >= punchSpeed * 0.4f && leftkickTimer < punchSpeed * 0.6f)
            {
                

            }
            if (leftkickTimer >= punchSpeed * 0.6f && leftkickTimer < punchSpeed * 1f)
            {
                

            }*/
            if (rightkickTimer >= kickSpeed * 1f)
            {
                JointDrive x = thighsJoint[1].slerpDrive;
                x.positionDamper = 10;
                x.positionSpring = 100;
                x.maximumForce = 100;
                thighsJoint[1].slerpDrive = x;

                Vector3 jointValue = new Vector3(0, 0, 0);

                thighsJoint[1].targetAngularVelocity = jointValue;

                JointDrive x1 = legsJoint[1].slerpDrive;
                x1.positionDamper = 0.6f;
                x1.positionSpring = 50;
                x1.maximumForce = 50;
                legsJoint[1].slerpDrive = x1;

                Vector3 jointValue1 = new Vector3(0, 0, 0);

                legsJoint[1].targetAngularVelocity = jointValue1;
                /*
                JointDrive x = thighsJoint[1].slerpDrive;
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

                legsJoint[1].targetAngularVelocity = jointValue1;*/
                rightKick = false;
                rightkickTimer = 0f;

            }
        }

        character.attackTarget = leftKickTarget;
        ////character.windTimer = leftWindTimer;
        //character.attackTimer = leftkickTimer;
        //character.windUp = leftWindUp;
        //character.attack = leftKick;
    }
}
