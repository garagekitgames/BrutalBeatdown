using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
using UnityEngine.AI;
using System.Linq;
using DG.Tweening;

public class WaypointPatrol : Action
{

	public CharacterThinker character;
	public EnemyAwareness enemyAwareness;
	public EnemyAIBase enemyAI;

	public NavMeshAgent agent;

	public SneakyEnemyAI sneakyAI;


	public SharedTransform waypointTransform;

	public SharedInt waypointIndex;

	public SharedFloat waypointWaitduration;

	public BodyPartMono headPart;
	public float smoothTime = 0.3f;
	//public float yVelocity = 0.0f;

	public bool isIdle = true;

	public SharedVector3 waypointPosition;

	public override void OnStart()
	{
		base.OnAwake();
		character = GetComponent<CharacterThinker>();
		enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
		enemyAI = GetComponent<EnemyAIBase>();
		sneakyAI = GetComponent<SneakyEnemyAI>();
		//player.Value = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
		agent = this.GetComponent<NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;

		headPart = character.bpHolder.BodyPartsName[BodyPartNames.headName];

		if (isIdle)
		{
            if(sneakyAI.waypoints.Count > 0)
            {
                waypointTransform.Value = sneakyAI.waypoints[waypointIndex.Value].waypoint;
                //Debug.Log("lookSideIndex.Value : " + lookSideIndex.Value);
                waypointWaitduration.Value = sneakyAI.waypoints[waypointIndex.Value].wait;
            }
			
		}
	}

	public override TaskStatus OnUpdate()
	{
        if (sneakyAI.waypoints.Count > 0)
            waypointPosition.Value = waypointTransform.Value.position;


        return TaskStatus.Success;
	}

    public override void OnEnd()
    {
        base.OnEnd();
        if (waypointIndex.Value + 1 < sneakyAI.waypoints.Count)
        {
            waypointIndex.Value = waypointIndex.Value + 1;
        }
        else
        {
            waypointIndex.Value = 0;
        }

    }
}