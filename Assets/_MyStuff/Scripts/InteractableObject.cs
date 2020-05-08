using System;
using UnityEngine;
using garagekitgames;

public class InteractableObject : MonoBehaviour
{
    public enum Priority
    {
        Ignore,
        Lowest,
        Low,
        Medium,
        High,
        Highest,
        Forced
    }

    public enum Damage
    {
        Ignore,
        Default,
        Object,
        Punch,
        Kick,
        DivingKick,
        Headbutt,
        DivingHeadbutt,
        Knockout,
        Weapon
    }

    public enum Grab
    {
        Ignore,
        Break,
        Drain,
        Perminant,
        Climb
    }

    public enum State
    {
        Ignore,
        Default,
        InHand,
        Thrown,
        Stationary,
        Moving
    }

    public InteractableObject.Priority priorityModifier = InteractableObject.Priority.High;

    public InteractableObject.Grab grabModifier = InteractableObject.Grab.Break;

    public InteractableObject.Damage damageModifier = InteractableObject.Damage.Default;

    [Tooltip("Enable if the rigidbody is part of a ragdoll (This is a workaround for the extra force thats required to lift a ragdoll)")]
    public bool partOfRagdoll = true;

    [HideInInspector]
    public Transform cachedTransform;

    [HideInInspector]
    public Rigidbody cachedRigidbody;

    [HideInInspector]
    public Collider cachedCollider;

    public bool checkVelocity;

    [HideInInspector]
    public static float offset;

    public CharacterThinker character;

    public CharacterLimpSetting limpSetting;

    //public 

    public bool isGrabbed = false;

    public bool notLimp = true;
    private void Start()
    {

        this.cachedTransform = base.transform;
        this.cachedRigidbody = this.cachedTransform.GetComponent<Rigidbody>();
        this.cachedCollider = this.cachedTransform.GetComponent<Collider>();
        character = transform.root.GetComponent<CharacterThinker>();
        if(character)
        {
            limpSetting = character.GetComponent<CharacterLimpSetting>();
        }
        
        /*if (this.checkVelocity)
        {
            base.InvokeRepeating("CheckVelocity", InteractableObject.offset, 0.5f);
            InteractableObject.offset += 0.01f;
        }*/
    }

    public void CheckVelocity()
    {
        if (this.cachedRigidbody.velocity.sqrMagnitude > 10f)
        {
            this.damageModifier = InteractableObject.Damage.Object;
        }
        else
        {
            this.damageModifier = InteractableObject.Damage.Default;
        }
    }

    private void Update()
    {
        if(character)
        {
            /*if(isGrabbed)
            {
                character.grabCount += 1;
            }
            else
            {
                character.grabCount -= 1;
            }*/

            if (character.isGrabbed)
            {
                
                cachedRigidbody.useGravity = false;
                
            }

            if (!character.isGrabbed)
            {
                cachedRigidbody.useGravity = true;
                
            }
        }
       
    }
}
