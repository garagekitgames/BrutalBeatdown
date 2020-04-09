using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;

public class ProximitySensor : MonoBehaviour
{
    public CharacterThinker character;
    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.root.GetComponent<CharacterThinker>();   
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    Transform collisiontransform = other.transform;
    //    CharacterThinker enemyCharacter = collisiontransform.root.GetComponent<CharacterThinker>();
    //    if (transform.root != collisiontransform.root)
    //    {

    //        if (collisiontransform.tag == "Enemy")
    //        {
    //            if (!character.attacking && enemyCharacter.health.alive && character.teamID != enemyCharacter.teamID)
    //            {
    //                RaycastHit hit;
    //                var dir = transform.position - collisiontransform.position;

    //                if (Physics.Raycast(transform.position, -dir, out hit))
    //                {
    //                    Debug.DrawRay(transform.position, -dir * hit.distance, Color.red);

    //                    if (hit.transform.CompareTag("Enemy"))
    //                    {
    //                        character.target = collisiontransform.position;
    //                        IEnumerator coroutine = character.DoSimpleAttack(0.2f);
    //                        this.StartCoroutine(coroutine);

    //                    }

    //                }


    //            }
    //        }







    //    }
    //}
    // Update is called once per frame






    void Update()
    {
        
    }
}
