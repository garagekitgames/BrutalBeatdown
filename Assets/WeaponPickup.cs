using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using SO;
using System;
using System.Linq;
using DG.Tweening;

public class WeaponPickup : MonoBehaviour
{
    public List<Collider> visibleWeapons = new List<Collider>();
    public float viewRadius = 1f;
    public LayerMask weaponLayer;
    public CharacterThinker character;

    public BodyPartMono hipPart;

    public bool weaponPickUp = false;

    public string weaponButton;

    public BodyPartMono weaponPart;

    public BodyPartMono weaponHolderPart;

    public WeaponScript myWeapon;

    public float weaponButtonClickTimer;

    public bool weaponInHand = false;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        character = GetComponent<CharacterThinker>();
        if(character)
        {
            hipPart = character.bpHolder.BodyPartsName[BodyPartNames.hipName];
            weaponHolderPart = character.bpHolder.BodyPartsName[BodyPartNames.rhandName];
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindVisibleWeapons();

        if(myWeapon == null)
        {
            HandlePickupInput();
            HandlePickupOutput();
        }
        else
        {
            HandleWeaponUseInput();
            HandleWeaponUseOutput();

            HandleWeaponThrowInput();
            HandleWeaponThrowOutput();
        }
        

    }

    public void FindVisibleWeapons()
    {
        visibleWeapons.Clear();

        Collider[] weaponsInRadius = Physics.OverlapSphere(hipPart.bodyPartTransform.position, viewRadius, weaponLayer);


        Array.Sort<Collider>(weaponsInRadius, (x, y) => Vector3.Distance(hipPart.bodyPartTransform.position, x.transform.position)
                                                        .CompareTo(Vector3.Distance(hipPart.bodyPartTransform.position, y.transform.position)));

        visibleWeapons = weaponsInRadius.ToList<Collider>();
            
    }

    public void HandlePickupInput()
    {
        if(visibleWeapons.Count >= 1)
        {
            if (character.player.GetButtonDown(weaponButton))
            {
                weaponPickUp = true;
            }
        }
    }

    public void HandlePickupOutput()
    {
        if(weaponPickUp && !weaponInHand)
        {
            weaponPickUp = false;
            if (visibleWeapons.Count >= 1 && myWeapon == null)
            {
                var firstWeapon = visibleWeapons.First().GetComponent<WeaponScript>();
                if (!(firstWeapon.weaponState == WeaponScript.WeaponStates.InHand) && (firstWeapon.ownerCharacter != this.character))
                {
                    myWeapon = firstWeapon;
                   // ball = ballIncoming;
                    if (myWeapon)
                    {
                        myWeapon.weaponState = WeaponScript.WeaponStates.InHand;
                        myWeapon.ownerCharacter = this.character;
                    }
                    
                    weaponInHand = true;

                    
                    //call myWeapon.WeaponScript.Pickup();
                    if (myWeapon && myWeapon.character == null)
                    {
                        myWeapon.Pickup(weaponHolderPart, character);
                        

                    }
                }

                

                
            }
                
        }
    }

    public void HandleWeaponUseInput()
    {
        if (character.player.GetButtonDown(weaponButton))
        {

            weaponButtonClickTimer = 0f;
            //character.leftgrab = false;

        }

        else if (character.player.GetButton(weaponButton))
        {
            //print("Key Held");
            if (weaponButtonClickTimer < 1f) // as long as key is held down increase time, this records how long the key is held down
            {
                weaponButtonClickTimer += Time.deltaTime; //this records how long the key is held down
            }
            //if (character.simpleThrwoButtonClickTimer > 0.2f && character.leftgrab == false)    // if the button is pressed for more than 0.2 seconds grab
            //{
            //    character.leftgrab = true;


            //}
            //else    // else ready the arm, pull back for punch / grab
            //{
            //    //call punch action readying
            //}

            
        }

        else if (character.player.GetButtonUp(weaponButton))
        {
            //print("Key Released");
            if (weaponButtonClickTimer <= 0.3f) // as long as key is held down increase time, this records how long the key is held down
            {

                //if (!character.attacking)
                //{

                //    //character.targetting = true;

                //    IEnumerator coroutine = character.DoSimpleAttack(0.2f);
                //    StartCoroutine(coroutine);
                //    //character.attack = true;

                //}
                if (myWeapon)
                {
                    myWeapon.Attack();
                }

            }
            else
            {
                //throw weapon
                weaponButtonClickTimer = 0f;
                if(myWeapon)
                {
                    if(weaponInHand)
                    {
                        myWeapon.thrownBy = character.teamID;
                        myWeapon.ownerCharacter = null;
                        myWeapon.Throw();

                        myWeapon.weaponState = WeaponScript.WeaponStates.Thrown;
                        weaponInHand = false;
                        myWeapon = null;
                        //IEnumerator coroutine = SimpleWait(0.3f);
                        //character.StartCoroutine(coroutine);
                        //var target = myTarget;
                    }
                }

                //----old code------//
                //myWeapon.Throw();
                //IEnumerator coroutine = SimpleWait(1);
                //character.StartCoroutine(coroutine);
                //-----old code-------//
            }
        }
    }

    public IEnumerator SimpleWait(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        myWeapon = null;
    }

    public void HandleWeaponUseOutput()
    {

    }

    public void HandleWeaponThrowInput()
    {

    }

    public void HandleWeaponThrowOutput()
    {

    }
}
