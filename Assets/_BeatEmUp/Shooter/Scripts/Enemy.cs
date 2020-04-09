using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using garagekitgames.shooter;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public GameObject deathEffect;
    Transform playerTarget;
    NavMeshAgent pathFinder;

    float attackInterval = 1;
    float nextAttackTime;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        StartCoroutine(updatePath());
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection, int teamID)
    {
        if(damage >= health)
        {
            Destroy(Instantiate(deathEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, 2);

        }
        TakeDamage(damage);
    }

    

    IEnumerator updatePath()
    {
        float refreshRate = 0.25f;

        while(playerTarget != null)
        {
            Vector3 targetPosition = new Vector3(playerTarget.position.x, 0, playerTarget.position.z);
            if(!dead)
            {
                pathFinder.SetDestination(targetPosition);
            }
            
            yield return new WaitForSeconds(refreshRate);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null && collision.transform.CompareTag("Player"))
        {
            damageable.TakeDamage(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(Time.time > nextAttackTime)
        {
            IDamageable damageable = collision.transform.GetComponent<IDamageable>();
            if (damageable != null && collision.transform.CompareTag("Player"))
            {
                damageable.TakeDamage(1);
            }

            nextAttackTime = Time.time + attackInterval;
        }
       
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
