using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
using UnityEngine.AI;
using System.Linq;

public class FaceDirection : Action
{

	public CharacterThinker character;
	public EnemyAwareness enemyAwareness;
	public EnemyAIBase enemyAI;

	public NavMeshAgent agent;

    public SneakyEnemyAI sneakyAI;


    public SharedInt faceDirection;

    public SharedInt directionIndex;

    public SharedFloat waitDuration;
    

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

        
        if(sneakyAI.directions.Count > 0)
        {
            faceDirection.Value = (int)sneakyAI.directions[directionIndex.Value].direction;
            waitDuration.Value = sneakyAI.directions[directionIndex.Value].wait;
        }
        else
        {
            //faceDirection.Value = (int)sneakyAI.directions[directionIndex.Value].direction;
            //waitDuration.Value = sneakyAI.directions[directionIndex.Value].wait;
        }
        

    }

	public override TaskStatus OnUpdate()
	{
        if (sneakyAI.directions.Count > 0)
        {
            character.targetting = true;
            switch (faceDirection.Value)
            {
                case 1:
                    
                    character.target = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position + Vector3.forward;
                    break;
                case 2:
                    character.target = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position + Vector3.back;
                    break;
                case 3:
                    character.target = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position + Vector3.right;
                    break;
                case 4:
                    character.target = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position + Vector3.left;
                    break;
            }
        }
            
        return TaskStatus.Success;
	}

    public override void OnEnd()
    {
        base.OnEnd();
        if (directionIndex.Value + 1 < sneakyAI.directions.Count)
        {
            directionIndex.Value = directionIndex.Value + 1;
        }
        else
        {
            directionIndex.Value = 0;
        }

    }
}