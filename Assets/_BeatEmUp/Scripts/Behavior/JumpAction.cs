using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
using UnityEngine.AI;
using System.Collections;

public class JumpAction : Action
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


        //character.targetting = true;
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
        var distance = Vector3.Distance(enemyAwareness.player.position, character.bpHolder.BodyPartsName["hip"].BodyPartTransform.position);
        var direction = (enemyAwareness.player.position - character.bpHolder.BodyPartsName["hip"].BodyPartTransform.position);
        if(distance <= agent.stoppingDistance && !agent.pathPending)
        {
            canMove.Value = false;
            enemyAI.canMove = false;

            character.target = enemyAwareness.player.position;

            direction.Normalize();
            direction.y = 0;
            character.inputDirection = direction;
            if (!character.attacking)
            {

                //character.targetting = true;

                IEnumerator coroutine = character.DoSimpleAttack(0.2f);
                StartCoroutine(coroutine);
                //character.attack = true;

            }

            return TaskStatus.Success;
        }
        else if (distance <= 2.5f && !agent.pathPending)
        {
            //canMove.Value = false;
            //enemyAI.canMove = false;

            //enemyAI.canMove = false;
            //agent.isStopped = true;
            character.target = enemyAwareness.player.position;
            
            direction.Normalize();
            direction.y = 0;
            character.inputDirection = direction;
            if (!character.attacking && !character.GetComponent<Jump>().inAir)
            {

                //character.targetting = true;

                IEnumerator coroutine = character.DoSimpleAttack(0.1f);
                StartCoroutine(coroutine);
                //character.attack = true;

            }


            return TaskStatus.Success;
        }
        else if(distance <= 4 && !agent.pathPending)
        {
            //canMove.Value = false;
            //enemyAI.canMove = false;

            //enemyAI.canMove = false;
            //agent.isStopped = true;
            character.target = enemyAwareness.player.position;
            //v
            if (!character.attacking && character.GetComponent<Jump>().canJump)
            {
                character.GetComponent<Jump>().JumpNow(direction.normalized);
            }
                


            return TaskStatus.Success;
        }
    
        else
        {
            //character.targetting = true;
            //character.target = destination;
            return TaskStatus.Running;
        }


        //if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        //{
        //    //canMove.Value = false;
        //    //enemyAI.canMove = false;


        //    if (!character.attacking)
        //    {
        //    character.targetting = true;
        //    character.targetting = true;
        //    character.target = enemyAwareness.player.position;
        //        IEnumerator coroutine = character.DoSimpleAttack(0.2f);
        //        StartCoroutine(coroutine);

        //    }


        //    //
        //return TaskStatus.Success;
        //}

        //return TaskStatus.Running;
        // character.targetting = true;
       
        //shooting.Shoot();
        //character.targetting = true;
        //return TaskStatus.Success;

    }

}