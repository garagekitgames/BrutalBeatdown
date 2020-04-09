using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using garagekitgames;
public class IsAlive : Conditional
{
    public CharacterThinker character;
    public SharedBool isAlive;

    public override void OnAwake()
    {
        base.OnAwake();
        character = GetComponent<CharacterThinker>();
    }

    public override TaskStatus OnUpdate()
	{
        isAlive.Value = character.health.alive;
        if (character.health.alive)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
		
	}
}