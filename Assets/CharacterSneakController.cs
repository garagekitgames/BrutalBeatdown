using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using SO;
using DG.Tweening;

using UnityEngine.Events;

public class CharacterSneakController : MonoBehaviour
{
	public CharacterThinker character;
    public CharacterMaintainHeight hipHeight;
    public CharacterMaintainHeight lHandHeight;
    public CharacterMaintainHeight rHandHeight;

    public ConfigurableJoint lArm;
    public ConfigurableJoint lFArm;
    public ConfigurableJoint rArm;
    public ConfigurableJoint rFArm;

    public GameObject coverObject;
    public GameObject particleGO;

    public ParticleSystem runParticle;

    public UnityEvent OnCaughtEvent;

    public Rigidbody hipRb;

    public bool playerCaught;
    public bool playerWon;

    public float walkingHeight = 0.6f;
    public float coverObjectHeight = -0.8f;

    public DanceController danceController; 
	// Start is called before the first frame update
	void Start()
    {
		character = GetComponent<CharacterThinker>();
        hipHeight = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartMaintainHeight;
        lHandHeight = character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartMaintainHeight;
        rHandHeight = character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartMaintainHeight;
        lArm = character.bpHolder.BodyPartsName[BodyPartNames.larmName].bodyPartConfigJoint;
        lFArm = character.bpHolder.BodyPartsName[BodyPartNames.lfarmName].bodyPartConfigJoint;
        rArm = character.bpHolder.BodyPartsName[BodyPartNames.rarmName].bodyPartConfigJoint;
        rFArm = character.bpHolder.BodyPartsName[BodyPartNames.rfarmName].bodyPartConfigJoint;
        hipRb = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartRb;
        coverObject = character.transform.FindDeepChild("CoverObject").gameObject;
        particleGO = character.transform.FindDeepChild("RunParticle").gameObject;
        runParticle = particleGO.GetComponent<ParticleSystem>();
        //coverObject.transform.SetParent(null, true);
        danceController = this.GetComponent<DanceController>();
        danceController.enabled = false;
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCaught || playerWon)
        {
            var emission1 = runParticle.emission;
            emission1.rateOverTime = 0;
            return;
        }

        HandleCrouch();

        if(character.isSeen)
        {
            character.speed = 1500;
        }
        else
        {
            character.speed = 1000;
        }
        var emission = runParticle.emission;
        emission.rateOverTime = hipRb.velocity.sqrMagnitude;
    }

    public void HandleCrouch()
    {
        if (character.walking == false)
        {

            hipHeight.desiredHeight = 0;
            lHandHeight.desiredHeight = -3;
            rHandHeight.desiredHeight = -3;
            coverObject.transform.DOLocalMove( new Vector3(0, 0, 0), 0.2f);
            //coverObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);

        }
        else
        {

            hipHeight.desiredHeight = walkingHeight;
            lHandHeight.desiredHeight = 3;
            rHandHeight.desiredHeight = 3;
            coverObject.transform.DOLocalMove( new Vector3(coverObjectHeight, 0, 0), 0.2f);
           // coverObject.transform.DOLocalRotate(new Vector3(0, -15, 0), 1);
        }
    }

    public void OnWon()
    {
        playerWon = true;
        var qDrag = coverObject.GetComponent<QuadraticDrag>();
        qDrag.drag = 0.2f;

        var joint = coverObject.GetComponent<ConfigurableJoint>();

        joint.xMotion = ConfigurableJointMotion.Free;
        joint.yMotion = ConfigurableJointMotion.Free;
        joint.zMotion = ConfigurableJointMotion.Free;

        joint.angularXMotion = ConfigurableJointMotion.Free;
        joint.angularYMotion = ConfigurableJointMotion.Free;
        joint.angularZMotion = ConfigurableJointMotion.Free;


        joint.connectedBody = null;


        Destroy(joint);

        coverObject.GetComponent<Rigidbody>().AddForce(((Vector3.up) * 100 )+ ((Vector3.forward) * 50), ForceMode.Impulse);


        hipHeight.desiredHeight = 1.2f;
        lHandHeight.enabled = false;
        rHandHeight.enabled = false;


        danceController.enabled = true;

        //coverObject.transform.DOLocalMove(new Vector3(coverObjectHeight, 0, 0), 0.2f);

    }
    public void OnCaught()
    {
        playerCaught = true;

        //var joint = coverObject.GetComponent<ConfigurableJoint>();

        //joint.xMotion = ConfigurableJointMotion.Free;
        //joint.yMotion = ConfigurableJointMotion.Free;
        //joint.zMotion = ConfigurableJointMotion.Free;

        //joint.angularXMotion = ConfigurableJointMotion.Free;
        //joint.angularYMotion = ConfigurableJointMotion.Free;
        //joint.angularZMotion = ConfigurableJointMotion.Free;

        //joint.connectedBody = null;

        //Destroy(coverObject.GetComponent<ConfigurableJoint>());
        coverObject.GetComponent<CollisionCheck>().enabled = false;

        coverObject.GetComponent<Rigidbody>().AddForce(10000 * Vector3.up);

        //coverObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //coverObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //var dropPosition = coverObject.transform.position;
        //coverObject.transform.parent = null;
        //coverObject.transform.localPosition = dropPosition;
        //coverObject.transform.SetParent(null, true);

        // coverObject.transform.parent

        //Throw away box and go limp
        lHandHeight.enabled = false;
        rHandHeight.enabled = false;

        //BodyPartMono bodyPart = character.bpHolder.bodyParts[windupPose.bodyPart];

        //JointDrive x = lArm.slerpDrive;
        //x.positionDamper = 0;
        //x.positionSpring = 0;
        //x.maximumForce = 0;
        //lArm.slerpDrive = x;

        //JointDrive x1 = lFArm.slerpDrive;
        //x1.positionDamper = 0;
        //x1.positionSpring = 0;
        //x1.maximumForce = 0;
        //lFArm.slerpDrive = x1;

        //JointDrive x2 = rArm.slerpDrive;
        //x2.positionDamper = 0;
        //x2.positionSpring = 0;
        //x2.maximumForce = 0;
        //rArm.slerpDrive = x2;

        //JointDrive x3 = rFArm.slerpDrive;
        //x3.positionDamper = 0;
        //x3.positionSpring = 0;
        //x3.maximumForce = 0;
        //rFArm.slerpDrive = x3;

        OnCaughtEvent.Invoke();


        

    }
}
