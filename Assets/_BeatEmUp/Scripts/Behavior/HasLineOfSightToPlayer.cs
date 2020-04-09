using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;

public class HasLineOfSightToPlayer : Conditional
{
    public CharacterThinker character;
    public EnemyAwareness enemyAwareness;
    public SharedTransform player;
    public SharedBool hasLineOfSightToPlayer;
    public SharedVector3 playerLastSeenDestination;
    public SharedVector3 privateLastSeenDestination;
    public SharedBool facingPlayer;

    public override void OnAwake()
    {
        base.OnAwake();
        character = GetComponent<CharacterThinker>();
        enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
        player.Value = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;

    }
    public override TaskStatus OnUpdate()
	{
        hasLineOfSightToPlayer.Value = enemyAwareness.hasLosToPlayer;
        facingPlayer.Value = enemyAwareness.facingPlayer;
        if (hasLineOfSightToPlayer.Value)
        {
            playerLastSeenDestination.Value = player.Value.position;
            enemyAwareness.playerLastSeen = player.Value.position;

            privateLastSeenDestination.Value = player.Value.position;
            enemyAwareness.privateLastSeen = player.Value.position;

            enemyAwareness.Alert(player.Value.position);
            return TaskStatus.Success;
        }
        else
        {

            return TaskStatus.Failure;
        }
    }
}