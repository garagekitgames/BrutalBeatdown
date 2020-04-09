using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using garagekitgames;

public class SwitchKick : MonoBehaviour {

    public Vector3 inputDirection;
    public CharacterInput input;

    public float switchSpeed = 20f;

    public CharacterFaceDirection hipFacing;

    public CharacterThinker character;

    public Vector3 rightStickInputDirection;

    // Use this for initialization
    void Start () {

        //input = GetComponent<CharacterInput>();
        //hipFacing = GetComponent<CharacterFaceDirection>();
        character = GetComponent<CharacterThinker>();
    }
	
	// Update is called once per frame
	void Update () {

        
        inputDirection = Vector3.zero;
        if (input.RHoldRight())
        {
            inputDirection += Vector3.right;
        }
        if (input.RHoldLeft())
        {
            inputDirection += Vector3.left;
        }
        if (input.RHoldUp())
        {
            inputDirection += Vector3.forward;
        }
        if (input.RHoldDown())
        {
            inputDirection += Vector3.back;
        }
        if (inputDirection != Vector3.zero)
        {
            // *** MOVE BASED ON INPUT DIRECTION ****
            //
            inputDirection.Normalize();
            /*if (true)
            {
                inputDirection = Camera.main.transform.TransformDirection(inputDirection);
                inputDirection.y = 0.0f;
            }*/

        }
        else
        {
            
        }

        rightStickInputDirection = new Vector3(character.player.GetAxisRaw("LookHorizontal"), 0, -character.player.GetAxisRaw("LookVertical"));
        if (rightStickInputDirection != Vector3.zero)
        {
           // Debug.Log("Before " + character.rightStickInputDirection);
            rightStickInputDirection.Normalize();
            //if (true) // camera relative movement
            //{
            //    character.rightStickInputDirection = Camera.main.transform.TransformDirection(character.rightStickInputDirection);
            //    character.rightStickInputDirection.z = 0f;
            //    character.rightStickInputDirection.y *= 2;
            //    //float magnitude = character.rightStickInputDirection.sqrMagnitude;
            //}
           // Debug.Log("After " + character.rightStickInputDirection);
        }

        print("Switch Kick inputDirection : " + rightStickInputDirection);

        hipFacing.bodyForward.y = rightStickInputDirection.x * switchSpeed;


    }
}
