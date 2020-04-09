using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{

    private LineRenderer lr;

    public int laserRange;

    public UnityEvent onPlayerDetected;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);
        LayerMask layerMask;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider.gameObject.tag.Contains("Player"))
            {
                onPlayerDetected.Invoke();
            }
            if (hit.collider.gameObject.tag.Contains("Obstacle"))
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else 
            lr.SetPosition(1, transform.position + (transform.forward * laserRange));
    }
}

