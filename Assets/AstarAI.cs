using UnityEngine;
// Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
// This line should always be present at the top of scripts which use pathfinding
using Pathfinding;
using garagekitgames;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class AstarAI : MonoBehaviour
{
    public Transform targetPosition;
    public Vector3 targetPositionVector;

    private Seeker seeker;
    private CharacterController controller;

    public CharacterThinker character;

    public Path path;

    public float speed = 2;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;

    public Transform hipPart;

    public bool canMove = true;

    public LineRenderer lineRenderer;

    public List<Vector3> points = new List<Vector3>();

    public bool visualizePath;

    public GameObject targetMarkerPrefab;

    public float updateFrequency = 0.2f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();


        targetPosition = GameObject.Instantiate(targetMarkerPrefab, this.transform.position, Quaternion.identity).transform;



    }
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        // If you are writing a 2D game you can remove this line
        // and use the alternative way to move sugggested further below.
        controller = GetComponent<CharacterController>();

        // Start a new path to the targetPosition, call the the OnPathComplete function
        // when the path has been calculated (which may take a few frames depending on the complexity)
        character = GetComponent<CharacterThinker>();
        hipPart = character.bpHolder.BodyPartsName["hip"].BodyPartTransform;
        //seeker.StartPath(hipPart.position, targetPosition.position, OnPathComplete);

        InvokeRepeating("SearchPath", 1, updateFrequency);
       
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
            //canMove = false;
            points.Clear();

            if(visualizePath && !reachedEndOfPath)
            {
                if (lineRenderer != null)
                    lineRenderer.enabled = true;
                int i = 0;
                foreach (var item in path.vectorPath)
                {

                    var thisPoint = item;
                    thisPoint.y += 0.2f;
                    if(i>1)
                    {
                        points.Add(thisPoint);
                    }
                    
                    
                    
                    i++;
                }

                if (lineRenderer != null)
                {

                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());
                }
            }
            else
            {
                if (lineRenderer != null)
                    lineRenderer.enabled = false;
            }
            


        }
    }

    public void GoTo(Transform target)
    {
        //seeker.StartPath(hipPart.position, target.position, OnPathComplete);
        IEnumerator coroutine = this.DoSimpleFollow(target);
        this.StartCoroutine(coroutine);
        Debug.Log(" GoTo(Transform target)");
    }

    public IEnumerator DoSimpleFollow(Transform target)
    {

        while ((Vector3.Distance(hipPart.position, target.position) > 1.5f))
        {
            seeker.StartPath(hipPart.position, target.position, OnPathComplete);
            character.target = target.position;
            yield return new WaitForEndOfFrame();
        }
        //CharacterThinker enemyCharacter = target.root.GetComponent<CharacterThinker>();
        //if (!character.attacking && !character.walking && enemyCharacter.health.alive)
        //{
        //    character.target = target.position;
        //    IEnumerator coroutine = character.DoSimpleAttack(0.5f);
        //    this.StartCoroutine(coroutine);
            
        //}
        //this.slamming = false;




    }

    public void GoTo(Vector3 targetPos)
    {

        seeker.StartPath(hipPart.position, targetPos, OnPathComplete);
        Debug.Log("GoTo(Vector3 targetPos) ");
        character.target = targetPos;
    }


    public void SearchPath()
    {
        if (character.stopDoingShit)
        {
            character.inputDirection = Vector3.zero;
            return;
        }

        seeker.StartPath(hipPart.position, targetPosition.position, OnPathComplete);
        
        
    }
    public void FixedUpdate()
    {
        if(character.stopDoingShit)
        {
            character.inputDirection = Vector3.zero;
            return;
        }
        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(hipPart.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - hipPart.position).normalized;
        dir.y = 0.0f;
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * speed * speedFactor;

        // Move the agent using the CharacterController component
        // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
        //controller.SimpleMove(velocity);

       // velocity.Normalize();
       // velocity.y = 0;
        //var inputDirection = velocity;
        
        //inputDirection.y = 0.0f;

        if(!reachedEndOfPath && canMove)
        {
            character.inputDirection = velocity;
        }
        else
        {
            character.inputDirection = Vector3.zero;
            //canMove = false;
        }
        

        // If you are writing a 2D game you may want to remove the CharacterController and instead use e.g transform.Translate
        // transform.position += velocity * Time.deltaTime;
    }
}