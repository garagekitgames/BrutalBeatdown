using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace garagekitgames
{
    [CreateAssetMenu(menuName = "GarageKitGames/Actitons/PlayerGrabOutput2")]
    public class PlayerGrabOutput2 : CharacterAction
    {
        public override void OnInitialize(CharacterThinker character)
        {
            base.OnInitialize(character);
            //PickAnAttack(character);
        }

        public override void OnFixedUpdate(CharacterThinker character)
        {
            if (character.leftThrow)
            {

                if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision != null)
                {
                    //print("LEft Climbing : " + climbing);
                    if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>() != null)
                    {
                        if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().partOfRagdoll)
                        {
                            // character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * 1500f, 3000f) * Time.deltaTime, ForceMode.VelocityChange);
                            character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = false;
                            character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);
                        }
                        /*else if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().grabModifier == InteractableObject.Grab.Climb)
                        {
                            if (climbing)
                            {
                                climbing = false;

                                //chestBody.AddForce(Vector3.ClampMagnitude(chestBody.velocity * chestBody.velocity.magnitude * character.minThrow * character.humanThrowScale / chestBody.mass, character.maxThrow * character.humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);
                                //chestBody.AddForce(Vector3.ClampMagnitude(chestBody.velocity * chestBody.velocity.magnitude * 3000 , 8000) * Time.deltaTime, ForceMode.VelocityChange);
                                chestBody.AddForce((Vector3.up * 5000f + myCharacterMovement.inputDirection * 5000f) * Time.deltaTime, ForceMode.VelocityChange);
                            }
                        }*/
                    }

                    character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow / character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow) * Time.deltaTime, ForceMode.VelocityChange);

                }
                character.leftThrow = false;
                character.leftgrab = false;
                character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.grabNow = false;
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
                                /* print("Throw Force : " + character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * 500f / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass);
                                 print("Throw Force MAgnitude: " + Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * 500f / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, 1000f));
                                 */ //character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity* 1500f , 3000f) * Time.deltaTime, ForceMode.VelocityChange);
                                character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = false;
                                character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);
                            }
                            /*else if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().grabModifier == InteractableObject.Grab.Climb)
                            {
                                if (climbing)
                                {
                                    climbing = false;
                                    print("Right Climbing : " + climbing);
                                    //chestBody.AddForce(Vector3.ClampMagnitude(chestBody.velocity * chestBody.velocity.magnitude * character.minThrow * character.humanThrowScale / chestBody.mass, character.maxThrow * character.humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);
                                    //chestBody.AddForce(Vector3.ClampMagnitude(chestBody.velocity * chestBody.velocity.magnitude * 3000 , 8000) * Time.deltaTime, ForceMode.VelocityChange);
                                    chestBody.AddForce((Vector3.up * 5000f + myCharacterMovement.inputDirection * 5000f) * Time.deltaTime, ForceMode.VelocityChange);
                                }
                            }*/
                        }
                    }

                    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow) * Time.deltaTime, ForceMode.VelocityChange);

                }
                character.rightThrow = false;
                character.rightgrab = false;
                character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.grabNow = false;
            }


            if (character.leftgrab)
            {

                ////Move hands in front of body 
                //character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                //character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 200f * Time.deltaTime, ForceMode.VelocityChange);

                Debug.Log("leftgrab After pullForceVector : " + character.pullForceVector);
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

                        //a = (target.cachedCollider.ClosestPoint(headBody.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position).normalized;
                           // grabPointLeft = (target.cachedCollider.ClosestPoint(headBody.transform.position));

                          
                    }
                    else
                    {
                       // a = (target.cachedCollider.ClosestPoint(character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position).normalized;

                    }


                    character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-(a * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                    character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(a * 200f * Time.deltaTime, ForceMode.VelocityChange);
                    //if(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.collidingBody == target)
                    //{
                    character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.grabNow = true;


                    if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.hasJoint)
                    {
                        //print(inputDirection);
                        /* character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-((character.pullForceVector) * 0.67f) * Time.deltaTime, ForceMode.VelocityChange);

                         character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                         */
                        if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision != null)
                        {
                            if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>() != null)
                            {
                                if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().partOfRagdoll)
                                {
                                    /* print("Throw Force : " + character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * 500f / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass);
                                     print("Throw Force MAgnitude: " + Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * 500f / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, 1000f));
                                     */ //character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity* 1500f , 3000f) * Time.deltaTime, ForceMode.VelocityChange);
                                    character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = true;
                                    character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 0.8f) * Time.deltaTime, ForceMode.VelocityChange);

                                    character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                    //character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);
                                }

                                /*else if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().grabModifier == InteractableObject.Grab.Climb)
                                {
                                    climbing = true;

                                    if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.hasJoint)
                                    {
                                        character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-((climbForceVector) * 0.3f) * Time.deltaTime, ForceMode.VelocityChange);
                                        chestBody.AddForce(-((climbForceVector) * 0.3f) * Time.deltaTime, ForceMode.VelocityChange);
                                        character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(climbForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                    }
                                    else
                                    {
                                        character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-((climbForceVector) * 0.5f) * Time.deltaTime, ForceMode.VelocityChange);
                                        chestBody.AddForce(-((climbForceVector) * 0.5f) * Time.deltaTime, ForceMode.VelocityChange);
                                        character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(climbForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                    }



                                }*/
                                else
                                {
                                     character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);
                                    //chestBody.AddForce(-((character.pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);
                                    character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                    /* print("character.pullForceVector Force : " + character.pullForceVector * 0.5f * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass / 10f);

                                     character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-((character.pullForceVector) * 0.3f * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass / 10f) * Time.deltaTime, ForceMode.VelocityChange);

                                     character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce(character.pullForceVector * 0.5f * character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass / 10f  * Time.deltaTime, ForceMode.VelocityChange);
                                     */
                                }
                                /*else
                                {
                                    climbing = false;
                                }*/
                            }

                        }

                        //forearmsBody[0].AddForce(-(inputDirection) * 500f * Time.deltaTime, ForceMode.VelocityChange);
                        //chestBody.AddForce((inputDirection) * 1000f * Time.deltaTime, ForceMode.VelocityChange);
                        //if (speed > 1f)
                        //{
                        //    chestBody.velocity = chestBody.velocity.normalized * 1f;
                        //}

                    }
                    //else
                    //{


                    //character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartRb.AddForce(-((character.rightStickInputDirection) * 300f) * Time.deltaTime, ForceMode.VelocityChange);

                    //character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce((character.rightStickInputDirection) * 300f * Time.deltaTime, ForceMode.VelocityChange);



                    // }
                    //}
                    //character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.AddForce((target.ClosestPoint(character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position).normalized * 150 * Time.smoothDeltaTime  , ForceMode.VelocityChange);

                }


            }

            if (character.rightgrab)
            {

                ////Move hands in front of body 
                //character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                //character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward * 200f * Time.deltaTime, ForceMode.VelocityChange);
                Debug.Log("rightgrab After pullForceVector : " + character.pullForceVector);

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

                        //a = (target.cachedCollider.ClosestPoint(headBody.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position).normalized;
                        // grabPointLeft = (target.cachedCollider.ClosestPoint(headBody.transform.position));


                    }
                    else
                    {
                        // a = (target.cachedCollider.ClosestPoint(character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartRb.transform.position).normalized;

                    }

                    /*grabPointRight = (target.ClosestPoint(headBody.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.transform.position);
                    grabPointRight.y += 1f;*/

                    character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-(a * 200f) * Time.deltaTime, ForceMode.VelocityChange);
                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(a * 200f * Time.deltaTime, ForceMode.VelocityChange);

                    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.grabNow = true;
                    //sphere.transform.position = (target.ClosestPoint(character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.transform.position);

                    //if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.collidingBody == target)
                    //{
                    if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.hasJoint)
                    {
                        //character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-((inputDirection) * 670f) * Time.deltaTime, ForceMode.VelocityChange);

                        // character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce((inputDirection) * 1000f * Time.deltaTime, ForceMode.VelocityChange);
                        /* character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 0.67f) * Time.deltaTime, ForceMode.VelocityChange);

                         character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                         */
                        if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision != null)
                        {
                            if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>() != null)
                            {
                                if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().partOfRagdoll)
                                {
                                    /* print("Throw Force : " + character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * 500f / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass);
                                     print("Throw Force MAgnitude: " + Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * 500f / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, 1000f));
                                     */ //character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity* 1500f , 3000f) * Time.deltaTime, ForceMode.VelocityChange);
                                        // character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed

                                    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = true;
                                    character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 0.8f) * Time.deltaTime, ForceMode.VelocityChange);

                                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                    //character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().AddForce(Vector3.ClampMagnitude(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().velocity.magnitude * character.minThrow * character.humanThrowScale / character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass, character.maxThrow * character.humanThrowScale) * Time.deltaTime, ForceMode.VelocityChange);
                                }

                               /* else if (character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().grabModifier == InteractableObject.Grab.Climb)
                                {
                                    climbing = true;
                                    if (character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.hasJoint)
                                    {
                                        character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-((climbForceVector) * 0.3f) * Time.deltaTime, ForceMode.VelocityChange);
                                        chestBody.AddForce(-((climbForceVector) * 0.3f) * Time.deltaTime, ForceMode.VelocityChange);
                                        character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(climbForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                    }
                                    else
                                    {
                                        character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-((climbForceVector) * 0.5f) * Time.deltaTime, ForceMode.VelocityChange);
                                        chestBody.AddForce(-((climbForceVector) * 0.5f) * Time.deltaTime, ForceMode.VelocityChange);
                                        character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(climbForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);
                                    }




                                }*/
                                else
                                {
                                    character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);
                                    //chestBody.AddForce(-((character.pullForceVector) * 1f) * Time.deltaTime, ForceMode.VelocityChange);
                                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.pullForceVector * 1f * Time.deltaTime, ForceMode.VelocityChange);

                                    /*character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-((character.pullForceVector) * 0.3f * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass / 10f) * Time.deltaTime, ForceMode.VelocityChange);

                                    character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce(character.pullForceVector * 0.5f * character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<Rigidbody>().mass / 10f * Time.deltaTime, ForceMode.VelocityChange);
                                */
                                }
                                /*else
                                {
                                    climbing = false;
                                }*/

                            }


                        }

                        //character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartRb.AddForce(-(inputDirection) * 500f * Time.deltaTime, ForceMode.VelocityChange);
                        //chestBody.AddForce((inputDirection) * 1000f * Time.deltaTime, ForceMode.VelocityChange);
                    }
                    // else
                    //{

                    //character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartRb.AddForce(-((character.rightStickInputDirection) * 300f) * Time.deltaTime, ForceMode.VelocityChange);

                    //character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce((character.rightStickInputDirection) * 300f * Time.deltaTime, ForceMode.VelocityChange);


                    // }
                    //}
                    // character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.AddForce((target.ClosestPoint(character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.transform.position) - character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartRb.transform.position).normalized * 150 * Time.smoothDeltaTime, ForceMode.VelocityChange);

                }


            }
        }

        public override void OnUpdate(CharacterThinker character)
        {
            
        }
    }

}
