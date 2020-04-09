using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
using garagekitgames.shooter;
using UnityEngine.AI;
public class Shooting : Action
{
    public CharacterThinker character;
    public EnemyAwareness enemyAwareness;
    public EnemyAIBase enemyAI;
    public CharacterShooting shooting;
    public NavMeshAgent agent;
    public override void OnAwake()
    {
        base.OnAwake();
        character = GetComponent<CharacterThinker>();
        enemyAwareness = transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>();
        enemyAI = GetComponent<EnemyAIBase>();
        shooting = GetComponent<CharacterShooting>();
        agent = GetComponent<NavMeshAgent>();
    }
    public override void OnStart()
	{
        character.targetting = true;
    }

	public override TaskStatus OnUpdate()
	{
        enemyAI.canMove = false;
        agent.isStopped = true;
        shooting.Shoot();
        character.targetting = true;
        return TaskStatus.Success;
	}
}