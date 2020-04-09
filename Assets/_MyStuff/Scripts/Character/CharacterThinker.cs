using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EZCameraShake;
using DamageEffect;
using System.Linq;

using Rewired;

namespace garagekitgames
{
    public class CharacterThinker : MonoBehaviour
    {
        public CharacterRuntimeSet RuntimeSet;
        public CharacterBodyPartHolder bpHolder = new CharacterBodyPartHolder();

        public List<CharacterAction> actions = new List<CharacterAction>();

        private Dictionary<string, object> memory;

        public CharacterInput input;

        public int playerId = 0;
        public Player player; // The Rewired Player

        public CharacterLegsSimple1 legs; //this is too complex to refactor

        public LegsConfig legConfig;

        public float speed = 100;
        public float normalSpeed;
        public float windUpSpeed;

        public float moveMagnitude;
        public List<AttackData> attacks = new List<AttackData>();

        public List<AttackData> primaryAttacks = new List<AttackData>();
        public List<AttackData> secondaryAttacks = new List<AttackData>();

        public Vector3 target; //Later replace this with the set target action

        public AttackData LP;
        public AttackData RP;
        public AttackData LK;
        public AttackData RK;

        public bool LPwindUp = false;
        public bool LPattack = false;
        public float LPattackButtonClickTimer;
        public float LPattackPower;
        public float LPattackTimer;
        public float LPwindTimer;

        public bool RPwindUp = false;
        public bool RPattack = false;
        public float RPattackButtonClickTimer;
        public float RPattackPower;
        public float RPattackTimer;
        public float RPwindTimer;

        public bool LKwindUp = false;
        public bool LKattack = false;
        public float LKattackButtonClickTimer;
        public float LKattackPower;
        public float LKattackTimer;
        public float LKwindTimer;

        public bool RKwindUp = false;
        public bool RKattack = false;
        public float RKattackButtonClickTimer;
        public float RKattackPower;
        public float RKattackTimer;
        public float RKwindTimer;

        //----------------------------------Attack hit miss stuff


        public bool youHitMe;

        public int successHitCounter;
        public int missedHitCounter;
        public int attackCounter;

        public bool youMissed;

        public bool attacking =  false;


        public bool bleedNow = false;
        public bool breakFace = false;

        public bool hurt = false;
        public bool canBeHurt = true;
        public float nextHurtTime;
        public float timeBetweenGettingHurt = 0.2f;
        //----------------------------------------------------------

        public bool targetting = true;

        public bool stopDoingShit = false;

        public bool stopAttacking = false;
        public bool stopLooking = false;

        public bool enableDrag = true;

        public int teamID = 1;

        public bool amIMainPlayer =  false;

        public NavMeshAgent agent;
        //-----------------------------------------Action Attributes-----------------------------
        public Vector3 currentFacing;
        public Vector3 targetDirection;
        public bool walking;
        public Vector3 inputDirection;

        //attack data 
        public AttackData currentAttack;
        public bool windUp = false;
        public bool attack = false;

        public bool simplewind = false;
        public bool simpleattack = false;
        //public AttackData currentAttack;
        public float attackButtonClickTimer;
        public float attackPower;
        public float attackTimer;
        public float windTimer;
        public Vector3 attackTarget;

        private DamageEffectScript damageEffectScript;
        public CharacterHealth health;

        public bool canDamage = true;

        public List<DamageData> damages = new List<DamageData>();
        //public AttackData currentAttack;
        //public GameObject targetGO;


        //death cry
        public BodyPart cryBodyPart;
        public BodyPartMono cryPart;

        public InteractableObject objectOfInterest;

        public InteractableObject grabObjectOfInterest;

        public Transform lastHurtObject;

        public bool grounded;

        public bool isHurting;

        public bool isGrabbed;

       // public int grabCount = 0;

        [Header("Grab Settings")]
        public bool leftgrab;
        public bool rightgrab;
        public float leftbuttonClickTimer = 0f;
        public float rightbuttonClickTimer = 0f;

        public bool isGrabbing = false;

        

        [Header("Push & Pull Settings")]

        public Vector3 rightStickInputDirection;
        public Vector3 pullForceVector;

        public float horizontalPullForce = 800f;
        public float verticalPullForce = 1000f;

        public float xForceDirection = 0f, zForceDirection = 0f;

        [Header("Throw Settings")]
        public bool leftThrow = false;
        public bool rightThrow = false;

        public float throwPower;
        public float minThrow = 100f;
        public float maxThrow = 400f;

        public float humanThrowScale = 15f;

        public float simpleThrwoButtonClickTimer = 0f;

        [Header("Slam Settings")]
        public bool slam = false;
        public bool slamming = false;

        public float slamTime = 1f;
        public float slamTimer = 0f;

        [Header("Navigation AI Settings")]
        public AstarAI navigator;

        public bool movingAway;


        public bool canBeSeen;
        public bool isSeen;

        public bool isCaught;

        public void DeathCry()
        {
            EffectsController.Instance.PlayDeathSound(cryPart.BodyPartTransform.position, 200, cryPart.BodyPartTransform.tag);
            RuntimeSet.Remove(this);
        }
        public void SetStopDoingShit(bool value)
        {
            if(value)
            {
                windUp = false;
                attack = false;
                windUp = false;
                simplewind = false;
                simpleattack = false;
                attackButtonClickTimer = 0;
                attackPower = 0;
                attackTimer = 0;
                windTimer = 0;
                legs.walking = false;
            }
            else
            {
                windUp = false;
                attack = false;
                simplewind = false;
                simpleattack = false;
                attackButtonClickTimer = 0;
                attackPower = 0;
                attackTimer = 0;
                windTimer = 0;
                legs.walking = false;
            }

            if(health.alive)
            {
                bpHolder.ResetJoints(false);
            }
            else
            {
                bpHolder.ResetJoints(true);
            }
            //bpHolder.ResetJoints(true);
            this.stopDoingShit = value;
            
            //walking = false;
            // windUp = false;
            // attack = false;
        }

        
        public T Remember<T>(string key)
        {
            object result;
            if (!memory.TryGetValue(key, out result))
                return default(T);
            return (T)result;
        }
        public void Remember<T>(string key, T value)
        {
            memory[key] = value;
        }

        public void SetTargettting(bool value)
        {
            this.targetting = value;
        }
        public void SetEnableDrag(bool value)
        {
            this.enableDrag = value;
        }
        

        public void Awake()
        {
            bpHolder = this.GetComponent<CharacterBodyPartHolder>();
            input = this.GetComponent<CharacterInput>();
            
            memory = new Dictionary<string, object>();

            legs = this.GetComponent<CharacterLegsSimple1>();

            damageEffectScript = transform.root.GetComponent<DamageEffectScript>();
            health = transform.root.GetComponent<CharacterHealth>();
            player = ReInput.players.GetPlayer(playerId);
            normalSpeed = speed;
            windUpSpeed = speed / 5;

            navigator = this.GetComponent<AstarAI>();
        }

        void OnDisable()
        {
            bpHolder.ResetJoints(false);
            //if(!amIMainPlayer)
            //{
                RuntimeSet.Remove(this);
            //}
            
            // Debug.Log("PrintOnDisable: script was disabled");
        }

        void OnEnable()
        {
            windUp = false;
            attack = false;
            simplewind = false;
            simpleattack = false;
            attackButtonClickTimer = 0;
            attackPower = 0;
            attackTimer = 0;
            windTimer = 0;
            legs.walking = false;
            bpHolder.ResetJoints(false);
            RuntimeSet.Add(this);
            //Debug.Log("PrintOnEnable: script was enabled");
        }

        
        // Use this for initialization
        void Start()
        {
            agent = transform.GetComponent<NavMeshAgent>();
           // Debug.Log(bpHolder.bodyParts.Keys.Count);
            foreach (CharacterAction action in actions)
            {
                action.OnInitialize(this);
            }
            cryPart = this.bpHolder.bodyPartsName["hip"];
              
            //legs.feet[0] = this.bpHolder.bodyParts[legConfig.feet[0]].BodyPartRb;
            //legs.feet[1] = this.bpHolder.bodyParts[legConfig.feet[1]].BodyPartRb;

            //legs.legs[0] = this.bpHolder.bodyParts[legConfig.feet[0]].BodyPartRb;
            //legs.legs[1] = this.bpHolder.bodyParts[legConfig.feet[1]].BodyPartRb;

            //legs.shins[0] = this.bpHolder.bodyParts[legConfig.legs[0]].BodyPartRb;
            //legs.shins[1] = this.bpHolder.bodyParts[legConfig.legs[1]].BodyPartRb;

            //legs.thighs[0] = this.bpHolder.bodyParts[legConfig.thighs[0]].BodyPartRb;
            //legs.thighs[1] = this.bpHolder.bodyParts[legConfig.thighs[1]].BodyPartRb;

            //legs.legHeights[0] = this.bpHolder.bodyParts[legConfig.legs[0]].BodyPartMaintainHeight;
            //legs.legHeights[1] = this.bpHolder.bodyParts[legConfig.legs[1]].BodyPartMaintainHeight;

            //legs.chestBody = this.bpHolder.bodyParts[legConfig.chest].BodyPartRb;

            //legs.legRate = legConfig.legRate;
            //legs.legRateIncreaseByVelocity = legConfig.legRateIncreaseBy;
            //legs.liftForce = legConfig.liftForce;
            //legs.holdDownForce = legConfig.holdDownForce;
            //legs.moveForwardForce = legConfig.moveForwardForce;
            //legs.inFrontVelocityM = legConfig.inFrontVelocityM;
            //legs.chestBendDownForce = legConfig.chestBendDownForce;

        }

        private void LateUpdate()
        {
            //grounded = false;
        }
        // Update is called once per frame
        void Update()
        {
            GroundCheck();
            if (stopDoingShit)
                return;

           /* foreach(CharacterAction action in actions)
            {
                action.OnUpdate(this);
            }*/

            foreach (var action in actions)
            {
                if(action is PlayerCharacterAttackInput)
                {
                    if(!stopAttacking)
                    {
                        action.OnUpdate(this);
                    }
                }
                else if(action is PlayerCharacterAttackOutput)
                {
                    if (!stopAttacking)
                    {
                        action.OnUpdate(this);
                    }
                }
                else if(action is PlayerCharacterSetTargetInput)
                {
                    if (!stopLooking)
                    {
                        action.OnUpdate(this);
                    }
                }
                else
                {

                    action.OnUpdate(this);
                }


            }

            if (hurt)
            {
                if (damages.Count >= 1)
                {
                    DamageData damage = damages.OrderByDescending(i => i.impulseMagnitude).FirstOrDefault<DamageData>();
                    lastHurtObject = damage.collisionContacts[0].thisCollider.transform;
                    DamageCheck(damage);
                    damages.Clear();
                }

            }

            if (hurt && canBeHurt)
            {
                canBeHurt = false;
                hurt = false;
                nextHurtTime = Time.time + timeBetweenGettingHurt;
            }

            if(Time.time >= nextHurtTime)
            {
                canBeHurt = true;
            }

            if (health.currentHP.Value <= 0.0f)
            {
                if (health.alive == true)
                {
                    DeathCry();
                    //alive = false;
                }




                //firstDeath = true;

            }


            // targetGO.transform.position = target;
            //target = this.Remember<Vector3>("inputDirection");
            /*  attack = this.Remember<bool>("attack");
              windUp = this.Remember<bool>("windUp");
              attackButtonClickTimer = this.Remember<float>("attackButtonClickTimer");
              attackPower = this.Remember<float>("attackPower");
               currentAttack = this.Remember<AttackData>("currentAttack");

             if(attack)
             {
                 //if (youHitMe)
                 //{

                 //    successHitCounter++;
                 //    youHitMe = false;

                 //}
                 //else
                 //{

                 //    youMissed = true;

                 //}

                 attacking = true;
             }

             if(!attack || windUp)
             {
                 if (attacking)
                 {
                     if(youHitMe)
                     {
                         successHitCounter++;
                         youHitMe = false;
                     }
                     else
                     {
                         missedHitCounter++;

                     }
                     attackCounter++;
                     attacking = false;
                 }

             }*/
            /*if(youMissed)
            {
                missedHitCounter++;
                youMissed = false;
            }*/
            canDamage = false;


            
        }

        public void checkDamage(DamageData damage)
        {
            Debug.Log("checkDamage impulseMagnitude: " + damage.impulseMagnitude * 2);
            damages.Add(damage);
        }

        public void DamageCheck(DamageData damage)
        {
           // if (!canDamage)
               // return;
            //for (int i = 0; i < collisionContacts.Length; i++)
            // {
            ContactPoint contactPoint = damage.collisionContacts[0];
            float value;
            float num = 0f;
            // if (impulseMagnitude / 10 > 1)
            // {


            //  }
            //num = impulseMagnitude * collisionRigidbody.mass / rb.mass;
            num = damage.impulseMagnitude * 1f;

            Debug.Log("num impulseMagnitude: " + num);

            //rb.AddForce((contactPoint.normal + Vector3.up) * num * 2, ForceMode.Impulse);
            //rb.AddExplosionForce(num * 400 , contactPoint.normal, 10000);

            //EffectsController.Instance.slowDownTime(3, 4, 0);
            if (num > 5)
            {

                //contactPoint.thisCollider.attachedRigidbody.AddForce((contactPoint.normal + Vector3.up) * num, ForceMode.VelocityChange);
                //contactPoint.thisCollider.attachedRigidbody.AddForce((contactPoint.normal) * num, ForceMode.VelocityChange);



                //contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity), ForceMode.VelocityChange);

                if(damage.isTackle)
                {
                    if (((health.currentHP.Value - Mathf.RoundToInt(1)) <= 0))
                    {
                        if (amIMainPlayer)
                        {
                            contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity.normalized) * num, ForceMode.VelocityChange);
                            // EffectsController.Instance.CreateFloatingTextEffect(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                            bpHolder.ResetJoints(true);
                        }
                        else
                        {
                           contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity.normalized) * num, ForceMode.VelocityChange);
                            bpHolder.ResetJoints(true);
                            // EffectsController.Instance.CreateFloatingTextEffect(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                        }
                        // EffectsController.Instance.CreateFloatingTextEffectForScore(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                    }
                    else
                    {
                        if (amIMainPlayer)
                        {
                            contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity.normalized) * num, ForceMode.VelocityChange);
                            // EffectsController.Instance.CreateFloatingTextEffect(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                        }
                        else
                        {
                            contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity.normalized) * num, ForceMode.VelocityChange);
                            //EffectsController.Instance.CreateFloatingTextEffect(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                        }



                    }
                }
                else
                {
                     if (((health.currentHP.Value - Mathf.RoundToInt(1)) <= 0))
                {
                    if (amIMainPlayer)
                    {
                        contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity.normalized) * num, ForceMode.VelocityChange);
                        // EffectsController.Instance.CreateFloatingTextEffect(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                        bpHolder.ResetJoints(true);
                    }
                    else
                    {
                        contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity.normalized) * num, ForceMode.VelocityChange);
                        bpHolder.ResetJoints(true);
                        // EffectsController.Instance.CreateFloatingTextEffect(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                    }
                    // EffectsController.Instance.CreateFloatingTextEffectForScore(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                }
                else
                {
                    if (amIMainPlayer)
                    {
                        contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity.normalized) * num, ForceMode.VelocityChange);
                        // EffectsController.Instance.CreateFloatingTextEffect(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                    }
                    else
                    {
                        contactPoint.thisCollider.attachedRigidbody.AddForce((damage.relativeVelocity.normalized) * num, ForceMode.VelocityChange);
                        //EffectsController.Instance.CreateFloatingTextEffect(damage.collisionContacts[0].point, num, this.transform, (int)num, amIMainPlayer);
                    }



                }
                }

               


                /*********************************************************Use This *************************************/

                EffectsController.Instance.PlayImpactSound(damage.collisionContacts[0].point, num, damage.collisionCollider.name);


                EffectsController.Instance.CreateHitEffect(damage.collisionContacts[0].point, num, -damage.collisionContacts[0].normal);
                //EffectsController.Instance.CreateCash(damage.collisionContacts[0].point, damage.cashToDrop);



                EffectsController.Instance.PlayGruntSound(damage.collisionContacts[0].point, num, damage.collisionCollider.tag);


                // health.applyDamage(Mathf.RoundToInt(num));
                health.applyDamage(Mathf.RoundToInt(1));




                if (num > 65)
                {

                    this.bleedNow = true;
                    EffectsController.Instance.PlayBloodSpurtSound(damage.collisionContacts[0].point, num, damage.collisionCollider.tag);
                    EffectsController.Instance.PlayBreakSound(damage.collisionContacts[0].point, num, damage.collisionCollider.tag);

                    damageEffectScript.Blink(0, 0.2f);


                    if(CameraShaker.Instance != null)
                    {
                        CameraShaker.Instance.ShakeOnce(Mathf.Clamp(num / 5, 1, 5), 40, 0.05f, 0.1f);
                        //Debug.Log("Screenshake amplitude : " + num / 5);
                    }

                }
                else if ((num <= 65) && (num > 10))
                {

                    EffectsController.Instance.PlayBloodSpurtSound(damage.collisionContacts[0].point, num, damage.collisionCollider.tag);
                    EffectsController.Instance.PlayBreakSound(damage.collisionContacts[0].point, num, damage.collisionCollider.tag);

                    damageEffectScript.Blink(0, 0.4f);


                    if (CameraShaker.Instance != null)
                    {
                        CameraShaker.Instance.ShakeOnce(Mathf.Clamp(num / 5, 1, 5), 10, 0.05f, 0.4f);

                    }

                    //Debug.Log("Screenshake amplitude : " + num / 5);


                }
            }

            // }

            

        }

        public void AlignToVector(Rigidbody part, Vector3 alignmentVector, Vector3 targetVector, float stability, float speed)
        {
            if (part == null)
            {
                return;
            }
            Vector3 a = Vector3.Cross(Quaternion.AngleAxis(part.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed, part.angularVelocity) * alignmentVector, targetVector * 10f);

            // Vector3 a = Vector3.Cross(Quaternion.AngleAxis(part.angularVelocity.magnitude * 57.29578f * stability / speed, part.angularVelocity) * alignmentVector, targetVector * 10f);
            if (!float.IsNaN(a.x) && !float.IsNaN(a.y) && !float.IsNaN(a.z))
            {
                part.AddTorque(a * speed * speed);
                if (true)
                {
                    Debug.DrawRay(part.position, alignmentVector * 0.2f, Color.red, 0f, false);
                    Debug.DrawRay(part.position, targetVector * 0.2f, Color.green, 0f, false);
                }
            }
        }

        public void GroundCheck()
        {
            /*foreach (var item in bpHolder.bodyParts.Values)
            {
                if()
            }*/

            var onGroundParts = bpHolder.bodyParts.Values.Where(t => t.partOnGround == true).ToList<BodyPartMono>();

            if(onGroundParts.Count > 0)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }

        public void IsGrabbedCheck()
        {
            var grabCount = 0;
            foreach (var item in bpHolder.bodyParts.Values)
            {
                if(item.BodyPartInteractableObject)
                {
                    if(item.BodyPartInteractableObject.isGrabbed == true)
                    {
                        grabCount++;
                    }
                }
            }

            if(grabCount > 0)
            {
                isGrabbed = true;
            }
            else
            {
                isGrabbed = false;
            }

            /*var grabbedParts = bpHolder.bodyParts.Values.Where(t => t.BodyPartInteractableObject != null).Where(t => t.BodyPartInteractableObject.isGrabbed == true).ToList<BodyPartMono>();

            if (grabbedParts.Count > 0)
            {
                isGrabbed = true;
            }
            else
            {
                isGrabbed = false;
            }*/


        }

        public IEnumerator DoSimpleAttack(float attackSpeed)
        {

            

            attackButtonClickTimer = 0f;
            windUp = false;
            float elapsed = 0.0f;

            while (elapsed < attackSpeed)
            {

                attackButtonClickTimer += Time.deltaTime;
                if (windUp == false && !attack)    // if the button is pressed for more than 0.2 seconds grab
                {
                    windUp = true;
                    //character.Remember("windUp", windUp);
                    PickAnAttack(this);
                }
                elapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (!attack) // as long as key is held down increase time, this records how long the key is held down
            {
                //float attackPower = Mathf.Clamp(currentAttack.startAttackPower * (1 + attackButtonClickTimer * currentAttack.attackPowerIncreaseRate), currentAttack.minAttackPower, currentAttack.maxAttackPower);
                AttackData currentAttack = this.currentAttack;

                //float attackButtonClickTimer = character.Remember<float>("attackButtonClickTimer");
                float attackPower = Mathf.Clamp(currentAttack.startAttackPower * (1 + attackButtonClickTimer * currentAttack.attackPowerIncreaseRate), currentAttack.minAttackPower, currentAttack.maxAttackPower);
                this.attackPower = attackPower;

                attack = true;
                attackButtonClickTimer = 0f;

                //EventManager.TriggerEvent("release");
            }

            windUp = false;


        }

        private void PickAnAttack(CharacterThinker character)
        {
            AttackData currentAttack = character.attacks[UnityEngine.Random.Range(0, character.attacks.Count)];
            character.currentAttack = currentAttack;
        }

        public IEnumerator DoSimpleGrab(float grabAttemptDuration)
        {
            yield return new WaitForSeconds(0.2f);
           // if (this.leftbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
           // {
             //   this.leftbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
            //}
           // if (this.leftbuttonClickTimer > 0.2f && this.leftgrab == false)    // if the button is pressed for more than 0.2 seconds grab
           // {
                this.leftgrab = true;


            //}
            //else    // else ready the arm, pull back for punch / grab
            //{
                //call punch action readying
            //}


            //if (this.rightbuttonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
            //{
            //    this.rightbuttonClickTimer += Time.deltaTime; //this records how long the key is held down
            //}
            //if (this.rightbuttonClickTimer > 0.2f && this.rightgrab == false)    // if the button is pressed for more than 0.2 seconds grab
            //{
                this.rightgrab = true;


            //}
           // else    // else ready the arm, pull back for punch / grab
           // {
                //call punch action readying
            //}

            yield return new WaitForSeconds(grabAttemptDuration);

            if (this.isGrabbing)
            {

            }
            else
            {
                //this.leftgrab = false;
                //this.rightgrab = false;

                
                this.leftThrow = true;
                this.simpleThrwoButtonClickTimer = 0f;

                this.rightThrow = true;
                this.simpleThrwoButtonClickTimer = 0f;
            }


        }


        public IEnumerator DoSimpleSlam(float grabAttemptDuration)
        {

            while (this.slam)
            {
                this.slamming = true;
                yield return new WaitForEndOfFrame();
            }
            this.slamming = false;

            


        }

        private void FixedUpdate()
        {
            IsGrabbedCheck();
            if (stopDoingShit)
                return;


            foreach (CharacterAction action in actions)
            {
                action.OnFixedUpdate(this);
            }

            /*Rigidbody part1 = this.bpHolder.bodyPartsName["head"].BodyPartRb;
            Vector3 alignmentVector1 = -this.bpHolder.bodyPartsName["head"].BodyPartRb.transform.right;
            Vector3 targetVector1 = -this.bpHolder.bodyPartsName["hip"].BodyPartRb.transform.right;
            AlignToVector(part1, alignmentVector1, targetVector1, 0.3f, 1f);

            Rigidbody part2 = this.bpHolder.bodyPartsName["chest"].BodyPartRb;
            Vector3 alignmentVector2 = -this.bpHolder.bodyPartsName["chest"].BodyPartRb.transform.right;
            Vector3 targetVector2 = -this.bpHolder.bodyPartsName["hip"].BodyPartRb.transform.right;
            AlignToVector(part2, alignmentVector2, targetVector2, 0.3f, 1f);*/

            
        }
    }

}