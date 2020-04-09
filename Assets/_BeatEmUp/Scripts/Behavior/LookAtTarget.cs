using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;

public class LookAtTarget : Action
{
    public CharacterThinker character;
    public EnemyAwareness enemyAwareness;
    public SharedVector3 target;

    public bool targetPlayer;

    public SharedTransform targetPlayerTransform;
    //public SharedVector3 targetVector;

    public override void OnAwake()
    {
        base.OnAwake();
        character = GetComponent<CharacterThinker>();
        enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
        //player.Value = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
    }

    public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
        Vector3 direction = Vector3.one;
        Vector3 targetToLookAt = Vector3.one;
        if (targetPlayer)
        {
            targetToLookAt = targetPlayerTransform.Value.position;
            direction = targetPlayerTransform.Value.position - character.bpHolder.bodyPartsName[BodyPartNames.hipName].bodyPartTransform.position;
            direction.Normalize();
            direction.y = 0;
        }
        else
        {
            targetToLookAt = target.Value;
            direction = target.Value - character.bpHolder.bodyPartsName[BodyPartNames.hipName].bodyPartTransform.position;
            direction.Normalize();
            direction.y = 0;
        }
         
        character.target = targetToLookAt;
        //character.targetting = true;
        //var newvector = character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.forward.normalized;
        //newvector.y = 0;
        //// Debug.Log("Angle  : " + Vector3.Angle(newvector, direction));

        ////if (Vector3.Angle(character.bpHolder.bodyPartsName["hip"].bodyPartTransform.InverseTransformDirection(character.inputDirection), direction) < 5.0f)
        ////{
        ////    //Task.current.Succeed();
        ////}
        //if (Vector3.Angle(newvector, direction) <= 10f)
        //{

        //    return TaskStatus.Success;
        //}
        //else
        //{
        //    //var direction = (enemyAwareness.player.position - character.bpHolder.BodyPartsName["hip"].BodyPartTransform.position);

        //    character.inputDirection = direction;
        //    return TaskStatus.Running;
        //}

        return TaskStatus.Success;

    }
}