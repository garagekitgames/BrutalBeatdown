using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using garagekitgames;
using UnityEngine.Events;
public class ParkPeopleCollisionHandler : MonoBehaviour
{

    public LayerMask obstacleLayer;
    public LayerMask characterLayer;
    public UnityEvent OnObstacleCollision;

    public CharacterThinker character;
    public Rigidbody hipPart;
    public bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        character = transform.root.GetComponent<CharacterThinker>();
        hipPart = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartRb;
       
    }


    private void OnTriggerEnter(Collider other)
    {



        Transform collisiontransform = other.transform;
        if (transform.root != collisiontransform.root)
        {
            //Debug.Log("inside on trigger");
            if ((obstacleLayer & (1<< other.gameObject.layer)) != 0 && once)
            {
                Debug.Log("Collided with Obstacle");
                // hipPart.AddExplosionForce(10000, other.transform.position, 5, 10);
                Vector3 forcedir = (hipPart.transform.position - collisiontransform.position).normalized;
                hipPart.AddForce((forcedir * 300 + Vector3.up * 200) , ForceMode.Impulse);

                //DamageData damage = new DamageData(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop, true);
                //Debug.Log(" impulseMagnitude: " + impulseMagnitude);
                //character.checkDamage(damage);
                once = false;
                OnObstacleCollision.Invoke();
            }
            else if ((characterLayer & (1 << other.gameObject.layer)) != 0 && once)
            {
                Debug.Log("Collided with Obstacle");
                // hipPart.AddExplosionForce(10000, other.transform.position, 5, 10);
                Vector3 forcedir = (hipPart.transform.position - collisiontransform.position).normalized;
                hipPart.AddForce((forcedir * 300 + Vector3.up * 200), ForceMode.Impulse);

                //DamageData damage = new DamageData(transform, rigidbody, collider, contacts, relativeVelocity, magnitude, impulseMagnitude, cashToDrop, true);
                //Debug.Log(" impulseMagnitude: " + impulseMagnitude);
                //character.checkDamage(damage);

                //Fight


                once = false;
                OnObstacleCollision.Invoke();
            }
            //else if (transform.root != collisiontransform.root)
        }
        
    }

    public void SetOnceValue(bool value)
    {
        once = value;
    }
    private void OnTriggerExit(Collider other)
    {
        Transform collisiontransform = other.transform;
        if (transform.root != collisiontransform.root)
        {
            //Debug.Log("inside on trigger");
            if ((obstacleLayer & (1 << other.gameObject.layer)) != 0 && !once)
            {
                //once = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
