using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace garagekitgames
{
    public class CharacterUI : MonoBehaviour
    {

        public Canvas playerCanvas;
        public BodyPart playerRootPart;
        public CharacterThinker character;

        public Transform playerRootPosition;
        CharacterFaceDirection bpFD;
        public Transform proxSensor;
        // Use this for initialization
        void Start()
        {
            playerCanvas = transform.GetComponentInChildren<Canvas>();
            character = transform.GetComponent<CharacterThinker>();
            BodyPartMono bodyPartMono = character.bpHolder.bodyParts[playerRootPart];

            playerRootPosition = bodyPartMono.BodyPartTransform;

            bpFD = bodyPartMono.BodyPartFaceDirection;
           // proxSensor = transform.GetComponentInChildren<ProximitySensor>().transform;

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 canvasPosition = playerRootPosition.transform.position;
            canvasPosition.y = 0.5f;
            playerCanvas.transform.position = canvasPosition;
            Vector3 position1 = playerRootPosition.transform.position;
            position1.y = 0;
            Vector3 position2 = character.target;
            position2.y = 0;

            //playerCanvas.transform.rotation = playerRootPosition.transform.rotation;
            Vector3 direction = -(position1 - position2);
            //direction.x = 0;
            //direction.z = 0;



            Quaternion rotationValue = Quaternion.LookRotation(Vector3.down,direction);
            //rotationValue.x = 90f;
            //rotationValue.z = 0f;
           // print("SLider Rotation : " + Quaternion.ToEulerAngles(rotationValue));
            //playerCanvas.transform.rotation = Quaternion.Euler(rotationValue.x, rotationValue.y, rotationValue.z);
            playerCanvas.transform.rotation = rotationValue;
            if(proxSensor != null)
            {
               // proxSensor.transform.rotation = rotationValue;
               // Vector3 proxSensorPos = playerRootPosition.transform.position;
               // proxSensorPos.y = 1f;
               // proxSensor.transform.position = proxSensorPos;
            }
            
        }

        
    }
}

