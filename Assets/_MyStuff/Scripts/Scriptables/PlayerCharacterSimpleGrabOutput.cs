using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace garagekitgames
{
    [CreateAssetMenu(menuName = "GarageKitGames/Actitons/PlayerSimpleGrabOutput")]
    public class PlayerCharacterSimpleGrabOutput : CharacterAction
    {
        public override void OnInitialize(CharacterThinker character)
        {
            base.OnInitialize(character);
            
        }

        public override void OnFixedUpdate(CharacterThinker character)
        {
            if (character.slam)
            {

                character.slamTimer += Time.deltaTime;
                if (character.slamTimer < character.slamTime * 0.2f)
                {

                    //Prepare slam

                    Vector3 horizontalVelocity = character.bpHolder.BodyPartsName[BodyPartNames.chestName].BodyPartRb.velocity;
                    character.pullForceVector = new Vector3(character.xForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), (2.5f) * character.verticalPullForce, character.zForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));
                    character.bpHolder.bodyPartsName[BodyPartNames.hipName].BodyPartMaintainHeight.desiredHeight = character.bpHolder.bodyPartsName[BodyPartNames.hipName].BodyPartMaintainHeight.initialDesiredHeight + 0.5f;

                }
                if (character.slamTimer >= character.slamTime * 0.4f && character.slamTimer < character.slamTime * 1f)
                {
                    Vector3 horizontalVelocity = character.bpHolder.BodyPartsName[BodyPartNames.chestName].BodyPartRb.velocity;
                    character.pullForceVector = new Vector3(character.xForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), (-2) * character.verticalPullForce, character.zForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));

                    /*character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);

                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);


                    character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);
                    character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                    */


                    //character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(-(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 50) + (Vector3.down * 200) * Time.deltaTime, ForceMode.VelocityChange);
                    if(character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision)
                    {
                        character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce((character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 2) + (Vector3.down * 150) * Time.deltaTime, ForceMode.VelocityChange);

                    }

                    if (character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision)
                    {
                        //character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(-(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 50) + (Vector3.down * 200) * Time.deltaTime, ForceMode.VelocityChange);
                        character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce((character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 2) + (Vector3.down * 150) * Time.deltaTime, ForceMode.VelocityChange);

                    }

                    character.bpHolder.bodyPartsName[BodyPartNames.hipName].BodyPartMaintainHeight.desiredHeight = 0f;
                }
                /*if (character.slamTimer >= character.slamTime * 0.6f && character.slamTimer < character.slamTime * 1f)
                {

                }*/
                if (character.slamTimer >= character.slamTime * 1f)
                {

                    character.bpHolder.bodyPartsName[BodyPartNames.hipName].BodyPartMaintainHeight.desiredHeight = character.bpHolder.bodyPartsName[BodyPartNames.hipName].BodyPartMaintainHeight.initialDesiredHeight;

                    character.slam = false;
                    character.slamTimer = 0f;

                }



               
            }

            if (character.leftThrow)
            {

                if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision != null)
                {
                    if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>() != null)
                    {
                        if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().partOfRagdoll)
                        {
                             character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = false;
                            character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce((Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) + Vector3.up * 3000) * Time.deltaTime, ForceMode.VelocityChange);
                            //character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) * (character.simpleThrwoButtonClickTimer) * Time.deltaTime, ForceMode.VelocityChange);

                        }
                        //else
                        //{
                        //    character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = false;
                        //    character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce((Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) + Vector3.up * 3000) * Time.deltaTime, ForceMode.VelocityChange);
                            
                        //}

                    }

                    character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow / character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow) * Time.deltaTime, ForceMode.VelocityChange);

                }
                character.leftThrow = false;
                character.leftgrab = false;
                character.isGrabbing = false;
                character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.grabNow = false;

                JointDrive x = character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartConfigJoint.slerpDrive;
                x.positionDamper = 100;
                x.positionSpring = 1000;
                x.maximumForce = 1000;
                character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartConfigJoint.slerpDrive = x;

                //Vector3 jointValue = new Vector3(20f, 0f, 0f);

                character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartConfigJoint.targetAngularVelocity = new Vector3(-15f, -10f, 5f);

                //BodyPartMono LFARMbodyPart = character.bpHolder.bodyPartsName[BodyPartNames.larmName];
                JointDrive x1 = character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartConfigJoint.slerpDrive;
                x1.positionDamper = 100;
                x1.positionSpring = 1000;
                x1.maximumForce = 1000;
                character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartConfigJoint.slerpDrive = x1;

                //Vector3 jointValue = new Vector3(20f, 0f, 0f);

                character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartConfigJoint.targetAngularVelocity = new Vector3(-40f, 4f, 6f);

            }

            if (character.rightThrow)
            {
                if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision != null)
                {

                    if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision != null)
                    {
                        if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>() != null)
                        {
                            if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().partOfRagdoll)
                            {
                               character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = false;
                                character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce((Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) + Vector3.up * 3000) * Time.deltaTime, ForceMode.VelocityChange);
                                //character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) * (character.simpleThrwoButtonClickTimer) * Time.deltaTime, ForceMode.VelocityChange);

                            }

                            //else
                            //{
                            //    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = false;
                            //    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce((Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) + Vector3.up * 3000) * Time.deltaTime, ForceMode.VelocityChange);

                            //}

                        }
                    }

                    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow) * Time.deltaTime, ForceMode.VelocityChange);

                }
                character.rightThrow = false;
                character.rightgrab = false;
                character.isGrabbing = false;
                character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.grabNow = false;

                JointDrive x = character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartConfigJoint.slerpDrive;
                x.positionDamper = 100;
                x.positionSpring = 1000;
                x.maximumForce = 1000;
                character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartConfigJoint.slerpDrive = x;

                //Vector3 jointValue = new Vector3(20f, 0f, 0f);

                character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartConfigJoint.targetAngularVelocity = new Vector3(15f, 10f, 5f);

                //BodyPartMono LFARMbodyPart = character.bpHolder.bodyPartsName[BodyPartNames.larmName];
                JointDrive x1 = character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartConfigJoint.slerpDrive;
                x1.positionDamper = 100;
                x1.positionSpring = 1000;
                x1.maximumForce = 1000;
                character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartConfigJoint.slerpDrive = x1;

                //Vector3 jointValue = new Vector3(20f, 0f, 0f);

                character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartConfigJoint.targetAngularVelocity = new Vector3(40f, -4f, 6f);

            }


            if (character.leftgrab)
            {
                //Loosen hand :
                //JointDrive x = armsJoint[1].slerpDrive;
                //x.positionDamper = 0f;
                //x.positionSpring = 0f;
                //x.maximumForce = 0f;
                //armsJoint[1].slerpDrive = x;

                //Vector3 jointValue = new Vector3(0f, 0f, 0f);

                //armsJoint[1].targetAngularVelocity = jointValue;

                //JointDrive x1 = forearmsJoint[1].slerpDrive;
                //x1.positionDamper = 0f;
                //x1.positionSpring = 0f;
                //x1.maximumForce = 0f;
                //forearmsJoint[1].slerpDrive = x1;

                //Vector3 jointValue1 = new Vector3(0f, 0f, 0f);

                //BodyPartMono LARMbodyPart = character.bpHolder.bodyPartsName[BodyPartNames.larmName];
                JointDrive x = character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartConfigJoint.slerpDrive;
                x.positionDamper = 0;
                x.positionSpring = 0;
                x.maximumForce = 0;
                character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartConfigJoint.slerpDrive = x;

                //Vector3 jointValue = new Vector3(20f, 0f, 0f);

                character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartConfigJoint.targetAngularVelocity = new Vector3(0f, 0f, 0f);

                //BodyPartMono LFARMbodyPart = character.bpHolder.bodyPartsName[BodyPartNames.larmName];
                JointDrive x1 = character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartConfigJoint.slerpDrive;
                x1.positionDamper = 0;
                x1.positionSpring = 0;
                x1.maximumForce = 0;
                character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartConfigJoint.slerpDrive = x1;

                //Vector3 jointValue = new Vector3(20f, 0f, 0f);

                character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartConfigJoint.targetAngularVelocity = new Vector3(0f, 0f, 0f);


                if (character.grabObjectOfInterest != null)
                {
                    Vector3 a;
                    if (true)
                    {

                        if (character.grabObjectOfInterest)
                        {
                            a = (character.grabObjectOfInterest.cachedCollider.ClosestPoint(character.bpHolder.bodyPartsName[BodyPartNames.head].BodyPartRb.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position).normalized;
                           // attackTarget = a;
                        }
                        else
                        {
                            a = (character.target - character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position).normalized;


                        }

                       
                          
                    }
                    else
                    {
                      
                    }

                    //Move hand towards target
                     character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-(a * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                     character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(a * 200f * Time.deltaTime, ForceMode.VelocityChange);

                    
                    character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.grabNow = true;

                    //character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.GrabNow(character.grabObjectOfInterest);

                    if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.hasJoint)
                    {
                        character.isGrabbing = true;

                        if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision != null)
                        {
                            if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>() != null)
                            {
                                if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().partOfRagdoll)
                                {
                                    
                                   character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = true;
                                    character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 0.8f) * Time.deltaTime, ForceMode.VelocityChange);

                                    character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                }

                                
                                else
                                {
                                    //character.isGrabbing = false;
                                    character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);
                                    character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                  
                                }
                               
                            }

                        }

                       
                    }
                    
                    character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-((character.rightStickInputDirection) * 300f) * Time.deltaTime, ForceMode.VelocityChange);

                    character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce((character.rightStickInputDirection) * 300f * Time.deltaTime, ForceMode.VelocityChange);

                   
                }

                else
                {
                    //Move hands in front of body 
                    character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                    character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 200f * Time.deltaTime, ForceMode.VelocityChange);

                }


            }

            if (character.rightgrab)
            {

                JointDrive x = character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartConfigJoint.slerpDrive;
                x.positionDamper = 0;
                x.positionSpring = 0;
                x.maximumForce = 0;
                character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartConfigJoint.slerpDrive = x;

                //Vector3 jointValue = new Vector3(20f, 0f, 0f);

                character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartConfigJoint.targetAngularVelocity = new Vector3(0f, 0f, 0f);

                //BodyPartMono LFARMbodyPart = character.bpHolder.bodyPartsName[BodyPartNames.larmName];
                JointDrive x1 = character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartConfigJoint.slerpDrive;
                x1.positionDamper = 0;
                x1.positionSpring = 0;
                x1.maximumForce = 0;
                character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartConfigJoint.slerpDrive = x1;

                //Vector3 jointValue = new Vector3(20f, 0f, 0f);

                character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartConfigJoint.targetAngularVelocity = new Vector3(0f, 0f, 0f);

                if (character.grabObjectOfInterest != null)
                {
                    Vector3 a;
                    if (true)
                    {

                        if (character.grabObjectOfInterest)
                        {
                            a = (character.grabObjectOfInterest.cachedCollider.ClosestPoint(character.bpHolder.bodyPartsName[BodyPartNames.head].BodyPartRb.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.transform.position).normalized;
                            // attackTarget = a;
                        }
                        else
                        {
                            a = (character.target - character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.transform.position).normalized;


                        }

                       

                    }
                    else
                    {
                       
                    }


                    //Move hand towards target
                    character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-(a * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(a * 200f * Time.deltaTime, ForceMode.VelocityChange);

                    
                    

                    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.grabNow = true;

                    //character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.GrabNow(character.grabObjectOfInterest);

                    if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.hasJoint)
                    {
                        character.isGrabbing = true;
                        if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision != null)
                        {
                            if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>() != null)
                            {
                                if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().partOfRagdoll)
                                {
                                    
                                    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = true;
                                    character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 0.8f) * Time.deltaTime, ForceMode.VelocityChange);

                                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                 }

                               
                                else
                                {
                                    //character.isGrabbing = false;
                                    character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);
                                    
                                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);

                                }
                               

                            }


                        }

                    }
                    
                    character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-((character.rightStickInputDirection) * 300f) * Time.deltaTime, ForceMode.VelocityChange);

                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce((character.rightStickInputDirection) * 300f * Time.deltaTime, ForceMode.VelocityChange);
                 }
                else
                {
                    //Move hands in front of body 
                    character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 200f * Time.deltaTime, ForceMode.VelocityChange);

                }

            }
            
        }

        public override void OnUpdate(CharacterThinker character)
        {
            
        }
    }

}
