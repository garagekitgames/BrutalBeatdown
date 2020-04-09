using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
using UnityEngine.AI;
using System.Collections;

public class MoveToDestination : Action
{

    public CharacterThinker character;
    public EnemyAwareness enemyAwareness;
    public EnemyAIBase enemyAI;
    public SharedVector3 targetDestination;
    // public SharedTransform targetPlayerTransform;
    public SharedBool canMove;

    public NavMeshAgent agent;

    public SharedBool isAlive;

   // public SharedVector3



    public override void OnAwake()
    {
        base.OnAwake();
        character = GetComponent<CharacterThinker>();
        enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
        enemyAI = GetComponent<EnemyAIBase>();
        //player.Value = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
        agent = this.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    public override void OnStart()
	{
        Vector3 destination = targetDestination.Value;
        //navigator.targetPosition.position = destination;
        agent.SetDestination(destination);
        character.target = destination;
        enemyAI.canMove = true;
        canMove.Value = true;
        agent.isStopped = false;
        character.targetting = false;
    }

	public override TaskStatus OnUpdate()
	{
        Vector3 destination = targetDestination.Value;
        //navigator.targetPosition.position = destination;
        agent.SetDestination(destination);
        character.target = destination;
        enemyAI.canMove = true;
        canMove.Value = true;
        agent.isStopped = false;
        character.targetting = false;


        //if (character.health.alive && enemyAI.canMove)
        //{
        //    var targetDirection = agent.nextPosition - character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position;
        //    targetDirection.Normalize();
        //    targetDirection.y = 0;
        //    character.inputDirection = targetDirection;
        //    //agent.velocity = character.bpHolder.bodyPartsName["hip"].bodyPartRb.velocity;
        //}
        //else
        //{
        //    character.inputDirection = Vector3.zero;
        //}


        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            canMove.Value = false;
            enemyAI.canMove = false;

            //if (!character.attacking)
            //{

            //    character.targetting = true;
            //    character.target = destination;
            //    IEnumerator coroutine = character.DoSimpleAttack(0.2f);
            //    StartCoroutine(coroutine);

            //}
            //character.target = destination;


            return TaskStatus.Success;
        }
        else
        {
            //character.targetting = true;
            //character.target = destination;
            return TaskStatus.Running;
        }
        //else
        //{
        //    character.target = enemyAwareness.player.position;
        //    character.targetting = true;
        //}

        

    }

    public override void OnConditionalAbort()
    {
        base.OnConditionalAbort();
        enemyAI.canMove = false;
        if(agent.enabled == true )
        {
            agent.isStopped = true;
        }
        
        
    }
}