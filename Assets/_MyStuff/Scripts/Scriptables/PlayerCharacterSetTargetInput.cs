using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace garagekitgames
{
    [CreateAssetMenu(menuName = "GarageKitGames/Actitons/PlayerSetTargetInput")]
    public class PlayerCharacterSetTargetInput : CharacterAction
    {
        public bool touchInput;
        public LayerMask layer;
        int layerMask = 1 << 8;
        public override void OnFixedUpdate(CharacterThinker character)
        {
            
        }

        public override void OnUpdate(CharacterThinker character)
        {
            if (touchInput)
            {
                Touch[] myTouches = Input.touches;
                if (Input.touchCount == 1)
                {
                    //for (int i = 0; i < Input.touchCount; i++)
                    //{
                    if (myTouches[0].phase == TouchPhase.Stationary || myTouches[0].phase == TouchPhase.Moved)
                    {
                        Ray mouseRay = GenerateMouseRay();
                        RaycastHit hit;

                        if (Physics.Raycast(mouseRay.origin, mouseRay.direction,  out hit, 100, layer))
                        {
                            // GameObject temp = Instantiate(target, hit.point, Quaternion.identity);
                            //Debug.DrawRay(mouseRay.origin, mouseRay.direction * hit.distance, Color.yellow);
                            character.target = hit.point;
                        }

                    }
                    //}
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Ray mouseRay = GenerateMouseRay();
                    RaycastHit hit;

                    if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100, layer))
                    {
                        // GameObject temp = Instantiate(target, hit.point, Quaternion.identity);
                        //Debug.DrawRay(mouseRay.origin, mouseRay.direction * hit.distance, Color.yellow);
                        character.target = hit.point;
                    }

                }
            }
            
        }

        Ray GenerateMouseRay()
        {
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

            Vector3 mousePosFarW = Camera.main.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosNearW = Camera.main.ScreenToWorldPoint(mousePosNear);

            Ray mouseRay = new Ray(mousePosNearW, mousePosFarW - mousePosNearW);

            return mouseRay;



        }
    }
}
