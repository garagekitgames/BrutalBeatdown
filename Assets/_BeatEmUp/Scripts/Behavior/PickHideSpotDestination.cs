using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
using UnityEngine.AI;
using System.Linq;
public class PickHideSpotDestination : Action
{

    public CharacterThinker character;
    public EnemyAwareness enemyAwareness;
    public EnemyAIBase enemyAI;
    //public SharedVector3 targetDestination;
    public SharedVector3 hideSpot;
    //public SharedVector3 playerDestination;
    // public SharedTransform targetPlayerTransform;
    public SharedBool canMove;

    public NavMeshAgent agent;

    public SharedBool isAlive;

    public SharedVector3 playerLastSeen;

    public SharedVector3 randomDestination;

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
        
    }

	public override TaskStatus OnUpdate()
	{
        //randomDestination.Value = new Vector3(Random.Range(1.5f, 13.5f), 0, Random.Range(1.5f, 18.5f));

        if (enemyAwareness.hidingSpots.Count > 0)
        {
            var list = enemyAwareness.hidingSpots.OrderBy(x => Vector3.Distance(playerLastSeen.Value, x.transform.position)).ToList();
            int maxIndex = 0;
            if((enemyAwareness.hidingSpots.Count - Mathf.RoundToInt(3.0f / enemyAwareness.hidingSpots.Count)) > 0)
            {
                maxIndex = enemyAwareness.hidingSpots.Count - Mathf.RoundToInt(3.0f / enemyAwareness.hidingSpots.Count);

            }
            Vector3 destination = list[Random.Range(0, maxIndex)].transform.position;

            //Debug.Log("Total : " + enemyAwareness.hidingSpots.Count);
            //enemyAwareness.hidingSpots.OrderBy(x => Vector3.Distance(playerLastSeen.Value, x.transform.position)).FirstOrDefault().transform.position;//enemyAwareness.hidingSpots[Random.Range(0, enemyAwareness.hidingSpots.Count)].transform.position;
            //navigator.targetPosition.position = destination;
            agent.SetDestination(destination);
            character.target = destination;
            hideSpot.Value = destination;

            //randomDestination.Value = destination;
            var list2 = enemyAwareness.patrolSpots.OrderBy(x => Vector3.Distance(character.cryPart.bodyPartTransform.position, x.transform.position)).ToList();
            int maxIndex2 = 0;
            if ((enemyAwareness.patrolSpots.Count - Mathf.RoundToInt(3.0f / enemyAwareness.patrolSpots.Count)) > 0)
            {
                maxIndex2 = enemyAwareness.patrolSpots.Count - Mathf.RoundToInt(3.0f / enemyAwareness.patrolSpots.Count);

            }
            Vector3 destination2 = list2[Random.Range(0, maxIndex2)].transform.position;

            randomDestination.Value = destination2;


            //Task.current.Succeed();
            return TaskStatus.Success;
        }
        else
        {
            Vector3 destination = new Vector3(Random.Range(1.5f, 13.5f), 0, Random.Range(1.5f, 18.5f));


            //Debug.Log("Total : " + enemyAwareness.hidingSpots.Count);
            //enemyAwareness.hidingSpots.OrderBy(x => Vector3.Distance(playerLastSeen.Value, x.transform.position)).FirstOrDefault().transform.position;//enemyAwareness.hidingSpots[Random.Range(0, enemyAwareness.hidingSpots.Count)].transform.position;
            //navigator.targetPosition.position = destination;
            agent.SetDestination(destination);
            character.target = destination;
            hideSpot.Value = destination;

            //randomDestination.Value = destination;


            //Task.current.Succeed();
            return TaskStatus.Success;
            //return TaskStatus.Failure;
            //Task.current.Fail();

           


        }





    }
}