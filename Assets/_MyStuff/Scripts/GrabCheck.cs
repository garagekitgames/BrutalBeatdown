using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;

public class GrabCheck : MonoBehaviour {
    public bool hasJoint;
    public bool grabNow;
    public Collider targetCollider;
    public Collider mycollision;
    public CharacterThinker character;
   // public ConfigurableJoint childConfigJoint;
    //public GameObject childTransform;
    public bool grabSuccess =  false;
    public ConfigurableJoint test;
    // Use this for initialization
    void Start () {

        character = transform.root.GetComponent<CharacterThinker>();
        //childTransform = transform.GetChild(0).gameObject;

        //if(childTransform)
        //    childConfigJoint = childTransform.GetComponent<ConfigurableJoint>();
        

    }
	
	// Update is called once per frame
	void Update () {

        /*if(myForceTest.target)
        {
            targetCollider = myForceTest.target;
        }*/
        

        if(!grabNow)
        {
            Destroy(test);
            hasJoint = false;
            mycollision = null;
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        //mycollision = collision.collider;
        //if (collision.transform.root != transform.root && collision.gameObject.GetComponent<Rigidbody>() != null && !hasJoint && grabNow && targetCollider == collision.collider)

        if (collision.transform.root != transform.root && collision.gameObject.GetComponent<InteractableObject>() != null && !hasJoint && grabNow)
        {
            if(collision.gameObject.GetComponent<InteractableObject>().priorityModifier != InteractableObject.Priority.Ignore)
            {
                test = gameObject.AddComponent<ConfigurableJoint>(); ;
                // childConfigJoint.connectedBody = collision.rigidbody;
                grabSuccess = true;
                test.connectedBody = collision.rigidbody;

                //test.connectedBody = collision.rigidbody;
                //this.actor.controlHandeler.leftCanClimb = true;
                test.xMotion = ConfigurableJointMotion.Locked;
                test.yMotion = ConfigurableJointMotion.Locked;
                test.zMotion = ConfigurableJointMotion.Locked;

                test.angularXMotion = ConfigurableJointMotion.Limited;
                test.angularYMotion = ConfigurableJointMotion.Limited;
                test.angularZMotion = ConfigurableJointMotion.Limited;

                //test.xMotion = ConfigurableJointMotion.Limited;
                //test.yMotion = ConfigurableJointMotion.Limited;
                //test.zMotion = ConfigurableJointMotion.Limited;
                //SoftJointLimit testLinearLimit = new SoftJointLimit();
                //testLinearLimit.limit = 0.001f;
                //testLinearLimit.bounciness = 1f;
                //test.linearLimit = testLinearLimit;

                //SoftJointLimitSpring testLinearLimitSpring = new SoftJointLimitSpring();
                //testLinearLimitSpring.spring = 1000;
                //testLinearLimitSpring.damper = 1;
                //test.linearLimitSpring = testLinearLimitSpring;

                test.enablePreprocessing = false;
                test.projectionDistance = 0.1f;
                test.projectionAngle = 180f;
                test.projectionMode = JointProjectionMode.PositionAndRotation;
                test.enableCollision = false;

                /* test.breakForce = 30000f;
                 test.breakTorque = 30000f;*/
                /*this.actor.bodyHandeler.leftGrabRigidbody = collisionRigidbody;
                this.actor.bodyHandeler.leftGrabInteractable = collisionInteractable;*/

                mycollision = collision.collider;

                hasJoint = true;
            }
            
        }
        else
        {

            grabSuccess = false;
        }
    }

    public void GrabNow(InteractableObject collision)
    {
        //mycollision = collision.collider;
        //if (collision.transform.root != transform.root && collision.gameObject.GetComponent<Rigidbody>() != null && !hasJoint && grabNow && targetCollider == collision.collider)

        if (collision.cachedTransform.root != transform.root && collision != null && !hasJoint && grabNow)
        {
             this.transform.position = collision.cachedTransform.position;

            test = gameObject.AddComponent<ConfigurableJoint>(); ;
            // childConfigJoint.connectedBody = collision.rigidbody;
            grabSuccess = true;
            test.connectedBody = collision.cachedRigidbody;

            //test.connectedBody = collision.rigidbody;
            //this.actor.controlHandeler.leftCanClimb = true;
            test.xMotion = ConfigurableJointMotion.Locked;
            test.yMotion = ConfigurableJointMotion.Locked;
            test.zMotion = ConfigurableJointMotion.Locked;

            test.angularXMotion = ConfigurableJointMotion.Limited;
            test.angularYMotion = ConfigurableJointMotion.Limited;
            test.angularZMotion = ConfigurableJointMotion.Limited;

            //test.xMotion = ConfigurableJointMotion.Limited;
            //test.yMotion = ConfigurableJointMotion.Limited;
            //test.zMotion = ConfigurableJointMotion.Limited;
            //SoftJointLimit testLinearLimit = new SoftJointLimit();
            //testLinearLimit.limit = 0.001f;
            //testLinearLimit.bounciness = 1f;
            //test.linearLimit = testLinearLimit;

            //SoftJointLimitSpring testLinearLimitSpring = new SoftJointLimitSpring();
            //testLinearLimitSpring.spring = 1000;
            //testLinearLimitSpring.damper = 1;
            //test.linearLimitSpring = testLinearLimitSpring;

            test.enablePreprocessing = false;
            test.projectionDistance = 0.1f;
            test.projectionAngle = 180f;
            test.projectionMode = JointProjectionMode.PositionAndRotation;
            test.enableCollision = false;

            /* test.breakForce = 30000f;
             test.breakTorque = 30000f;*/
            /*this.actor.bodyHandeler.leftGrabRigidbody = collisionRigidbody;
            this.actor.bodyHandeler.leftGrabInteractable = collisionInteractable;*/

            mycollision = collision.cachedCollider;

            hasJoint = true;
        }
        else
        {

            grabSuccess = false;
        }
    }
}
