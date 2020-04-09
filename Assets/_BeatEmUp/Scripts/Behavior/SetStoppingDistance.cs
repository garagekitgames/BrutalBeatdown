using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
using UnityEngine.AI;

public class SetStoppingDistance : Action
{
	public CharacterThinker character;
	public EnemyAwareness enemyAwareness;
	public EnemyAIBase enemyAI;
	public SharedVector3 targetDestination;
	// public SharedTransform targetPlayerTransform;
	public SharedBool canMove;

	public NavMeshAgent agent;

	public SharedBool isAlive;

    public SharedFloat stoppingDistance;

	public override void OnAwake()
	{
		base.OnAwake();
		character = GetComponent<CharacterThinker>();
		enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
		enemyAI = GetComponent<EnemyAIBase>();
		//player.Value = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
		agent = this.GetComponent<NavMeshAgent>();
	}

	public override void OnStart()
	{
        agent.stoppingDistance = stoppingDistance.Value;

    }

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}