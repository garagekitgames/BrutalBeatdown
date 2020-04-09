using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace garagekitgames.shooter
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        Rigidbody rB;
        Vector3 _moveVelocity;
        float _rotateAngle;
        private void Start()
        {
            rB = this.GetComponent<Rigidbody>();
        }
        public void Move(Vector3 moveVelocity)
        {
            _moveVelocity = moveVelocity;


        }
        private void FixedUpdate()
        {
            rB.MovePosition(rB.position + _moveVelocity * Time.deltaTime);
            //rB.MoveRotation(Quaternion.Euler(Vector3.up * _rotateAngle) );
        }

        public void LookAt(Vector3 point)
        {
            point = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(point);

            /*Vector3 lookDir = (point - transform.position).normalized;
            float rotateAngle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;
            _rotateAngle = rotateAngle;*/


        }
    }

}
