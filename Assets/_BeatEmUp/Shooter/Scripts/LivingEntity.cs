using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace garagekitgames.shooter
{
    public class LivingEntity : MonoBehaviour, IDamageable
    {
        public float startingHealth;
        public float health;
        protected bool dead;

        public event Action OnDeath;

        public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection, int teamID)
        {
            TakeDamage(damage);
        }

        public virtual void TakeDamage(float damage)
        {
            health -= damage;
            AudioManager.instance.Play("Hit");
            if (health <= 0 && !dead)
            {
                Die();
            }
        }

        private void Die()
        {
            AudioManager.instance.Play("Death");
            dead = true;
            if (OnDeath != null)
            {
                OnDeath();
            }
            Destroy(this.gameObject);
        }

        // Start is called before the first frame update
        public virtual void Start()
        {
            health = startingHealth;
        }

        // Update is called once per frame
        void Update()
        {

        }




    }

}
