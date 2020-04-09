using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace garagekitgames
{
    public class SetTargetInFront : MonoBehaviour
    {

        public CharacterThinker character;

        public Vector3 targetSample;

        public Vector3 globalPosition;

        public CharacterFaceDirection fD;

        public GameObject prefab;
        // Use this for initialization

        public BodyPart playerRootPart;

        GameObject targetTransform;
        void Start()
        {
            character = transform.GetComponent<CharacterThinker>();
            targetTransform = Instantiate(prefab);
            BodyPartMono bodyPartMono = character.bpHolder.bodyParts[playerRootPart];
            fD = bodyPartMono.BodyPartFaceDirection;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 inputDirection = character.inputDirection;
            
            //fD.bodyForward;
            ConvertMoveInputAndPassItToAnimator(inputDirection);
        }

        void ConvertMoveInputAndPassItToAnimator(Vector3 inputDirection)
        {
            //Convert the move input from world positions to local positions so that they have the correct values
            //depending on where we look
            Vector3 localMove = transform.InverseTransformDirection(inputDirection);
            //localMove.Normalize();
            float turnAmount = localMove.y;
            float forwardAmount = localMove.z;

            //
            globalPosition = fD.transform.position + fD.rigidbody.transform.TransformDirection(fD.bodyForward).normalized * 2;
            targetTransform.transform.position = globalPosition;
            globalPosition.y = 1.8f;

            character.target = globalPosition;
            /* if (turnAmount != 0)
                 turnAmount *= 2;

             */
            //if (log)
            //{
            /*print("Forward : " + localMove);
                print("turnAmount : " + turnAmount);
                print("forwardAmount : " + forwardAmount);
                print("inputdirection : " + inputDirection);
                print("hipFacing : " + fD.bodyForward);*/
            //}

            //testVector2 = new Vector3(-forwardAmount, turnAmount, 0f);
        }
    }
}

