﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.Events;
using DamageEffect;

namespace garagekitgames
{
    [System.Serializable]
    public class MyIntEvent : UnityEvent<int>
    {
    }


    public class CollisionCheck : MonoBehaviour,IDamageable
    {
        public Color damageIndicationColor;
        private Rigidbody rb;

        private bool once = false;

        public CharacterThinker character;

        public CharacterHealth health;

        //public MyIntEvent m_MyEvent;

        public int myTeamID;

        private int cashToDrop;
        private DamageEffectScript damageEffectScript;

        public UnityEvent onCaught;

        public bool stealthCollision;

        public EnemyAwareness enemyAwareness;

        // Use this for initialization
        void Start()
        {
           // if (m_MyEvent == null)
            //    m_MyEvent = new MyIntEvent();

            //FloatingTextController.Initialize();
            rb = transform.GetComponent<Rigidbody>();
            character = transform.root.GetComponent<CharacterThinker>();
            health = transform.root.GetComponent<CharacterHealth>();
            damageEffectScript = transform.root.GetComponent<DamageEffectScript>();
            if (character)
            {
                myTeamID = character.teamID;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //if((collision.gameObject.layer == 8))
            //{
            //    EffectsController.Instance.CreateSmokeEffect(collision.contacts[0].point, collision.impulse.magnitude);
            //}

            if (character)
            {
                myTeamID = character.teamID;
            }
            if (!health)
                return;

            if (!health.alive)
                return;

            if (!collision.rigidbody)
                return;
            Transform collisiontransform = collision.transform;
            if (transform.root != collisiontransform.root && !once)
            {
                CharacterThinker cT = collisiontransform.root.GetComponent<CharacterThinker>();
                CharacterHealth cH = collisiontransform.root.GetComponent<CharacterHealth>();

                //if(cT)
                
                
                if (cT)
                {
                    if (character.amIMainPlayer && myTeamID != cT.teamID  && stealthCollision)
                    {
                       // Debug.Log("Outside Collision Object : " + collision.rigidbody.transform.name);
                        if (character.canBeHurt && cT.GetComponent<Jump>().inAir && ((cT.bpHolder.BodyPartsName[BodyPartNames.chestName].bodyPartRb == collision.rigidbody) || (cT.bpHolder.BodyPartsName[BodyPartNames.headName].bodyPartRb == collision.rigidbody) ))
                       {
                            cT.youHitMe = true;
                            character.hurt = true;
                            //Debug.Log("Collision Object : " + collision.rigidbody.transform.name);
                            Vector3 relativeVelocity = collision.relativeVelocity;
                            float magnitude = relativeVelocity.magnitude;
                            Rigidbody rigidbody = collision.rigidbody;
                            Collider collider = collision.collider;
                            ContactPoint[] contacts = collision.contacts;

                            //print("Impulse MAgnitude : " + collision.impulse.magnitude);
                            //float impulseMagnitude = collision.impulse.magnitude;
                            float impulseMagnitude = 20;

                            /* print("**********************************************************************************");
                            // print("Punch Damage : " + num);
                             print("Punch Damage Magnitude : " + magnitude);
                             print("Punch Damage Velocity : " + relativeVelocity);
                             print("Punch Damage impulseMagnitude by 10 : " + impulseMagnitude);
                             */
                            
                            //impulseMagnitude = Mathf.Clamp(impulseMagnitude, 1, 250);
                            DamageData damage = new DamageData(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop, true);
                           // Debug.Log(" impulseMagnitude: " + impulseMagnitude);
                            character.checkDamage(damage);

                            if (rb.gameObject.name.Contains("CoverObject"))
                            {
                                var qDrag = this.rb.GetComponent<QuadraticDrag>();
                                qDrag.drag = 0.2f;

                                var joint = this.rb.GetComponent<ConfigurableJoint>();

                                joint.xMotion = ConfigurableJointMotion.Free;
                                joint.yMotion = ConfigurableJointMotion.Free;
                                joint.zMotion = ConfigurableJointMotion.Free;

                                joint.angularXMotion = ConfigurableJointMotion.Free;
                                joint.angularYMotion = ConfigurableJointMotion.Free;
                                joint.angularZMotion = ConfigurableJointMotion.Free;


                                joint.connectedBody = null;


                                Destroy(joint);

                                rb.AddForce((collision.GetContact(0).normal + Vector3.up) * 1, ForceMode.Impulse);

                                //joint.connectedBody = null;
                                //Destroy(coverObject.GetComponent<ConfigurableJoint>());
                                rb.GetComponent<CollisionCheck>().enabled = false;
                                


                            }
                            // EffectsController.Instance.slowDownTime(2, 4, 0);
                            // health.applyDamage(Mathf.RoundToInt(1));

                            character.isCaught = true;
                           // onCaught.Invoke();
                            once = true;
                        }
                    }

                    if(cT.amIMainPlayer && cT.walking && enemyAwareness != null)
                    {
                        enemyAwareness.Alert(cT.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position);
                    }
                     

                    if (cH)
                    {

                        if ((cH.alive))
                        {
                            if (myTeamID != cT.teamID)
                            {
                                
                                if (character.canBeHurt)
                                {
                                    if (cT.amIMainPlayer && cT.attack )//&& (character.attack || character.windUp))
                                    {
                                        cT.youHitMe = true;
                                        character.hurt = true;
                                        //InteractableObject component = collision.transform.GetComponent<InteractableObject>();
                                        Vector3 relativeVelocity = collision.relativeVelocity;
                                        float magnitude = relativeVelocity.magnitude;
                                        Rigidbody rigidbody = collision.rigidbody;
                                        Collider collider = collision.collider;
                                        ContactPoint[] contacts = collision.contacts;
                                        //print("Impulse MAgnitude : " + collision.impulse.magnitude);
                                        //float impulseMagnitude = collision.impulse.magnitude;
                                        float impulseMagnitude = rigidbody.mass * magnitude;

                                        /* print("**********************************************************************************");
                                        // print("Punch Damage : " + num);
                                         print("Punch Damage Magnitude : " + magnitude);
                                         print("Punch Damage Velocity : " + relativeVelocity);
                                         print("Punch Damage impulseMagnitude by 10 : " + impulseMagnitude);
                                         */

                                        impulseMagnitude = Mathf.Clamp(impulseMagnitude, 1, 250);
                                        if (!character.amIMainPlayer)
                                        {
                                            // m_MyEvent.Invoke(Mathf.RoundToInt(impulseMagnitude * 2) / 10);
                                            //cashToDrop = Mathf.Clamp((Mathf.RoundToInt(impulseMagnitude * 2) / 20), 0, 3);
                                            //CoinController.Instance.AddCoins(Mathf.RoundToInt(impulseMagnitude * 2));
                                            /*if ((impulseMagnitude * 2 >= 50) && (impulseMagnitude * 2 < 60))
                                            {
                                                m_MyEvent.Invoke(1);
                                                cashToDrop = 1;
                                            }
                                            else if ((impulseMagnitude * 2 >= 60) && (impulseMagnitude * 2 < 70))
                                            {
                                                m_MyEvent.Invoke(2);
                                                cashToDrop = 2;
                                            }
                                            else if ((impulseMagnitude * 2 >= 70) && (impulseMagnitude * 2 < 80))
                                            {
                                                m_MyEvent.Invoke(4);
                                                cashToDrop = 4;
                                            }
                                            else if ((impulseMagnitude * 2 >= 80) && (impulseMagnitude * 2 < 90))
                                            {
                                                m_MyEvent.Invoke(8);
                                                cashToDrop = 8;
                                            }
                                            else if ((impulseMagnitude * 2 >= 90) && (impulseMagnitude * 2 < 100))
                                            {
                                                m_MyEvent.Invoke(10);
                                                cashToDrop = 10;
                                            }
                                            else if ((impulseMagnitude * 2 >= 100))
                                            {
                                                m_MyEvent.Invoke(20);
                                                cashToDrop = 20;
                                            }*/
                                            if ((impulseMagnitude * 2 >= 100))
                                            {
                                                //m_MyEvent.Invoke(40);
                                                cashToDrop = 1;
                                            }
                                            else
                                            {
                                                cashToDrop = 0;
                                            }

                                            if (impulseMagnitude * 2 > 150)
                                            {
                                                //EffectsController.Instance.slowDownTime(4, 0.2f, 1);

                                                
                                                cT.breakFace = true;


                                            }
                                            else if ((impulseMagnitude * 2 <= 150) && (impulseMagnitude * 2 > 10))
                                            {
                                               // EffectsController.Instance.slowDownTime(4, 0.02f, 0);

                                                
                                            }
                                        }


                                        



                                        DamageData damage = new DamageData(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop, false);

                                        character.checkDamage(damage);

                                        // DamageCheck(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop);

                                        once = true;
                                    }
                                    if (!cT.amIMainPlayer && cT.attack)
                                    {
                                        cT.youHitMe = true;
                                        character.hurt = true;
                                        //InteractableObject component = collision.transform.GetComponent<InteractableObject>();
                                        Vector3 relativeVelocity = collision.relativeVelocity;
                                        float magnitude = relativeVelocity.magnitude;
                                        Rigidbody rigidbody = collision.rigidbody;
                                        Collider collider = collision.collider;
                                        ContactPoint[] contacts = collision.contacts;
                                        //print("Impulse MAgnitude : " + collision.impulse.magnitude);
                                        //float impulseMagnitude = collision.impulse.magnitude;
                                        float impulseMagnitude = rigidbody.mass * magnitude;

                                        /* print("**********************************************************************************");
                                        // print("Punch Damage : " + num);
                                         print("Punch Damage Magnitude : " + magnitude);
                                         print("Punch Damage Velocity : " + relativeVelocity);
                                         print("Punch Damage impulseMagnitude by 10 : " + impulseMagnitude);
                                         */

                                        impulseMagnitude = Mathf.Clamp(impulseMagnitude, 1, 250);
                                        if (!character.amIMainPlayer)
                                        {
                                            // m_MyEvent.Invoke(Mathf.RoundToInt(impulseMagnitude * 2) / 10);
                                            //cashToDrop = Mathf.Clamp((Mathf.RoundToInt(impulseMagnitude * 2) / 20), 0, 3);
                                            //CoinController.Instance.AddCoins(Mathf.RoundToInt(impulseMagnitude * 2));
                                            /*if ((impulseMagnitude * 2 >= 50) && (impulseMagnitude * 2 < 60))
                                            {
                                                m_MyEvent.Invoke(1);
                                                cashToDrop = 1;
                                            }
                                            else if ((impulseMagnitude * 2 >= 60) && (impulseMagnitude * 2 < 70))
                                            {
                                                m_MyEvent.Invoke(2);
                                                cashToDrop = 2;
                                            }
                                            else if ((impulseMagnitude * 2 >= 70) && (impulseMagnitude * 2 < 80))
                                            {
                                                m_MyEvent.Invoke(4);
                                                cashToDrop = 4;
                                            }
                                            else if ((impulseMagnitude * 2 >= 80) && (impulseMagnitude * 2 < 90))
                                            {
                                                m_MyEvent.Invoke(8);
                                                cashToDrop = 8;
                                            }
                                            else if ((impulseMagnitude * 2 >= 90) && (impulseMagnitude * 2 < 100))
                                            {
                                                m_MyEvent.Invoke(10);
                                                cashToDrop = 10;
                                            }
                                            else if ((impulseMagnitude * 2 >= 100))
                                            {
                                                m_MyEvent.Invoke(20);
                                                cashToDrop = 20;
                                            }*/
                                            if ((impulseMagnitude * 2 >= 100))
                                            {
                                                //m_MyEvent.Invoke(40);
                                                cashToDrop = 1;
                                            }
                                            else
                                            {
                                                cashToDrop = 0;
                                            }

                                            if (impulseMagnitude * 2 > 150)
                                            {
                                               // EffectsController.Instance.slowDownTime(4, 0.2f, 1);

                                                
                                                cT.breakFace = true;


                                            }
                                            else if ((impulseMagnitude * 2 <= 150) && (impulseMagnitude * 2 > 10))
                                            {
                                              //  EffectsController.Instance.slowDownTime(4, 0.02f, 0);

                                                
                                            }
                                        }


                                        //cT.youHitMe = true;

                                       // Debug.Log(" impulseMagnitude: " + impulseMagnitude);

                                        DamageData damage = new DamageData(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop, false);

                                        character.checkDamage(damage);

                                        if (rb.gameObject.name.Contains("CoverObject"))
                                        {


                                            var qDrag = this.rb.GetComponent<QuadraticDrag>();
                                            qDrag.drag = 0.2f;
                                            var joint = this.rb.GetComponent<ConfigurableJoint>();

                                         

                                            joint.xMotion = ConfigurableJointMotion.Free;
                                            joint.yMotion = ConfigurableJointMotion.Free;
                                            joint.zMotion = ConfigurableJointMotion.Free;

                                            joint.angularXMotion = ConfigurableJointMotion.Free;
                                            joint.angularYMotion = ConfigurableJointMotion.Free;
                                            joint.angularZMotion = ConfigurableJointMotion.Free;

                                            joint.connectedBody = null;


                                            Destroy(joint);
                                            rb.AddForce((collision.GetContact(0).normal + Vector3.up) * 1, ForceMode.Impulse);
                                            //Destroy(coverObject.GetComponent<ConfigurableJoint>());
                                            rb.GetComponent<CollisionCheck>().enabled = false;

                                            
                                        }
                                         //EffectsController.Instance.slowDownTime(2, 4, 0);
                                        //health.applyDamage(Mathf.RoundToInt(1));
                                        character.isCaught = true;
                                      //  onCaught.Invoke();

                                        // DamageCheck(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop);

                                        once = true;
                                    }
                                }
                            


                            }
                        }
                        else
                        {
                            if (myTeamID != cT.teamID)
                            {
                                return;
                            }
                            else
                            {
                                if (character.canBeHurt)
                                {
                                    cT.youHitMe = true;
                                    character.hurt = true;
                                    //InteractableObject component = collision.transform.GetComponent<InteractableObject>();
                                    Vector3 relativeVelocity = collision.relativeVelocity;
                                    float magnitude = relativeVelocity.magnitude;
                                    Rigidbody rigidbody = collision.rigidbody;
                                    Collider collider = collision.collider;
                                    ContactPoint[] contacts = collision.contacts;
                                    //print("Impulse MAgnitude : " + collision.impulse.magnitude);
                                    //float impulseMagnitude = collision.impulse.magnitude;
                                    float impulseMagnitude = rigidbody.mass * magnitude;
                                    impulseMagnitude = Mathf.Clamp(impulseMagnitude, 1, 250);
                                    /* print("**********************************************************************************");
                                    // print("Punch Damage : " + num);
                                     print("Punch Damage Magnitude : " + magnitude);
                                     print("Punch Damage Velocity : " + relativeVelocity);
                                     print("Punch Damage impulseMagnitude by 10 : " + impulseMagnitude);
                                     */

                                    if (!character.amIMainPlayer)
                                    {
                                        //CoinController.Instance.AddCoins(Mathf.RoundToInt(impulseMagnitude * 2));
                                      //  m_MyEvent.Invoke(Mathf.RoundToInt(impulseMagnitude * 2)/10);
                                        //cashToDrop = Mathf.Clamp((Mathf.RoundToInt(impulseMagnitude * 2) / 200), 0, 3);
                                      /*  if ((impulseMagnitude * 2 >= 50) && (impulseMagnitude * 2 < 60))
                                        {
                                            m_MyEvent.Invoke(2);
                                            cashToDrop = 2;
                                        }
                                        else if ((impulseMagnitude * 2 >= 60) && (impulseMagnitude * 2 < 70))
                                        {
                                            m_MyEvent.Invoke(4);
                                            cashToDrop = 4;
                                        }
                                        else if ((impulseMagnitude * 2 >= 70) && (impulseMagnitude * 2 < 80))
                                        {
                                            m_MyEvent.Invoke(8);
                                            cashToDrop = 8;
                                        }
                                        else if ((impulseMagnitude * 2 >= 80) && (impulseMagnitude * 2 < 90))
                                        {
                                            m_MyEvent.Invoke(10);
                                            cashToDrop = 10;
                                        }
                                        else if ((impulseMagnitude * 2 >= 90) && (impulseMagnitude * 2 < 100))
                                        {
                                            m_MyEvent.Invoke(20);
                                            cashToDrop = 20;
                                        }
                                        */if ((impulseMagnitude * 2 >= 65))
                                        {
                                            //m_MyEvent.Invoke(40);
                                            DamageData damage = new DamageData(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop, false);
                                            character.checkDamage(damage);
                                            cashToDrop = 1;
                                        }
                                        else
                                        {
                                            cashToDrop = 0;
                                        }
                                    }



                                   
                                    //damageEffectScript.Blink(0, 0.1f);
                                    // DamageCheck(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop);
                                    once = true;
                                }

                            }

                        }
                            
                    }
                    

                    

                }
                
                
            }

        }

        private void OnCollisionExit(Collision collision)
        {
            Transform collisiontransform = collision.transform;
            if (transform.root != collisiontransform.root && once)
            {
                once = false;

            }
        }

        private void DamageCheck(Transform collisionTransform, Rigidbody collisionRigidbody, Collider collisionCollider, ContactPoint[] collisionContacts, Vector3 relativeVelocity, float velocityMagnitude, float impulseMagnitude, int cashToDrop)
        {
            //for (int i = 0; i < collisionContacts.Length; i++)
            // {
            ContactPoint contactPoint = collisionContacts[0];
            float value;
            float num = 0f;
            // if (impulseMagnitude / 10 > 1)
            // {


            //  }
            //num = impulseMagnitude * collisionRigidbody.mass / rb.mass;
            num = impulseMagnitude * 2f;
            
            //rb.AddForce((contactPoint.normal + Vector3.up) * num * 2, ForceMode.Impulse);
            //rb.AddExplosionForce(num * 400 , contactPoint.normal, 10000);


            if (num > 10)
            {

                contactPoint.thisCollider.attachedRigidbody.AddForce((contactPoint.normal + Vector3.up) * num  , ForceMode.VelocityChange);
                //contactPoint.thisCollider.attachedRigidbody.AddExplosionForce(num * 2, contactPoint.point, 10, 2 , ForceMode.Impulse);
                //contactPoint.thisCollider.attachedRigidbody.AddTorque(Vector3.forward * num * 100000, ForceMode.Impulse);
                //contactPoint.thisCollider.attachedRigidbody.AddForce((contactPoint.normal + Vector3.up) * num * 2f, ForceMode.VelocityChange);

                /* print("**********************************************************************************");
                 print("Punch Damage : " + num);
                 print("Punch Damage Magnitude : " + velocityMagnitude);
                 print("Punch Damage Velocity : " + relativeVelocity);
                 print("Punch Damage impulseMagnitude : " + impulseMagnitude);
                 print("Punch Damage ForceApplied : " + (contactPoint.normal + Vector3.up) * num * 2f);*/

                //if (collisionRigidbody.transform.root.GetComponent<ForceTests>().ControlledBy == ForceTests.ControlledTypes.Human)
                //{


                //FloatingTextController.CreateFloatingText("-" + Mathf.RoundToInt(num).ToString(), contactPoint.point, damageIndicationColor);

                //EffectsController.Instance.Shake(num, 0.18f);

               
                
                EffectsController.Instance.PlayImpactSound(collisionContacts[0].point, num, collisionCollider.tag);
                

                EffectsController.Instance.CreateHitEffect(collisionContacts[0].point, num, collisionContacts[0].normal);
                EffectsController.Instance.CreateCash(collisionContacts[0].point, cashToDrop);


               
                EffectsController.Instance.PlayGruntSound(collisionContacts[0].point, num, collisionCollider.tag);

                
                health.applyDamage(Mathf.RoundToInt(num));
                //contactPoint.thisCollider.attachedRigidbody.AddForce((contactPoint.normal + Vector3.up ) *  num * 2, ForceMode.VelocityChange);
                //EffectsController.Instance.CreateFloatingTextEffect(collisionContacts[0].point, num, this.transform);

                if (num > 65)
                {
                   // EffectsController.Instance.ShakeCam(0.08f, num / 1100);
                    //EffectsController.Instance.ShakeCam(0.08f, num / 800);
                    //CameraShaker.Instance.ShakeOnce(num , num, 0.05f, 1f);
                    //EffectsController.Instance.Shake(num * 2, 0.18f);
                    character.bleedNow = true;
                    EffectsController.Instance.PlayBloodSpurtSound(collisionContacts[0].point, num, collisionCollider.tag);
                    EffectsController.Instance.PlayBreakSound(collisionContacts[0].point, num, collisionCollider.tag);
                    damageEffectScript.Blink(0, 0.0125f);
                    //damageEffectScript.Blink(0, 0.1f);


                    // EffectControllerScriptableSingleton.Instance.PlayBloodSpurtSound(collisionContacts[0].point, num, collisionCollider.tag);
                    // EffectControllerScriptableSingleton.Instance.PlayBreakSound(collisionContacts[0].point, num, collisionCollider.tag);
                    CameraShaker.Instance.ShakeOnce(num / 5, num , 0.05f, 0.15f);
                    //EffectsController.Instance.ShakeCam(0.08f, num / 800);
                }
                else if((num <= 65) && (num > 10))
                {
                    //character.bleedNow = true;
                    //EffectsController.Instance.ShakeCam(0.2f, num / 1000);
                    //EffectsController.Instance.ShakeCam(0.2f, num / 500);
                    //CameraShaker.Instance.ShakeOnce(num / 5 , num / 5, 0.05f, 1f);
                    //EffectsController.Instance.Shake(num * 2, 0.18f);
                    EffectsController.Instance.PlayBloodSpurtSound(collisionContacts[0].point, num, collisionCollider.tag);
                    EffectsController.Instance.PlayBreakSound(collisionContacts[0].point, num, collisionCollider.tag);
                    damageEffectScript.Blink(0, 0.05f);
                    //damageEffectScript.Blink(0, 0.2f);

                    CameraShaker.Instance.ShakeOnce(num , num , 0.05f, 0.8f);
                    
                    //EffectsController.Instance.ShakeCam(0.2f, num / 500);
                    //EffectsController.Instance.Sleep(0.05f);
                }
            }

            // }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection, int teamID)
        {
            if(teamID != myTeamID)
            {
                TakeDamage(damage);
            }
            
        }

        public void TakeDamage(float damage)
        {
            //DamageData damage1 = new DamageData(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop);
            character.health.applyDamage(damage);
        }
    }
}