using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using garagekitgames;

public class DanceController : MonoBehaviour
{
    public AnimationCurve curve;
    public float kickCounter = 0;
    //public float kickDelay = 1f;
    // Start is called before the first frame update

    public CharacterThinker character;

    public BodyPartMono head;
    public BodyPartMono chest;
    public BodyPartMono hip;

    public BodyPartMono leftHand;
    public BodyPartMono rightHand;


    public BodyPartMono leftThigh;
    public BodyPartMono rightThigh;


    public BodyPartMono leftLeg;
    public BodyPartMono rightLeg;

    [Range(0, 2)]
    public float frequency_1 = 1;
    [Range(0, 2)]
    public float frequency_2 = 1;

    [Header("Head Settings")]
    public bool headNod = false;
    [Range(0, 30)]
    public float headNodForce = 20f;
    [Range(0, 30)]
    public float headWobbleForce = 20f;
    [Range(0, 30)]
    public float headShakeForce = 20f;

    [Header("Chest Settings")]
    public bool chestShake = false;
    [Range(0, 1)]
    public float chestBounceForce = 1;
    [Range(0, 30)]
    public float chestSideToSideForce = 20f;
    [Range(0, 30)]
    public float chestFrontBackForce = 20f;
    [Range(0, 30)]
    public float chestShakeForce = 20f;


    [Header("Hip Settings")]
    public bool hipShake = false;
    [Range(0, 1)]
    public float hipBounceForce = 1;
    [Range(0, 30)]
    public float hipSideToSideForce = 20f;
    [Range(0, 30)]
    public float hipFrontBackForce = 20f;
    [Range(0, 30)]
    public float hipShakeForce = 20f;



    

    void Start()
    {
        character = this.GetComponent<CharacterThinker>();
        head = character.bpHolder.BodyPartsName[BodyPartNames.headName];
        chest = character.bpHolder.BodyPartsName[BodyPartNames.chestName];
        hip = character.bpHolder.BodyPartsName[BodyPartNames.hipName];

        leftHand = character.bpHolder.BodyPartsName[BodyPartNames.lhandName];
        rightHand = character.bpHolder.BodyPartsName[BodyPartNames.rhandName];

        leftThigh = character.bpHolder.BodyPartsName[BodyPartNames.lthighName];
        rightThigh = character.bpHolder.BodyPartsName[BodyPartNames.rthighName];

        leftLeg = character.bpHolder.BodyPartsName[BodyPartNames.llegName];
        rightLeg = character.bpHolder.BodyPartsName[BodyPartNames.rlegName];

        //BodyPartNames.rhandName
    }

    // Update is called once per frame
    void Update()
    {
        kickCounter += Time.deltaTime;
        /*if (kickCounter >= 1)
        {
            kickCounter = 0;


        }*/

        float y = this.curve.Evaluate(kickCounter);
        

        print("y value : " + y);
        print("kickCounter value : " + kickCounter);

        if(headNod)
        {
            HeadNod(y);
            //HeadWobble(y);
        }

        if (hipShake)
        {
            HipShake(y);
            //HeadWobble(y);
        }

        if (chestShake)
        {
            ChestShake(y);
            //HeadWobble(y);
        }
    }

    public void HeadNod(float value)
    {
        ConfigurableJoint cJ = head.BodyPartConfigJoint;

        //JointDrive x = head.BodyPartConfigJoint.slerpDrive;
        //x.positionDamper = 100;
        //x.positionSpring = 1000;
        //x.maximumForce = 1000;
        //head.BodyPartConfigJoint.slerpDrive = x;

        //Vector3 jointValue = new Vector3(20f, 0f, 0f);

        float y = this.curve.Evaluate(kickCounter * frequency_1);

        float y1 = this.curve.Evaluate(kickCounter * frequency_2);

        head.BodyPartConfigJoint.targetAngularVelocity = new Vector3(headNodForce * y, headWobbleForce * y1, headShakeForce * y1);

       // CharacterMaintainHeight cMH = head.BodyPartMaintainHeight;
       // CharacterFaceDirection cFD = head.BodyPartFaceDirection;


       

        //cFD.facingDirection = new Vector3(headShakeForce * y1, headNodForce * y, 0);
    }

    public void HipShake(float value)
    {
        

        float y = this.curve.Evaluate(kickCounter * frequency_1);

        float y1 = this.curve.Evaluate(kickCounter * frequency_2);

       


        CharacterMaintainHeight cMH = hip.BodyPartMaintainHeight;
        CharacterFaceDirection cFD = hip.BodyPartFaceDirection;

        cMH.desiredHeight = cMH.initialDesiredHeight + ( y * hipBounceForce );

        cFD.facingDirection = new Vector3(hipShakeForce * y1, 0, -1);

       

        leftThigh.BodyPartConfigJoint.targetAngularVelocity = new Vector3(hipFrontBackForce * y, hipSideToSideForce * y1, 0f);

        

       rightThigh.BodyPartConfigJoint.targetAngularVelocity = new Vector3(hipFrontBackForce * y, hipSideToSideForce * y1, 0f);

    }

    public void ChestShake(float value)
    {


        float y = this.curve.Evaluate(kickCounter * frequency_1);

        float y1 = this.curve.Evaluate(kickCounter * frequency_2);




        CharacterMaintainHeight cMH = chest.BodyPartMaintainHeight;
        CharacterFaceDirection cFD = chest.BodyPartFaceDirection;

        //cMH.desiredHeight = cMH.initialDesiredHeight + (y * chestBounceForce);

        cFD.facingDirection = new Vector3(-chestShakeForce * y1, chestFrontBackForce * y, -1);



        chest.BodyPartConfigJoint.targetAngularVelocity = new Vector3(chestFrontBackForce * y, chestSideToSideForce * y1, 0f);



       // chest.BodyPartConfigJoint.targetAngularVelocity = new Vector3(chestFrontBackForce * y, chestSideToSideForce * y1, 0f);

    }


    public void HeadWobble(float value)
    {
        ConfigurableJoint cJ = head.BodyPartConfigJoint;

        //JointDrive x = head.BodyPartConfigJoint.slerpDrive;
        //x.positionDamper = 100;
        //x.positionSpring = 1000;
        //x.maximumForce = 1000;
        //head.BodyPartConfigJoint.slerpDrive = x;

        //Vector3 jointValue = new Vector3(20f, 0f, 0f);

        float y = this.curve.Evaluate(kickCounter * frequency_1);

        head.BodyPartConfigJoint.targetAngularVelocity = new Vector3(0, headNodForce * y, 0f);
    }
}
