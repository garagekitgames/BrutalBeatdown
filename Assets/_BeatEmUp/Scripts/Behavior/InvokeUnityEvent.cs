using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.Events;
public class InvokeUnityEvent : Action
{
    public UnityEvent eventToInvoke;
	public override void OnStart()
	{
        eventToInvoke.Invoke();

    }

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}