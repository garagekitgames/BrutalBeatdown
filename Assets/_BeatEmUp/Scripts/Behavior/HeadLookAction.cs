using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
using UnityEngine.AI;
using System.Linq;
using DG.Tweening;

public class HeadLookAction : Action
{
    public CharacterThinker character;
    public EnemyAwareness enemyAwareness;
    public EnemyAIBase enemyAI;

    public NavMeshAgent agent;

    public SneakyEnemyAI sneakyAI;


    public SharedInt lookSide;

    public SharedInt lookSideIndex;

    public SharedFloat lookWaitDuration;

    public BodyPartMono headPart;
    public float smoothTime = 0.3f;
    //public float yVelocity = 0.0f;

    public bool isIdle = true;

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

        if(isIdle)
        {
            if (sneakyAI.lookSides.Count > 0)
            {
                lookSide.Value = (int)sneakyAI.lookSides[lookSideIndex.Value].side;
                Debug.Log("lookSideIndex.Value : " + lookSideIndex.Value);
                lookWaitDuration.Value = sneakyAI.lookSides[lookSideIndex.Value].wait;
            }
            
        }
        
    }

	public override TaskStatus OnUpdate()
	{
        if (sneakyAI.lookSides.Count > 0)
        {
            //float yVelocity = 0.0f;
            switch (lookSide.Value)
            {
                case 0:
                    DOTween.To(() => headPart.BodyPartConfigJoint.targetAngularVelocity, x => headPart.BodyPartConfigJoint.targetAngularVelocity = x, sneakyAI.lookStraightPose, sneakyAI.headTurnDuration);
                    //DOTween.To(() => headPart.BodyPartConfigJoint.targetAngularVelocity, x => headPart.BodyPartConfigJoint.targetAngularVelocity = x, new Vector3(2, 2, 2), 1);

                    break;
                case 1:
                    //Look Left
                    //headPart.BodyPartTransform.DOLocalRotate(sneakyAI.leftHeadRotation, sneakyAI.headTurnDuration);
                    //float newZAngle = Mathf.SmoothDamp(headPart.BodyPartConfigJoint.targetAngularVelocity.z, sneakyAI.lookLeftPose.z, ref yVelocity, smoothTime);
                    //headPart.BodyPartConfigJoint.targetAngularVelocity = new Vector3(sneakyAI.lookLeftPose.x, sneakyAI.lookLeftPose.y, newZAngle);

                    DOTween.To(() => headPart.BodyPartConfigJoint.targetAngularVelocity, x => headPart.BodyPartConfigJoint.targetAngularVelocity = x, sneakyAI.lookLeftPose, sneakyAI.headTurnDuration);
                    //DOTween.To(() => headPart.BodyPartConfigJoint.targetAngularVelocity, x => headPart.BodyPartConfigJoint.targetAngularVelocity = x, new Vector3(2, 2, 2), 1);

                    break;
                case 2:
                    //Look Right
                    //headPart.BodyPartTransform.DOLocalRotate(sneakyAI.rightHeadRotation, sneakyAI.headTurnDuration);
                    //float newZAngle2 = Mathf.SmoothDamp(headPart.BodyPartConfigJoint.targetAngularVelocity.z, sneakyAI.lookRightPose.z, ref yVelocity, smoothTime);
                    //headPart.BodyPartConfigJoint.targetAngularVelocity = new Vector3(sneakyAI.lookRightPose.x, sneakyAI.lookRightPose.y, newZAngle2);


                    DOTween.To(() => headPart.BodyPartConfigJoint.targetAngularVelocity, x => headPart.BodyPartConfigJoint.targetAngularVelocity = x, sneakyAI.lookRightPose, sneakyAI.headTurnDuration);

                    //Mathf.SmoothDamp(camTargetGroup.m_Targets[i].weight, (float)1 / (float)(i + 5), ref yVelocity, smoothTime);
                    //headPart.BodyPartConfigJoint.targetAngularVelocity = sneakyAI.lookRightPose;
                    break;

            }
        }
         
        return TaskStatus.Success;
	}

    public override void OnEnd()
    {
        base.OnEnd();
        if (lookSideIndex.Value + 1 < sneakyAI.lookSides.Count)
        {
            lookSideIndex.Value = lookSideIndex.Value + 1;
        }
        else
        {
            lookSideIndex.Value = 0;
        }

    }
}