using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;

public class CharacterStealthPlayer : MonoBehaviour
{
    public CharacterThinker character;
    public LayerMask raycastLayermask;
    public Transform hipPart;
    public Transform target;
    public bool stopFollow;

    public float attackDistance = 1.5f;
    public float attackSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        character = this.GetComponent<CharacterThinker>();
        hipPart = character.bpHolder.BodyPartsName["hip"].BodyPartTransform;
    }

    // Update is called once per frame
    void Update()
    {
        Input();
        if (target != null)
        {
            CharacterThinker enemyCharacter = target.root.GetComponent<CharacterThinker>();
            if (Vector3.Distance(hipPart.position, target.position) < attackDistance)
            {
                if (!character.attacking && enemyCharacter.health.alive && character.teamID != enemyCharacter.teamID)
                {
                    character.target = target.position;
                    IEnumerator coroutine = character.DoSimpleAttack(attackSpeed);
                    this.StartCoroutine(coroutine);
                    //if (!attack)
                }
            }
        }

        character.target = character.navigator.targetPosition.position;

    }

    

    public void Input()
    {
        if (!character.health.alive || character.health.knockdown || character.isHurting)// if (character.health.knockdown) //if( !character.health.alive || character.health.knockdown || character.isHurting)
        {
            return;
        }

        if (character.health.knockdown) //if( !character.health.alive || character.health.knockdown || character.isHurting)
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

        //if (character.player.GetButtonDown("PrimaryAttackLeft")) //(character.input.PressLeftPunch())
        //{
        //    if (!character.attacking )
        //    {
        //        IEnumerator coroutine = character.DoSimpleAttack(0.2f);
        //        character.StartCoroutine(coroutine);
        //        //if (!attack)
        //    }
        //}



        if (UnityEngine.Input.GetMouseButton(0))
        {
            Ray mouseRay = GenerateMouseRay();
            RaycastHit hit;

            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100, raycastLayermask))
            {
                // GameObject temp = Instantiate(target, hit.point, Quaternion.identity);
                Debug.DrawRay(mouseRay.origin, mouseRay.direction * hit.distance, Color.yellow);

                
                
                if (!hit.transform.CompareTag("Enemy"))
                {
                    character.navigator.targetPosition.SetParent(null);
                    character.navigator.targetPosition.position = hit.point;
                    stopFollow = true;
                    target = null;
                    character.navigator.canMove = true;

                }
                else //if (hit.transform.CompareTag("Enemy"))
                {
                    //IEnumerator coroutine = this.DoSimpleFollow(target);
                    //this.StartCoroutine(coroutine);
                    //Debug.Log(" GoTo(Transform target)");
                    //character.navigator.GoTo(hit.transform);

                    character.navigator.targetPosition.SetParent(hit.transform);
                   // Debug.Log(" I hit : "+ hit.transform.name);
                    character.navigator.targetPosition.localPosition = Vector3.zero;
                    target = hit.transform;
                    stopFollow = false;
                    character.navigator.canMove = true;

                }

                //if (!hit.transform.CompareTag("Enemy"))
                //{
                //    character.navigator.GoTo(hit.point);
                //    stopFollow = true;
                //    target = null;

                //}
                //else if (hit.transform.CompareTag("Enemy"))
                //{
                //    IEnumerator coroutine = this.DoSimpleFollow(target);
                //    this.StartCoroutine(coroutine);
                //    Debug.Log(" GoTo(Transform target)");
                //    //character.navigator.GoTo(hit.transform);
                //    target = hit.transform;
                //    stopFollow = false;

                //}



            }

        }


    }

    public IEnumerator DoSimpleFollow(Transform target)
    {

        while ((Vector3.Distance(hipPart.position, target.position) > 1.5f) && !stopFollow)
        {
            character.navigator.GoTo(target.position);
            character.target = target.position;
            yield return new WaitForEndOfFrame();
        }
        //CharacterThinker enemyCharacter = target.root.GetComponent<CharacterThinker>();
        //if (!character.attacking && !character.walking && enemyCharacter.health.alive)
        //{
        //    character.target = target.position;
        //    IEnumerator coroutine = character.DoSimpleAttack(0.5f);
        //    this.StartCoroutine(coroutine);

        //}
        //this.slamming = false;




    }

    Ray GenerateMouseRay()
    {
        Vector3 mousePosFar = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 mousePosFarW = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosNearW = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mouseRay = new Ray(mousePosNearW, mousePosFarW - mousePosNearW);

        return mouseRay;



    }

}
