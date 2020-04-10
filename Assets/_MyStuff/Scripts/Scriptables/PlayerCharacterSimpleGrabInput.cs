using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace garagekitgames
{
    [CreateAssetMenu(menuName = "GarageKitGames/Actitons/PlayerSimpleGrabInput")]
    public class PlayerCharacterSimpleGrabInput : CharacterAction
    {


        // Start is called before the first frame update
        public override void OnInitialize(CharacterThinker character)
        {
            base.OnInitialize(character);
            //PickAnAttack(character);
        }

        public override void OnFixedUpdate(CharacterThinker character)
        {
           
        }

        public override void OnUpdate(CharacterThinker character)
        {
            if( !character.health.alive || character.health.knockdown || character.isHurting)// Use character.isGrabbed  in condition to stop grab when the player is grabbed //|| character.isHurting || character.isGrabbed  ||
            {
                character.leftgrab = false;
                character.rightgrab = false;

                character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.grabNow = false;
                if(character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision != null)
                {
                    character.bpHolder.BodyPartsName[BodyPartNames.rhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = false;
                }
                

                character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.grabNow = false;
                if(character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision != null)
                {
                    character.bpHolder.BodyPartsName[BodyPartNames.lhandName].BodyPartGrabCheck.mycollision.GetComponent<InteractableObject>().isGrabbed = false;

                }

                character.rightThrow = true;
                character.leftThrow = true;
                return;
            }
            character.rightStickInputDirection = new Vector3(character.player.GetAxisRaw("LookHorizontal"), character.player.GetAxisRaw("LookVertical"), 0);
            if (character.rightStickInputDirection != Vector3.zero)
            {
                //Debug.Log("Before " + character.rightStickInputDirection);
                character.rightStickInputDirection.Normalize();
                if (true) // camera relative movement
                {
                    character.rightStickInputDirection = Camera.main.transform.TransformDirection(character.rightStickInputDirection);
                    character.rightStickInputDirection.z = 0f;
                    character.rightStickInputDirection.y *= 2;
                    //float magnitude = character.rightStickInputDirection.sqrMagnitude;
                }
                //Debug.Log("After " + character.rightStickInputDirection);
            }

            //When left stick is not used, use the right stick input for x and z
            if (character.inputDirection.x > 0.5f || character.inputDirection.x < -0.5f)
            {
                character.xForceDirection = character.inputDirection.x;
            }
            else
            {
                // xForceDirection = mouseX;
                character.xForceDirection = character.rightStickInputDirection.x;
            }

            if (character.inputDirection.z > 0.5f || character.inputDirection.z < -0.5f)
            {
                character.zForceDirection = character.inputDirection.z;
            }
            else
            {
                character.zForceDirection = character.rightStickInputDirection.z;
            }


            Vector3 horizontalVelocity = character.bpHolder.BodyPartsName[BodyPartNames.chestName].BodyPartRb.velocity;

            //character.pullForceVector = new Vector3(character.xForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), character.rightStickInputDirection.y * character.verticalPullForce, character.zForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));

            
            //for simple slam 
            //character.pullForceVector = new Vector3(character.xForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), (-2) * character.verticalPullForce, character.zForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));


            

            if(character.isGrabbing)
            {
                //for Simple lift 
                if(!character.slamming)
                {
                    if(character.rightStickInputDirection.y != 0)
                    {
                        character.pullForceVector = new Vector3(character.xForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), character.rightStickInputDirection.y * character.verticalPullForce, character.zForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));

                    }
                    else
                    {
                        character.pullForceVector = new Vector3(character.xForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), (0.8f) * character.verticalPullForce, character.zForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));

                    }

                }
                else
                {

                }

                if (character.player.GetButtonDown("SimpleGrab"))
                {

                    character.simpleThrwoButtonClickTimer = 0f;
                    //character.leftgrab = false;

                }

                else if (character.player.GetButton("SimpleGrab"))
                {
                    //print("Key Held");
                    if (character.simpleThrwoButtonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
                    {
                        character.simpleThrwoButtonClickTimer += Time.deltaTime; //this records how long the key is held down
                    }
                    //if (character.simpleThrwoButtonClickTimer > 0.2f && character.leftgrab == false)    // if the button is pressed for more than 0.2 seconds grab
                    //{
                    //    character.leftgrab = true;


                    //}
                    //else    // else ready the arm, pull back for punch / grab
                    //{
                    //    //call punch action readying
                    //}

                }

                else if (character.player.GetButtonUp("SimpleGrab"))
                {
                    //print("Key Released");
                    if (character.simpleThrwoButtonClickTimer <= 0.2f ) // as long as key is held down increase time, this records how long the key is held down
                    {
                        character.slam = true;
                        character.pullForceVector = new Vector3(character.xForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.x) * 4)), (-2) * character.verticalPullForce, character.zForceDirection * (character.horizontalPullForce / (1 + Mathf.Abs(horizontalVelocity.z) * 4)));
                        character.DoSimpleSlam(0.5f);
                        

                    }
                    else
                    {
                    character.leftThrow = true;
                    character.simpleThrwoButtonClickTimer = 0f;

                    character.rightThrow = true;
                    character.simpleThrwoButtonClickTimer = 0f;

                    }
                }
            }

            else
            {
                if (character.player.GetButtonDown("SimpleGrab"))
                {

                    character.leftbuttonClickTimer = 0f;
                    character.leftgrab = false;

                    character.rightbuttonClickTimer = 0f;
                    character.rightgrab = false;
                    //var gad = character.grab
                    IEnumerator coroutine = character.DoSimpleGrab(character.grabAttemptDuration);
                    character.StartCoroutine(coroutine);

                }
            }

           /* else if (character.player.GetButton("SimpleGrab"))
            {
                //print("Key Held");
                if (character.leftbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
                {
                    character.leftbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
                }
                if (character.leftbuttonClickTimer > 0.2f && character.leftgrab == false)    // if the button is pressed for more than 0.2 seconds grab
                {
                    character.leftgrab = true;


                }
                else    // else ready the arm, pull back for punch / grab
                {
                    //call punch action readying
                }


                if (character.rightbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
                {
                    character.rightbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
                }
                if (character.rightbuttonClickTimer > 0.2f && character.rightgrab == false)    // if the button is pressed for more than 0.2 seconds grab
                {
                    character.rightgrab = true;


                }
                else    // else ready the arm, pull back for punch / grab
                {
                    //call punch action readying
                }

            }

            else if (character.player.GetButtonUp("SimpleGrab"))
            {
                
                character.leftThrow = true;
                character.leftbuttonClickTimer = 0f;

                character.rightThrow = true;
                character.rightbuttonClickTimer = 0f;

               
            }*/


            if (character.player.GetButtonDown("GrabLeft"))
            {

                character.leftbuttonClickTimer = 0f;
                character.leftgrab = false;

            }

            else if (character.player.GetButton("GrabLeft"))
            {
                //print("Key Held");
                if (character.leftbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
                {
                    character.leftbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
                }
                if (character.leftbuttonClickTimer > 0.2f && character.leftgrab == false)    // if the button is pressed for more than 0.2 seconds grab
                {
                    character.leftgrab = true;


                }
                else    // else ready the arm, pull back for punch / grab
                {
                    //call punch action readying
                }

            }

            else if (character.player.GetButtonUp("GrabLeft"))
            {
                //print("Key Released");
                /*if (character.leftbuttonClickTimer <= 0.2f && !leftpunch) // as long as key is held down increase time, this records how long the key is held down
                {
                    character.leftpunch = true;
                    leftbuttonClickTimer = 0f;
                }*/
                //else
                //{
                character.leftThrow = true;
                character.leftbuttonClickTimer = 0f;

                //}
            }


            if (character.player.GetButtonDown("GrabRight"))
            {

                character.rightbuttonClickTimer = 0f;
                character.rightgrab = false;

            }

            else if (character.player.GetButton("GrabRight"))
            {
                //print("Key Held");
                if (character.rightbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
                {
                    character.rightbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
                }
                if (character.rightbuttonClickTimer > 0.2f && character.rightgrab == false)    // if the button is pressed for more than 0.2 seconds grab
                {
                    character.rightgrab = true;


                }
                else    // else ready the arm, pull back for punch / grab
                {
                    //call punch action readying
                }

            }

            else if (character.player.GetButtonUp("GrabRight"))
            {
                //print("Key Released");
                /*if (character.leftbuttonClickTimer <= 0.2f && !leftpunch) // as long as key is held down increase time, this records how long the key is held down
                {
                    character.leftpunch = true;
                    leftbuttonClickTimer = 0f;
                }*/
                //else
                //{
                character.rightThrow = true;
                character.rightbuttonClickTimer = 0f;

                //}
            }

        }
    }
}

