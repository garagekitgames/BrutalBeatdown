using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TutorialDesigner;

namespace garagekitgames
{
    [CreateAssetMenu(menuName = "GarageKitGames/Actitons/PlayerAttackInput")]
    public class PlayerCharacterAttackInput : CharacterAction
    {
        public bool touchInput;

        public string attackButton;
        public override void OnInitialize(CharacterThinker character)
        {
            base.OnInitialize(character);
            //PickAnAttack(character);
        }
        public override void OnFixedUpdate(CharacterThinker character)
        {
            
        }

        public override void OnUpdate(CharacterThinker character)
        {
            if(character.health.knockdown) //if( !character.health.alive || character.health.knockdown || character.isHurting)
            {
                character.windUp = false;
                character.attack = false;
                character.simplewind = false;
                character.simpleattack = false;
                character.attackButtonClickTimer = 0;
                character.attackPower = 0;
                character.attackTimer = 0;
                character.windTimer = 0;
                return;
            }
            if (!touchInput)
            {

                float attackButtonClickTimer = character.attackButtonClickTimer;
                bool windUp = character.windUp;
                bool attack = character.attack;
                
                if (character.player.GetButtonDown(attackButton)) //(character.input.PressLeftPunch())
                {
                    attackButtonClickTimer = 0f;
                    windUp = false;

                    //if (!attack)
                        

                }

                else if (character.player.GetButton(attackButton))
                {
                    attackButtonClickTimer += Time.deltaTime;
                    if (windUp == false && !attack)    // if the button is pressed for more than 0.2 seconds grab
                    {
                        windUp = true;
                        //character.Remember("windUp", windUp);
                        PickAnAttack(character);
                    }
                    /* Debug.Log("windUp : " + windUp);
                     Debug.Log("attackButtonClickTimer : " + attackButtonClickTimer);*/
                    //EventManager.TriggerEvent("hold");
                }

                else if (character.player.GetButtonUp(attackButton))
                {
                    //print("Key Released");
                    if (!attack) // as long as key is held down increase time, this records how long the key is held down
                    {
                        //float attackPower = Mathf.Clamp(currentAttack.startAttackPower * (1 + attackButtonClickTimer * currentAttack.attackPowerIncreaseRate), currentAttack.minAttackPower, currentAttack.maxAttackPower);
                        AttackData currentAttack = character.currentAttack;

                        //float attackButtonClickTimer = character.Remember<float>("attackButtonClickTimer");
                        float attackPower = Mathf.Clamp(currentAttack.startAttackPower * (1 + attackButtonClickTimer * currentAttack.attackPowerIncreaseRate), currentAttack.minAttackPower, currentAttack.maxAttackPower);
                        character.attackPower = attackPower;

                        attack = true;
                        attackButtonClickTimer = 0f;

                        //EventManager.TriggerEvent("release");
                    }

                    windUp = false;


                }

                character.attackButtonClickTimer = attackButtonClickTimer;
                character.windUp = windUp;
                character.attack = attack;
                
            }
            else if(touchInput)
            {
                float attackButtonClickTimer = character.attackButtonClickTimer;
                bool windUp = character.windUp;
                bool attack = character.attack;
                Touch[] myTouches = Input.touches;

                
                if (Input.touchCount == 1)
                {
                    


                    if (myTouches[0].phase == TouchPhase.Began)
                    {
                        attackButtonClickTimer = 0f;
                        windUp = false;

                        //if (!attack)
                           
                    }
                    else if (myTouches[0].phase == TouchPhase.Stationary || myTouches[0].phase == TouchPhase.Moved)
                    {
                        PickAnAttack(character);
                        attackButtonClickTimer += Time.deltaTime;
                        if (windUp == false && !attack)    // if the button is pressed for more than 0.2 seconds grab
                        {
                            windUp = true;
                            

                        }
                    }
                    else if (myTouches[0].phase == TouchPhase.Ended || myTouches[0].phase == TouchPhase.Canceled)
                    {
                        //print("Key Released");
                        if (!attack) // as long as key is held down increase time, this records how long the key is held down
                        {

                            AttackData currentAttack = character.currentAttack;

                            //float attackButtonClickTimer = character.Remember<float>("attackButtonClickTimer");
                            float attackPower = Mathf.Clamp(currentAttack.startAttackPower * (1 + attackButtonClickTimer * currentAttack.attackPowerIncreaseRate), currentAttack.minAttackPower, currentAttack.maxAttackPower);
                            character.attackPower = attackPower;

                            attack = true;
                            attackButtonClickTimer = 0f;
                        }

                        windUp = false;

                    }
                }

                character.attackButtonClickTimer = attackButtonClickTimer;
                character.windUp = windUp;
                character.attack = attack;
            }
            
        }

        private void PickAnAttack(CharacterThinker character)
        {
            bool side = !character.prevSide;
            AttackData currentAttack = new AttackData();
            if (attackButton.Contains("SimplePrimaryAttack"))
            {
                //Debug.Log("Primary Attack");
                if(side)
                {
                    currentAttack = character.primaryAttacks[0];
                }
                else
                {
                    currentAttack = character.primaryAttacks[1];
                }
                //currentAttack = character.primaryAttacks[UnityEngine.Random.Range(0, character.primaryAttacks.Count)];
            }
            else
            {
                if (side)
                {
                    currentAttack = character.secondaryAttacks[0];
                }
                else
                {
                    currentAttack = character.secondaryAttacks[1];
                }
                //currentAttack = character.secondaryAttacks[UnityEngine.Random.Range(0, character.secondaryAttacks.Count)];
            }
            //AttackData currentAttack = character.attacks[UnityEngine.Random.Range(0, character.attacks.Count)];
            character.currentAttack = currentAttack;

            character.prevSide = side;
        }
    }
}