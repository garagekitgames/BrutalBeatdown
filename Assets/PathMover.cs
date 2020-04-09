using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using garagekitgames;
using System;
using UnityEngine.Events;

public class PathMover : MonoBehaviour
{
	public NavMeshAgent agent;
	public CharacterThinker character;

	private Queue<Vector3> pathPoints = new Queue<Vector3>();
    // Start is called before the first frame update
    public bool canMove = false;

    public float speed = 2;

    public float nextWaypointDistance = 3;

    public int currentWaypoint = 0;

    public bool reachedEndOfPath;

    public Transform hipPart;


    public float updateFrequency = 0.2f;

    public List<Vector3> path = new List<Vector3>();

    //public GameObject pathCreator;

    public UnityEvent OnResetPlayer;

    public PathCreator myPath;
    private void Awake()
    {

        //agent = this.GetComponent<NavMeshAgent>();
        //agent.updatePosition = false;
        //agent.updateRotation = false;

        //pathCreator = FindObjectOfType<PathCreator>().gameObject;
        


    }
    void Start()
    {
        character = GetComponent<CharacterThinker>();
        hipPart = character.bpHolder.BodyPartsName["hip"].BodyPartTransform;
        myPath.OnNewPathCreated += SetPoints;
        canMove = false;
        //StartMove();
        
    }

    private void SetPoints(IEnumerable<Vector3> points)
	{
		pathPoints = new Queue<Vector3>(points);
        
        currentWaypoint = 0;
        path = new List<Vector3>(points);
	}

    // Update is called once per frame
    void Update()
    {
       // UpdatePathing();
		//CharacterMover(); 
    }

    public void FixedUpdate()
    {
        if (character.stopDoingShit)
        {
            character.inputDirection = Vector3.zero;
            return;
        }
        if (path.Count == 0)
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
            distanceToWaypoint = Vector3.Distance(hipPart.position, path[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.Count)
                {
                    currentWaypoint++;
                    character.target = path[currentWaypoint];
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
        Vector3 dir = (path[currentWaypoint] - hipPart.position).normalized;
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

        if (!reachedEndOfPath && canMove)
        {
            character.inputDirection = velocity;
        }
        else
        {
            character.inputDirection = Vector3.zero;
            StopMove();
            //canMove = false;
        }


        // If you are writing a 2D game you may want to remove the CharacterController and instead use e.g transform.Translate
        // transform.position += velocity * Time.deltaTime;
    }


    public void StartMove()
    {
        canMove = true;
        OnResetPlayer.Invoke();
        currentWaypoint = 0;
        hipPart.position = myPath.transform.position;
    }

    public void StopMove()
    {
        canMove = false;
    }

    public void UpdatePathing()
    {
        if(ShouldSetDestination())
        {
            agent.SetDestination(pathPoints.Dequeue());
        }
    }

    private bool ShouldSetDestination()
    {
        if(pathPoints.Count == 0)
        {
            return false;
        }
        if (agent.remainingDistance <= agent.stoppingDistance || !agent.hasPath)
        {
            return true;
        }

        return false;
    }

    public void CharacterMover()
	{
        //if (character.stopDoingShit && !once)
        //{
        //	once = true;
        //	Dead();
        //	return;
        //}


        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {

            canMove = false;


        }
        else
        {
            canMove = true;
        }

        if (character.health.alive && canMove)
		{
			var targetDirection = agent.nextPosition - character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position;
			targetDirection.Normalize();
			targetDirection.y = 0;
			character.inputDirection = targetDirection;
			character.targetDirection = targetDirection;

			//agent.velocity = character.bpHolder.bodyPartsName["hip"].bodyPartRb.velocity;
		}
		else
		{
			character.inputDirection = Vector3.zero;
		}


		//if (!character.health.alive && !once)
		//{
		//	once = true;
		//	Dead();
		//}
	}
}
