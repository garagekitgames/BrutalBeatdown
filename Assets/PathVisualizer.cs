using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class PathVisualizer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public List<Vector3> points = new List<Vector3>();

    public Action<IEnumerable<Vector3>> OnPathCreated = delegate { };

    public AstarAI navigator;

    private void Awake()
    {

        lineRenderer = GetComponent<LineRenderer>();
        navigator = GetComponent<AstarAI>();


    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
