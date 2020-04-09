using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Rigidbody rB;
    public Vector2 floatMinMax;
    float force;
    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
        force = Random.Range(floatMinMax.x, floatMinMax.y);
        rB.AddForce(transform.right * force);
        rB.AddTorque(Random.insideUnitSphere * force);

        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
