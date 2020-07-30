using System;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using SO;
using System.Linq;

public class TargetCheck : MonoBehaviour
{
    public CharacterRuntimeSet playersAvailable;
    public  List<InteractableObject> interactablesInSphere = new List<InteractableObject>();

    public List<InteractableObject> interactablesInSphere2 = new List<InteractableObject>();

    public List<InteractableObject> withingAngle = new List<InteractableObject>();

    public  List<CharacterThinker> actorsInScene = new List<CharacterThinker>();

   // private static GameObject[] _throwPoints = null;

    public Collider[] _hitColliders = new Collider[96];

    private int _collidersFound;

    //[HideInInspector]
    public Transform tUpper;

    //[HideInInspector]
    public Transform tLower;

   // [HideInInspector]
    public Collider cUpper;

   // [HideInInspector]
    public Collider cLower;

    public InteractableObject upperInterest;

    public InteractableObject lowerIntrest;

    
    public float radius = 5f;

    
    public float fov = 90f;

    [HideInInspector]
    public bool targetInRange;

    //[HideInInspector]
    public CharacterThinker character;

    [HideInInspector]
    public bool initialized;

    public bool isBehindUpper;

    public bool isBehindLower;

  //  public GameObject throwTarget;

   // public ForceTests currentTargetActor;

    public CharacterThinker actorIntrest;

    // private const float RequiredThrowPointRescanTime = 0.25f;

    // private float currentThrowPointRescanTime;

    //  private ForceBeastTarget _forceTarget;


    public Vector3 globalPosition;

    public CharacterFaceDirection fD;

    public string baseFDPartName = "hip";

    public Jump characterJump;


    public enum TargetingType
    {
        Dynamic,
        ManualTargetByDistance,
        ManualTarget,
        NoTarget,
        DynamicTargetByDirection
    }

    public TargetCheck.TargetingType targetingType = TargetCheck.TargetingType.Dynamic;

    public CharacterThinker prevActorIntrest;
    public InteractableObject prevUpperInterest;

    public InteractableObject prevLowerIntrest;

    public int selectedCharacter;

    public GameObject longTarget;

    public List<GameObject> visibleTargets = new List<GameObject>();

    public bool debugLines = false;
    //public List<>

    public LayerMask targetMask;

    private void Awake()
    {
        if (interactablesInSphere == null)
        {
            interactablesInSphere = new List<InteractableObject>();
        }
        if (interactablesInSphere2 == null)
        {
            interactablesInSphere2 = new List<InteractableObject>();
        }
        withingAngle = new List<InteractableObject>();
        character = GetComponent<CharacterThinker>();
        characterJump = GetComponent<Jump>();
        //character = transform.GetComponent<CharacterThinker>();


        //if (TargetingHandeler._throwPoints == null)
        //{
        //    this.UpdateThrowpoints();
        //}
        //ForceBeastTarget targetOverride = MonoSingleton<Global>.Instance.GlobalCam.targetOverride;
        //if (targetOverride != null && targetOverride.enabled)
        //{
        //    this._forceTarget = targetOverride;
        //}
    }

    public void Start()
    {
        this.tUpper = character.bpHolder.BodyPartsName["head"].transform;
        this.cUpper = this.tUpper.GetComponent<Collider>();
        this.tLower = character.bpHolder.BodyPartsName["hip"].transform; ;
        this.cLower = this.tLower.GetComponent<Collider>();
        //this.actor = this;
        this.initialized = true;

        BodyPartMono bodyPartMono = character.bpHolder.BodyPartsName[baseFDPartName];
        fD = bodyPartMono.BodyPartFaceDirection;
    }
    private void UpdateHitColliders()
    {

        this._collidersFound = Physics.OverlapSphereNonAlloc(this.character.bpHolder.BodyPartsName["chest"].transform.position, this.radius, this._hitColliders, -1, QueryTriggerInteraction.Collide);
        
    }

    public void FindLongTarget()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(this.character.bpHolder.BodyPartsName["chest"].transform.position, 10, targetMask);

        
        
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                if (targetsInViewRadius[i].transform.root != this.character.transform.root)
                {
                    Transform target = targetsInViewRadius[i].transform;
                    Vector3 dirToTarget = (target.position - this.character.bpHolder.BodyPartsName["chest"].transform.position).normalized;
                    if (Vector3.Angle(fD.rigidbody.transform.TransformDirection(fD.bodyForward).normalized, dirToTarget) < 180 / 2)
                    {
                        float dstToTarget = Vector3.Distance(this.character.bpHolder.BodyPartsName["chest"].transform.position, target.position);
                        
                       visibleTargets.Add(target.gameObject);
                        if(debugLines)
                        {
                            Color color = Color.red;
                            Debug.DrawLine(this.character.bpHolder.BodyPartsName["chest"].transform.position, target.position, color);

                        }


                    
                    }
                }
                    

            }

        //Array.Sort<GameObject>(camTargetGroup.m_Targets, (x, y) => Vector3.Distance(Player.transform.position, x.target.transform.position)
        //                                                                        .CompareTo(Vector3.Distance(Player.transform.position, y.target.transform.position)));

        if(visibleTargets.Count > 0)
        {
            visibleTargets = visibleTargets.OrderBy(p => Vector3.Angle(fD.rigidbody.transform.TransformDirection(fD.bodyForward).normalized
                                                        , (p.transform.position - this.character.bpHolder.BodyPartsName["chest"].transform.position).normalized))
                                            //.OrderBy(p => Vector3.Distance(this.character.bpHolder.BodyPartsName["chest"].transform.position, p.transform.position))
                                            .ToList();

            var ct = visibleTargets[0].transform.root.GetComponent<CharacterThinker>();
            if(ct)
            {
                if(!ct.isGrabbed)
                {
                    longTarget = visibleTargets[0];
                    if (debugLines)
                    {
                        Color color = Color.green;
                        Debug.DrawLine(this.character.bpHolder.BodyPartsName["chest"].transform.position, longTarget.transform.position, color);

                    }
                }
                else
                {
                    longTarget = null;
                }
            }


            
        }
        else
        {
            longTarget = null;
        }

        character.longTarget = longTarget;
        
    }

    void ConvertMoveInputAndPassItToAnimator(Vector3 inputDirection)
    {
        //Convert the move input from world positions to local positions so that they have the correct values
        //depending on where we look
        Vector3 localMove = transform.InverseTransformDirection(inputDirection);
        //localMove.Normalize();
        float turnAmount = localMove.y;
        float forwardAmount = localMove.z;

        //
        globalPosition = fD.transform.position + fD.rigidbody.transform.TransformDirection(fD.bodyForward).normalized * 2;
        //targetTransform.transform.position = globalPosition;
        globalPosition.y = 1.8f;

        //character.target = globalPosition;

        /* if (turnAmount != 0)
             turnAmount *= 2;

         */
        //if (log)
        //{
        /*print("Forward : " + localMove);
            print("turnAmount : " + turnAmount);
            print("forwardAmount : " + forwardAmount);
            print("inputdirection : " + inputDirection);
            print("hipFacing : " + fD.bodyForward);*/
        //}

        //testVector2 = new Vector3(-forwardAmount, turnAmount, 0f);
    }

    public void Update()
    {
        //Vector3 inputDirection = character.inputDirection;

        //fD.bodyForward;
        //ConvertMoveInputAndPassItToAnimator(inputDirection);

        //CheckTargetingSphere();

        //if(character.ControlledBy == ForceTests.ControlledTypes.Human)
        //{
        //  character.target = upperIntrest.cachedTransform.position;
        //}

        // else if (character.ControlledBy == ForceTests.ControlledTypes.AI)
        //{

        //}
        if(characterJump)
        {
            if (characterJump.inAir)
            {
                targetingType = TargetingType.Dynamic;
            }
            else
            {
                targetingType = TargetingType.DynamicTargetByDirection;
            }
        }
        

        FindLongTarget();



        switch (targetingType)
        {
            case TargetingType.Dynamic:
                FindTargets();
                DynamicTarget();
                break;
            case TargetingType.ManualTargetByDistance:
                FindTargets();
                ManualTargetByDistance();
                break;
            case TargetingType.ManualTarget:
                FindTargets();
                ManualTarget();
                break;
            case TargetingType.NoTarget:
                FindTargets();
                NoTarget();
                break;
            case TargetingType.DynamicTargetByDirection:
                FindTargets2();
                TargetByDirection();
                DynamicTargetByDirection();
                break;
            default:
                break;
        }

    }

    public void ManualTargetByDistance()
    {
        actorIntrest = null;
        upperInterest = null;
        lowerIntrest = null;
        actorsInScene = playersAvailable.Items;
        /* foreach (var item in playersAvailable.Items)
         {
             actorsInScene.Add(item);
         }*/

        actorIntrest = actorsInScene.Where( x => x.teamID != this.character.teamID).OrderBy(t => Vector3.Distance(character.bpHolder.BodyPartsName["head"].transform.position, t.bpHolder.BodyPartsName["head"].transform.position)).FirstOrDefault();

        if(actorIntrest)
        {
            upperInterest = actorIntrest.bpHolder.BodyPartsName["head"].transform.GetComponent<InteractableObject>();
            lowerIntrest = actorIntrest.bpHolder.BodyPartsName["hip"].transform.GetComponent<InteractableObject>();
            character.target = upperInterest.cachedTransform.position;
            character.objectOfInterest = upperInterest;
            character.grabObjectOfInterest = upperInterest;
            character.targetting = true;
        }
        else
        {
            character.target = FindEmptyTarget(character.inputDirection);
            character.objectOfInterest = null;
            character.grabObjectOfInterest = null;
            character.targetting = false;
        }
        

    }

    public void ManualTarget()
    {
        actorIntrest = null;
        upperInterest = null;
        lowerIntrest = null;
        actorsInScene = playersAvailable.Items;
        /* foreach (var item in playersAvailable.Items)
         {
             actorsInScene.Add(item);
         }*/
        actorsInScene = actorsInScene.Where(x => x.teamID != this.character.teamID).ToList<CharacterThinker>();
        int choice;
        if (actorsInScene.Count > 0)
        {
            
            if (character.player.GetButtonDown("SwitchTargetRight"))
            {
                /*selectedCharacter++;
                if (selectedCharacter >= actorsInScene.Count - 1)
                {
                    selectedCharacter = 0;
                }*/

                selectedCharacter++;
               // choice = selectedCharacter % actorsInScene.Count;
            }
            if (character.player.GetButtonDown("SwitchTargetLeft"))
            {
               /* selectedCharacter--;
                if (selectedCharacter <= 0)
                {
                    selectedCharacter = actorsInScene.Count - 1;
                }*/

                selectedCharacter--;
                
            }
            choice = Mathf.Abs(selectedCharacter % actorsInScene.Count);
            //selectedCharacter = Mathf.Clamp(selectedCharacter, 0, actorsInScene.Count);
            actorIntrest = actorsInScene[choice];
        }
        

        if (actorIntrest)
        {
            upperInterest = actorIntrest.bpHolder.BodyPartsName["head"].transform.GetComponent<InteractableObject>();
            lowerIntrest = actorIntrest.bpHolder.BodyPartsName["hip"].transform.GetComponent<InteractableObject>();
            character.target = upperInterest.cachedTransform.position;
            character.objectOfInterest = upperInterest;
            character.grabObjectOfInterest = upperInterest;
            character.targetting = true;

            // if actor Interest is down, set the target to loweinterest
        }
        else
        {
            character.target = FindEmptyTarget(character.inputDirection);
            character.objectOfInterest = null;
            character.grabObjectOfInterest = null;
            character.targetting = false;
        }


    }
    public void NoTarget()
    {
        character.target = FindEmptyTarget(character.inputDirection);
        character.objectOfInterest = null;
        character.grabObjectOfInterest = null;
        character.targetting = false;

    }
    public void DynamicTarget()
    {
        if(upperInterest || lowerIntrest)
        {
            if(upperInterest)
            {
                character.target = upperInterest.cachedTransform.position;
                character.objectOfInterest = upperInterest;
                if(character.rightgrab || character.leftgrab)
                {
                    character.targetting = false; //change it to true, if you want character to look at the target
                }
                else
                {
                    character.targetting = true; //change it to true, if you want character to look at the target
                }
                
                if (upperInterest.character)
                {
                    actorIntrest = upperInterest.character;
                }
            }
            else if(lowerIntrest)
            {
                character.target = lowerIntrest.cachedTransform.position;
                character.objectOfInterest = lowerIntrest;
                if (character.rightgrab || character.leftgrab)
                {
                    character.targetting = false; //change it to true, if you want character to look at the target
                }
                else
                {
                    character.targetting = true; //change it to true, if you want character to look at the target
                }
                if (lowerIntrest.character)
                {
                    actorIntrest = lowerIntrest.character;
                }
            }

            if (lowerIntrest)
            {
                //character.target = lowerIntrest.cachedTransform.position;
                character.grabObjectOfInterest = lowerIntrest;
                if (character.rightgrab || character.leftgrab)
                {
                    character.targetting = false; //change it to true, if you want character to look at the target
                }
                else
                {
                    character.targetting = true; //change it to true, if you want character to look at the target
                }
                /*if (lowerIntrest.character)
                {
                    actorIntrest = lowerIntrest.character;
                }*/
            }

           // if(character.)

        }
        else
        {
            character.target = FindEmptyTarget(character.inputDirection);
            character.objectOfInterest = null;
            character.grabObjectOfInterest = null;
            character.targetting = false;
        }

        /*
        if (upperInterest)
        {
            character.target = upperInterest.cachedTransform.position;
            if(upperInterest.character)
            {
                actorIntrest = upperInterest.character;
            }
            
            character.targetting = true;
        }
        else
        {
            character.target = FindEmptyTarget(character.inputDirection);
            character.targetting = false;
        }

        if (lowerIntrest)
        {
            if (lowerIntrest.character)
            {
                actorIntrest = lowerIntrest.character;
            }
            //actorIntrest = lowerIntrest.character;
        }
        else
        {

        }

        if (actorIntrest && lowerIntrest)
        {
            character.target = lowerIntrest.cachedTransform.position;
            character.targetting = true;
        }
        else if (actorIntrest && upperInterest)
        {
            character.target = upperInterest.cachedTransform.position;
            character.targetting = true;
        }
        else
        {
            character.target = FindEmptyTarget(character.inputDirection);
            character.targetting = false;
        }*/
    }

    public void DynamicTargetByDirection()
    {
        if (upperInterest || lowerIntrest)
        {
            if (upperInterest)
            {
                character.target = upperInterest.cachedTransform.position;
                character.objectOfInterest = upperInterest;
                if (character.rightgrab || character.leftgrab)
                {
                    character.targetting = false; //change it to true, if you want character to look at the target
                }
                else
                {
                    character.targetting = true; //change it to true, if you want character to look at the target
                }

                if (upperInterest.character)
                {
                    actorIntrest = upperInterest.character;
                }
            }
            else if (lowerIntrest)
            {
                character.target = lowerIntrest.cachedTransform.position;
                character.objectOfInterest = lowerIntrest;
                if (character.rightgrab || character.leftgrab)
                {
                    character.targetting = false; //change it to true, if you want character to look at the target
                }
                else
                {
                    character.targetting = true; //change it to true, if you want character to look at the target
                }
                if (lowerIntrest.character)
                {
                    actorIntrest = lowerIntrest.character;
                }
            }

            if (lowerIntrest)
            {
                //character.target = lowerIntrest.cachedTransform.position;
                character.grabObjectOfInterest = lowerIntrest;
                if (character.rightgrab || character.leftgrab)
                {
                    character.targetting = false; //change it to true, if you want character to look at the target
                }
                else
                {
                    character.targetting = true; //change it to true, if you want character to look at the target
                }
                
            }



        }
        else
        {
            character.target = FindEmptyTarget(character.inputDirection);
            character.objectOfInterest = null;
            character.grabObjectOfInterest = null;
            character.targetting = false;
        }

    }
    public Vector3 FindEmptyTarget(Vector3 inputDirection)
    {
        //Convert the move input from world positions to local positions so that they have the correct values
        //depending on where we look
        Vector3 localMove = transform.InverseTransformDirection(inputDirection);
        //localMove.Normalize();
        float turnAmount = localMove.y;
        float forwardAmount = localMove.z;

        //
       // globalPosition = fD.transform.position + fD.rigidbody.transform.TransformDirection(fD.bodyForward).normalized * 2;

        if(inputDirection == Vector3.zero)
        {
            globalPosition = fD.transform.position + fD.rigidbody.transform.TransformDirection(fD.bodyForward).normalized * 2;
        }
        else
        {
            globalPosition = fD.transform.position + localMove * 2;
        }
        

        Debug.DrawLine(fD.transform.position, globalPosition, Color.blue);

        //targetTransform.transform.position = globalPosition;
        globalPosition.y = 1.8f;

        //character.target = globalPosition;

        return globalPosition;
        /* if (turnAmount != 0)
             turnAmount *= 2;

         */
        //if (log)
        //{
        /*print("Forward : " + localMove);
            print("turnAmount : " + turnAmount);
            print("forwardAmount : " + forwardAmount);
            print("inputdirection : " + inputDirection);
            print("hipFacing : " + fD.bodyForward);*/
        //}

        //testVector2 = new Vector3(-forwardAmount, turnAmount, 0f);
    }

    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(upperIntrest.cachedTransform.position, 1);
    }*/

    public void TargetByDirection()
    {
        if (character.rightgrab || character.leftgrab)
        {
            return;
        }
        if(withingAngle != null)
        {
            withingAngle.Clear();
        }
        


        if (interactablesInSphere != null)
        {
            if (true)
            {
                this.CheckTargetingSphere();
                if (interactablesInSphere.Count > 0)
                {
                    foreach (var item in interactablesInSphere)
                    {
                        Vector3 enemyDir = item.transform.position - character.bpHolder.BodyPartsName["hip"].transform.position;

                        float angle = Vector3.Angle(enemyDir, character.inputDirection);

                        if(angle <= this.fov)
                        {
                            withingAngle.Add(item);
                        }

                    }
                }
            }
        }
    }
    private void CheckTargetingSphere()
    {
        this.UpdateHitColliders();
        interactablesInSphere.Clear();
        interactablesInSphere2.Clear();
        //actorsInSphere.Clear();
        for (int i = 0; i < this._collidersFound; i++)
        {
            if (this._hitColliders[i].transform.root != this.character.transform.root)
            {
                CharacterThinker component = this._hitColliders[i].transform.root.GetComponent<CharacterThinker>();
                InteractableObject component2 = this._hitColliders[i].GetComponent<InteractableObject>();
                //if (!(component != null) || component.teamID != this.character.teamID)
                //{
                    if (component2 != null && component2.priorityModifier != InteractableObject.Priority.Ignore && component2.character != null )
                    {
                        if(component2.character.teamID != this.character.teamID)
                        {
                            interactablesInSphere.Add(component2);
                            interactablesInSphere2.Add(component2);
                        }
                        

                    }
                    else if (component2 != null && component2.priorityModifier != InteractableObject.Priority.Ignore && component2.character == null)
                    {
                        //add to objects in sphere 
                        interactablesInSphere.Add(component2);
                    }
                    if (this.character.amIMainPlayer)
                    {
                        //actorsInSphere.Add(component);
                    }
                //}
                
            }
        }
    }
    /*
    public void UpdateThrowpoints()
    {
        TargetCheck._throwPoints = GameObject.FindGameObjectsWithTag("Throw Point");
    }*/

    public void FindTargets()
    {
        if (this.initialized)
        {
            
            if (upperInterest != null)
            {
                this.prevUpperInterest = upperInterest;
            }
            if (lowerIntrest != null)
            {
                this.prevLowerIntrest = lowerIntrest;
            }
            if (actorIntrest != null)
            {
                this.prevActorIntrest = actorIntrest;
            }


            this.upperInterest = null;
            this.lowerIntrest = null;
            this.isBehindUpper = false;
            this.isBehindLower = false;
            if (interactablesInSphere != null)
            {
                if (true)
                {
                    this.CheckTargetingSphere();
                    if (interactablesInSphere.Count > 0)
                    {
                        float num = 0f;
                        float num2 = float.PositiveInfinity;
                        float num3 = 0f;
                        float num4 = float.PositiveInfinity;
                        for (int i = 0; i < interactablesInSphere.Count; i++)
                        {
                            if (interactablesInSphere[i] != null)
                            {
                                Vector3 a = Vector3.zero;
                                Vector3 vector = a - this.cUpper.bounds.center;
                                Vector3 vector2 = a - this.cLower.bounds.center;
                                float num5;
                                //switch (character.ControlledBy)
                                //{
                                    //case ForceTests.ControlledTypes.Human:
                                        a = interactablesInSphere[i].cachedCollider.ClosestPoint(this.tUpper.position);
                                        num5 = Vector3.SqrMagnitude(a - (this.cUpper.bounds.center + -this.tUpper.up / 4f + this.character.inputDirection / 2f));
                                       // break;
                                   
                                    //default:
                                     //   a = interactablesInSphere[i].cachedCollider.ClosestPoint(this.tUpper.position);
                                     //   num5 = Vector3.SqrMagnitude(a - (this.cUpper.bounds.center + -this.tUpper.up / 4f));
                                       // break;
                                //}
                                float num6 = Vector3.SqrMagnitude(a - (this.cLower.bounds.center + this.tLower.forward / 4f + -this.tLower.up / 4f + this.character.inputDirection));
                                float num7 = Vector3.Angle(a - this.cUpper.bounds.center, this.tUpper.forward + this.tUpper.position);
                                float num8 = Vector3.Angle(a - this.cLower.bounds.center, this.tLower.forward + this.tLower.position);
                                if (((num5 < 3f && num7 <= this.fov) || (num5 < 1.5f && num7 > this.fov)) && a.y > this.tUpper.position.y - 1f && a.y < this.tUpper.position.y + 1.5f)
                                {
                                    switch (interactablesInSphere[i].priorityModifier)
                                    {
                                        case InteractableObject.Priority.Lowest:
                                            num = num5 - 0.2f;
                                            break;
                                        case InteractableObject.Priority.Low:
                                            num = num5 - 0.3f;
                                            break;
                                        case InteractableObject.Priority.Medium:
                                            num = num5 - 0.4f;
                                            break;
                                        case InteractableObject.Priority.High:
                                            num = num5 - 0.5f;
                                            break;
                                        case InteractableObject.Priority.Highest:
                                            num = num5 - 0.5f;
                                            break;
                                        case InteractableObject.Priority.Forced:
                                            num = num5 - 100f;
                                            break;
                                    }
                                    if (num7 > this.fov)
                                    {
                                        num += 0.1f;
                                    }
                                    if (num5 <= num2)
                                    {
                                        this.upperInterest = interactablesInSphere[i];
                                        num2 = num;
                                        if (num7 > 120f)
                                        {
                                        }
                                    }
                                }
                                if (((num6 < 2f && num8 <= this.fov) || (num6 < 2f && num8 > this.fov)) && a.y > this.tLower.position.y - 1f && a.y < this.tLower.position.y + 1f)
                                {
                                    switch (interactablesInSphere[i].priorityModifier)
                                    {
                                        case InteractableObject.Priority.Lowest:
                                            num3 = num6 - 0.2f;
                                            break;
                                        case InteractableObject.Priority.Low:
                                            num3 = num6 - 0.3f;
                                            break;
                                        case InteractableObject.Priority.Medium:
                                            num3 = num6 - 0.4f;
                                            break;
                                        case InteractableObject.Priority.High:
                                            num3 = num6 - 0.5f;
                                            break;
                                        case InteractableObject.Priority.Highest:
                                            num3 = num6 - 0.5f;
                                            break;
                                        case InteractableObject.Priority.Forced:
                                            num3 = num6 - 100f;
                                            break;
                                    }
                                    if (num8 > this.fov)
                                    {
                                        num3 += 0.2f;
                                    }
                                    if (num6 <= num4)
                                    {
                                        this.lowerIntrest = interactablesInSphere[i];
                                        num4 = num3;
                                        if (num8 > 100f)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                /*else if (this.currentTargetActor != null)
                {
                    this.upperIntrest = this.currentTargetActor.bodyHandeler.transform.GetComponent<InteractableObject>();
                    this.lowerIntrest = this.currentTargetActor.bodyHandeler.transform.GetComponent<InteractableObject>();
                }*/
            }
        }
    }

    public void FindTargets2()
    {
        if (this.initialized)
        {

            if (upperInterest != null)
            {
                this.prevUpperInterest = upperInterest;
            }
            if (lowerIntrest != null)
            {
                this.prevLowerIntrest = lowerIntrest;
            }
            if (actorIntrest != null)
            {
                this.prevActorIntrest = actorIntrest;
            }


            this.upperInterest = null;
            this.lowerIntrest = null;
            this.isBehindUpper = false;
            this.isBehindLower = false;
            if (withingAngle != null)
            {
                if (true)
                {
                    this.CheckTargetingSphere();
                    if (withingAngle.Count > 0)
                    {
                        float num = 0f;
                        float num2 = float.PositiveInfinity;
                        float num3 = 0f;
                        float num4 = float.PositiveInfinity;
                        for (int i = 0; i < withingAngle.Count; i++)
                        {
                            if (withingAngle[i] != null)
                            {
                                Vector3 a = Vector3.zero;
                                Vector3 vector = a - this.cUpper.bounds.center;
                                Vector3 vector2 = a - this.cLower.bounds.center;
                                float num5;
                                //switch (character.ControlledBy)
                                //{
                                //case ForceTests.ControlledTypes.Human:
                                a = withingAngle[i].cachedCollider.ClosestPoint(this.tUpper.position);
                                num5 = Vector3.SqrMagnitude(a - (this.cUpper.bounds.center + -this.tUpper.up / 4f + this.character.inputDirection / 2f));
                                // break;

                                //default:
                                //   a = interactablesInSphere[i].cachedCollider.ClosestPoint(this.tUpper.position);
                                //   num5 = Vector3.SqrMagnitude(a - (this.cUpper.bounds.center + -this.tUpper.up / 4f));
                                // break;
                                //}
                                float num6 = Vector3.SqrMagnitude(a - (this.cLower.bounds.center + this.tLower.forward / 4f + -this.tLower.up / 4f + this.character.inputDirection));
                                float num7 = Vector3.Angle(a - this.cUpper.bounds.center, this.tUpper.forward + this.tUpper.position);
                                float num8 = Vector3.Angle(a - this.cLower.bounds.center, this.tLower.forward + this.tLower.position);
                                if (((num5 < 3f && num7 <= this.fov) || (num5 < 1.5f && num7 > this.fov)) && a.y > this.tUpper.position.y - 1f && a.y < this.tUpper.position.y + 1.5f)
                                {
                                    switch (withingAngle[i].priorityModifier)
                                    {
                                        case InteractableObject.Priority.Lowest:
                                            num = num5 - 0.2f;
                                            break;
                                        case InteractableObject.Priority.Low:
                                            num = num5 - 0.3f;
                                            break;
                                        case InteractableObject.Priority.Medium:
                                            num = num5 - 0.4f;
                                            break;
                                        case InteractableObject.Priority.High:
                                            num = num5 - 0.5f;
                                            break;
                                        case InteractableObject.Priority.Highest:
                                            num = num5 - 0.5f;
                                            break;
                                        case InteractableObject.Priority.Forced:
                                            num = num5 - 100f;
                                            break;
                                    }
                                    if (num7 > this.fov)
                                    {
                                        num += 0.1f;
                                    }
                                    if (num5 <= num2)
                                    {
                                        this.upperInterest = withingAngle[i];
                                        num2 = num;
                                        if (num7 > 120f)
                                        {
                                        }
                                    }
                                }
                                if (((num6 < 2f && num8 <= this.fov) || (num6 < 2f && num8 > this.fov)) && a.y > this.tLower.position.y - 1f && a.y < this.tLower.position.y + 1f)
                                {
                                    switch (withingAngle[i].priorityModifier)
                                    {
                                        case InteractableObject.Priority.Lowest:
                                            num3 = num6 - 0.2f;
                                            break;
                                        case InteractableObject.Priority.Low:
                                            num3 = num6 - 0.3f;
                                            break;
                                        case InteractableObject.Priority.Medium:
                                            num3 = num6 - 0.4f;
                                            break;
                                        case InteractableObject.Priority.High:
                                            num3 = num6 - 0.5f;
                                            break;
                                        case InteractableObject.Priority.Highest:
                                            num3 = num6 - 0.5f;
                                            break;
                                        case InteractableObject.Priority.Forced:
                                            num3 = num6 - 100f;
                                            break;
                                    }
                                    if (num8 > this.fov)
                                    {
                                        num3 += 0.2f;
                                    }
                                    if (num6 <= num4)
                                    {
                                        this.lowerIntrest = withingAngle[i];
                                        num4 = num3;
                                        if (num8 > 100f)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                /*else if (this.currentTargetActor != null)
                {
                    this.upperIntrest = this.currentTargetActor.bodyHandeler.transform.GetComponent<InteractableObject>();
                    this.lowerIntrest = this.currentTargetActor.bodyHandeler.transform.GetComponent<InteractableObject>();
                }*/
            }
        }
    }


    /*
        public void FindActors()
        {
            if (this.initialized && this.myForceTest.ControlledBy == ForceTests.ControlledTypes.Animation)
            {
                if (this.myForceTest.bodyHandeler.leftGrabJoint != null && this.myForceTest.bodyHandeler.rightGrabJoint != null)
                {
                    if (this.throwTarget == null)
                    {
                        if (this.currentThrowPointRescanTime >= 0.25f || TargetCheck._throwPoints.Length == 0 || TargetCheck._throwPoints[0] == null)
                        {
                            this.UpdateThrowpoints();
                        }
                        else
                        {
                            this.currentThrowPointRescanTime += Time.deltaTime;
                        }
                        if (TargetCheck._throwPoints != null && TargetCheck._throwPoints.Length > 0)
                        {
                            float num = float.PositiveInfinity;
                            for (int i = 0; i < TargetCheck._throwPoints.Length; i++)
                            {
                                float num2 = Vector3.SqrMagnitude(TargetCheck._throwPoints[i].transform.position - this.cUpper.bounds.center);
                                if (num2 <= num)
                                {
                                    this.throwTarget = TargetCheck._throwPoints[i];
                                    num = num2;
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.throwTarget = null;
                }
                if (this.currentTargetActor != null && this.currentTargetActor.actorState == Actor.ActorState.Dead)
                {
                    this.currentTargetActor = null;
                }
                Actor actor = this.currentTargetActor;
                if (TargetCheck.actorsInSphere != null)
                {
                    List<Actor> cachedActors = Actor.CachedActors;
                    for (int j = 0; j < cachedActors.Count; j++)
                    {
                        if (cachedActors[j].gangID != this.myForceTest.gangID && cachedActors[j].actorState != Actor.ActorState.Dead)
                        {
                            TargetCheck.actorsInSphere.Add(cachedActors[j]);
                        }
                    }
                    if (TargetCheck.actorsInSphere.Count > 0)
                    {
                        float num3 = float.PositiveInfinity;
                        if (this.currentTargetActor != null)
                        {
                            Transform partTransform = this.currentTargetActor.bodyHandeler.Head.PartTransform;
                            Vector3 position = partTransform.position;
                            Vector3 vector = partTransform.position - this.cUpper.bounds.center;
                            num3 = Vector3.SqrMagnitude(position - this.cUpper.bounds.center);
                            actor = this.currentTargetActor;
                        }
                        for (int k = 0; k < TargetCheck.actorsInSphere.Count; k++)
                        {
                            Transform partTransform = TargetCheck.actorsInSphere[k].bodyHandeler.Head.PartTransform;
                            Vector3 position = partTransform.position;
                            Vector3 vector = partTransform.position - this.cUpper.bounds.center;
                            float num4 = Vector3.SqrMagnitude(position - this.cUpper.bounds.center);
                            if (num4 <= num3)
                            {
                                actor = TargetCheck.actorsInSphere[k];
                                num3 = num4;
                            }
                        }
                    }
                }
                if (actor != null)
                {
                    if (this.currentTargetActor != null)
                    {
                        if (this.currentTargetActor.bodyHandeler.Root.PartTransform != actor.transform.root)
                        {
                            this.currentTargetActor = actor;
                        }
                    }
                    else
                    {
                        this.currentTargetActor = actor;
                    }
                }
                if (this.currentTargetActor != null)
                {
                    this.actorIntrest = this.currentTargetActor.bodyHandeler.Chest.PartTransform;
                }
            }
        }*/
}
