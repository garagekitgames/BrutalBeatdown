using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueTest : MonoBehaviour
{
    public float torque;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float turn = Input.GetAxis("Horizontal");
        var inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        inputDirection.Normalize();
        inputDirection = Camera.main.transform.TransformDirection(inputDirection);
        inputDirection.y = 0.0f;
        Vector3 middleFinger = Vector3.Cross(Vector3.up, inputDirection);
        Debug.DrawRay(transform.position, middleFinger * 3, Color.yellow);
        rb.AddTorque(middleFinger * torque );
    }
}
