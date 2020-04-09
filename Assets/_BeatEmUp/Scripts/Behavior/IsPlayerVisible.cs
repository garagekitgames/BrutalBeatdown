using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;

public class IsPlayerVisible : Conditional
{

    public CharacterThinker character;
    public EnemyAwareness enemyAwareness;
    public SharedTransform player;
    public SharedBool isPlayerVisible;
    public SharedVector3 playerLastSeenDestination;
    public SharedVector3 privateLastSeenDestination;

    public override void OnAwake()
    {
        base.OnAwake();
        character = GetComponent<CharacterThinker>();
        enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
        player.Value = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
    }


    public override void OnStart()
    {
        base.OnStart();
        
    }
    public override TaskStatus OnUpdate()
	{
        isPlayerVisible = enemyAwareness.canSeePlayer;
        if (enemyAwareness.canSeePlayer)
        {
            playerLastSeenDestination.Value = player.Value.position;
            enemyAwareness.playerLastSeen = player.Value.position;

            privateLastSeenDestination.Value = player.Value.position;
            enemyAwareness.privateLastSeen = player.Value.position;

            enemyAwareness.Alert(player.Value.position);
            //AudioManager.instance.Play("EnemyHuh");
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
		
	}

    public override void OnEnd()
    {
        base.OnEnd();
        
    }
}