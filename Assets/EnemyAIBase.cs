using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;
using garagekitgames;
//using System;

//    Patrol : Random spots / Waypoint patrol

//Two states : Alert, Calm

//variables : 
//vector3 personalLastSeen : this is set when soldier sees player personally

//vector3 playerLastSeen : if its infinity, player not seen, it is set when player is seen by any enemy
//bool isalert : if playerLastSeen, is not infinity,


//Awareness : 
//Voice -> when killed, overlapsphere and inform nearby enemies that you are dead call hearNoise(currentPosition)
//Ears -> heardnoiseMethod(Vector3 location)
//Eyes -> on trigger enter + FoV + Raycast setup -> overlapsphere and calls SetPlayerLastSeen(v3 location) on nearby allies and set personalLastSeen(v3 location)



//While alert
//While can seeplayer
//    wait(reactionTime)

//    While has LoS
//        Shoot
//.
//while playerLastSeen is not default value(means someone has seen player)
//	while personalLastSeen is not playerLastSeen

//        GoTo playerLastSeen
//		while not seePlayer

//            Search
//            wait

//            Search
//            wait

//            Search
//            wait
//While searching if the player is found,-> must do shooting
//.
//Wander / patrol
//Die


public class EnemyAIBase : MonoBehaviour
{
    public Transform player;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    public AstarAI navigator;
    public NavMeshAgent agent;
    public Vector3 destination;
    public Vector3 target;

    public float patrolSpeed = 0.5f;
    public float chaseSpeed = 1f;

    public float visibleRange = 80f;
    public float fielOfView = 110f;
    public float shotRange = 40.0f;

    public CharacterThinker character;

    public bool canMove = true;
    public LayerMask raycastLayermask;

    public EnemyAwareness enemyAwareness;
    public string playerTag = "Player";

    public Vector3 playerLastSeen;
    public Vector3 defaultLastSeen;
    // Start is called before the first frame update
    public bool once = false;
    void Start()
    {
        playerLastSeen = new Vector3(-1000, -1000, -1000);
        defaultLastSeen = new Vector3(-1000, -1000, -1000);
        character = GetComponent<CharacterThinker>();
        navigator = this.GetComponent<AstarAI>();
        agent = this.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        player = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
        enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
    }

    // Update is called once per frame
    void Update()
    {
        if (character.stopDoingShit && !once)
        {
            once = true;
            Dead();
            return;
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


        if (!character.health.alive && !once)
        {
            once = true;
            Dead();
        }
        //canMove = false; 
        
    }

    private void Dead()
    {
        enemyAwareness.Alert(player.position);
        enemyAwareness.stopNow = true;

        //enemyAwareness.behaviorTree.StopAllCoroutines();
        enemyAwareness.behaviorTree.StopAllTaskCoroutines();
        enemyAwareness.behaviorTree.enabled = false;
        navigator.enabled = false;
        agent.enabled = false;
        
    }

    //[Task]
    public void StopMove()
    {
        canMove = false;
    }
   // [Task]
    public void PickDestination(float x, float z)
    {

        Vector3 destination = new Vector3(x, 0, z);
        //navigator.targetPosition.position = destination;
        agent.SetDestination(destination);
        character.target = destination;
        canMove = true;
        //Task.current.Succeed();

    }

    //[Task]
    public void PickPlayerLastSeenDestination()
    {
        if(enemyAwareness.playerLastSeen != enemyAwareness.defaultLastSeen)
        {
            Vector3 destination = enemyAwareness.playerLastSeen;
            //navigator.targetPosition.position = destination;
            agent.SetDestination(destination);
            character.target = destination;
            canMove = true;
           // Task.current.Succeed();
        }
        else
        {
          //  Task.current.Fail();

        }
        

    }



    //[Task]
    public void PickNearbyHideDestination()
    {
        if(enemyAwareness.hidingSpots.Count > 0)
        {
            Vector3 destination = enemyAwareness.hidingSpots[Random.Range(0, enemyAwareness.hidingSpots.Count)].transform.position;
            //navigator.targetPosition.position = destination;
            agent.SetDestination(destination);
            character.target = destination;
            canMove = true;
            //Task.current.Succeed();
        }
        else
        {
            //Task.current.Fail();
        }
        

    }

    //[Task]
    public void PickRandomDestination()
    {
        Vector3 destination = new Vector3(Random.Range(4.5f, 16f), 0, Random.Range(1.5f, 41f));
        //navigator.targetPosition.position = destination;
        agent.SetDestination(destination);
        character.target = destination;
        canMove = true;
        //Task.current.Succeed();

    }

    //[Task]
    public void MoveToDestinationPatrol()
    {
       //if (Task.isInspected)
        //{
           // Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time);
        //}
        //if(navigator.reachedEndOfPath)
        //{
        //    Task.current.Succeed();
        //}

        if(enemyAwareness.canSeePlayer)
        {
            //Task.current.Fail();
        }
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            canMove = false;

            //Task.current.Succeed();
        }
    }

   // [Task]
    public void MoveToDestination()
    {
       // if(Task.isInspected)
        //{
           // Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time); 
        //}
        //if(navigator.reachedEndOfPath)
        //{
        //    Task.current.Succeed();
        //}
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            canMove = false;
           // Task.current.Succeed();
        }
    }

    //[Task]
    public void TargetPlayer()
    {
        character.target = player.position;
       // Task.current.Succeed();
    }

    //[Task]
    public void LookAtPlayer()
    {
        character.target = player.position;
        Vector3 direction = character.target - character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position;
        direction.Normalize();
        direction.y = 0;
        

        if (Vector3.Angle(character.bpHolder.bodyPartsName["hip"].bodyPartTransform.InverseTransformDirection(character.inputDirection), direction) < 5.0f)
        {
            //Task.current.Succeed();
        }
    }

    //[Task]
    public void ShootAtPlayer()
    {
        if (!character.attacking)
        {
            //character.target = target.position;
            IEnumerator coroutine = character.DoSimpleAttack(0.2f);
            this.StartCoroutine(coroutine);
            //if (!attack)
            //Task.current.Succeed();
        }
    }

    //[Task]
    bool CanSeePlayer()
    {
        //have LOS code here and return true or false depending on the circumstance
        Vector3 distance = character.target - character.bpHolder.bodyPartsName["head"].bodyPartTransform.position;

        RaycastHit hit;
        bool seeWall = false;

        Debug.DrawRay(character.bpHolder.bodyPartsName["head"].bodyPartTransform.position, distance, Color.red);

        if(Physics.Raycast(character.bpHolder.bodyPartsName["head"].bodyPartTransform.position, distance, out hit, 100, raycastLayermask))
        {
            if(hit.collider.tag.Contains("Obstacle"))
            {

                seeWall = true;

            }
        }

       // if (Task.isInspected)
        //    Task.current.debugInfo = string.Format("wall={0}", seeWall);

        if(distance.magnitude < visibleRange && !seeWall)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   // [Task]
    public bool Turn(float angle)
    {
        var p = character.bpHolder.bodyPartsName["head"].bodyPartTransform.position + Quaternion.AngleAxis(angle, Vector3.up) * character.bpHolder.bodyPartsName["hip"].bodyPartTransform.forward;

        character.target = p;

        return true;
    }

    // [Task]
    public bool IsAlive()
    {
        return character.health.alive;
    }

   // [Task]
    public bool IsPlayerVisible()
    {
        //bool canSeePlayer = false;
        //Vector3 playerPosition = Vector3.zero;
        //foreach (var item in enemyAwareness.visibleTargets)
        //{
        //    if(item.CompareTag(playerTag))
        //    {
        //        canSeePlayer = true;
        //        playerPosition = item.transform.position;
        //    }
        //    else
        //    {
        //        canSeePlayer = false;
        //    }
        //}

        //if(canSeePlayer)
        //{
        //    enemyAwareness.playerLastSeen = playerPosition;
        //    enemyAwareness.Alert(playerPosition);
        //}
        //else
        //{

        //}
        //return canSeePlayer;
        //enemyAwareness.visibleTargets

        return enemyAwareness.canSeePlayer;
    }

    //[Task]
    public bool IsPlayerLastSeen()
    {
        return !(enemyAwareness.playerLastSeen == enemyAwareness.defaultLastSeen);
    }

   // [Task]
    public void ForgetPlayer()
    {
        enemyAwareness.playerLastSeen = enemyAwareness.defaultLastSeen;
        //Task.current.Fail();
    }


    //[Task]
    public bool HasLineOfSight()
    {

        //character.target = player.position;
        //Vector3 direction = character.target - character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position;
        //direction.Normalize();
        //direction.y = 0;
        //var distance = character.target - character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position;


        ////if (Vector3.Angle(character.bpHolder.bodyPartsName["hip"].bodyPartTransform.InverseTransformDirection(character.inputDirection), direction) < 1.0f)
        ////{
        ////    return true;
        ////}
        ////else
        ////{
        ////    return false;
        ////}

        //character.target = player.position;
        //Vector3 target = character.target;
        //Vector3 dirToTarget = (target - transform.position).normalized;
        //if (Vector3.Angle(transform.forward, dirToTarget) < 1.0f)
        //{
        //    float dstToTarget = Vector3.Distance(transform.position, target);
        //    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, enemyAwareness.obstacleMask))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //else
        //{
        //    return false;
        //}

        return enemyAwareness.hasLosToPlayer;

    }

    //[Task]
    public void LookAtTarget()
    {
       
        //character.target = player.position;
        Vector3 direction = character.target - character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position;
        direction.Normalize();
        direction.y = 0;
        //character.inputDirection = direction;

        if (Vector3.Angle(character.bpHolder.bodyPartsName["hip"].bodyPartTransform.InverseTransformDirection(character.inputDirection), direction) < 5.0f)
        {
           // Task.current.Succeed();
        }
    }

    //[Task]
    public bool IsHealthLessThan(float health)
    {
        return character.health.currentHealth < health;
    }

    //[Task]
    public bool InDanger(float minDistance)
    {
        Vector3 distance = player.transform.position - character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position;
        return (distance.magnitude < minDistance);
    }

    //[Task]
    public void TakeCover()
    {
        Vector3 awayFromPlayer = player.transform.position - character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position;
        Vector3 destination = character.bpHolder.bodyPartsName["hip"].bodyPartTransform.position - awayFromPlayer * 2;
        agent.SetDestination(destination);
        character.target = destination;
        //Task.current.Succeed();
    }

}
