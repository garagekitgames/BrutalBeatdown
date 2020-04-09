using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace garagekitgames.shooter
{
    public class Projectile : MonoBehaviour
    {
        float bulletSpeed = 10;
        public LayerMask collisionMask;

        public float damage = 1;

        public float damageForce = 8000;
        public Vector3 velocity;

        public CharacterThinker shotByCharacter;

        public void SetSpeed(float speed)
        {
            bulletSpeed = speed;
        }

        public void SetShotBy(CharacterThinker _shotByCharacter)
        {
            shotByCharacter = _shotByCharacter;
        }
        // Start is called before the first frame update
        void Start()
        {
            //Destroy(this.gameObject, 2);
        }

        // Update is called once per frame
        void Update()
        {
            float moveDistance = bulletSpeed * Time.deltaTime;
            CheckCollisions(moveDistance);
            transform.Translate(Vector3.forward * moveDistance);

            
        }

        private void CheckCollisions(float moveDistance)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
            {
                OnHitObject(hitInfo);


            }
            else
            {
                StartCoroutine(DisableAfterX(3));
            }

            velocity = transform.forward - transform.position;
        }

        private void OnHitObject(RaycastHit hitInfo)
        {
            Debug.Log("Hit Body Normal: " + hitInfo.normal);
            IDamageable damageableObject = hitInfo.collider.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeHit(damage, hitInfo.point, transform.forward, shotByCharacter.teamID);
            }
            Rigidbody hitBody = hitInfo.collider.GetComponent<Rigidbody>();
            if (hitBody != null)
            {
                hitBody.AddForce(-hitInfo.normal * damageForce);
                Debug.Log("Hit Body : " + hitBody.transform.name);
            }
            this.gameObject.SetActive(false);
        }

        IEnumerator DisableAfterX(float sec)
        {
            yield return new WaitForSeconds(sec);
            this.gameObject.SetActive(false);
        }

        public int Add(int a, int b)
        {
            int c = a + b;
            return c;
        }
    }

}
