using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;

public class IsPlayerLastSeen : Conditional
{

    public CharacterThinker character;
    public EnemyAwareness enemyAwareness;
    public SharedTransform player;
    public SharedBool isPlayerLastSeen;
    public SharedVector3 playerLastSeenDestination;

    public override void OnAwake()
    {
        base.OnAwake();
        character = GetComponent<CharacterThinker>();
        enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
        player.Value = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
    }

    public override TaskStatus OnUpdate()
	{
        isPlayerLastSeen.Value = !(enemyAwareness.playerLastSeen == enemyAwareness.defaultLastSeen);
        if (isPlayerLastSeen.Value)
        {
            playerLastSeenDestination.Value = enemyAwareness.playerLastSeen;
            return TaskStatus.Success;
        }
        else
        {

            return TaskStatus.Failure;
        }
		
	}
}