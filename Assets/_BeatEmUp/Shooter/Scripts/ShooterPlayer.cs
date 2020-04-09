using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames.shooter;

namespace garagekitgames.shooter
{

    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(GunController))]
    public class ShooterPlayer : LivingEntity
    {
        public float speed;
        PlayerController controller;
        GunController gunController;

        public Renderer playerMaterial;

        Color originalColor;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            controller = GetComponent<PlayerController>();
            gunController = GetComponent<GunController>();
            playerMaterial = GetComponent<MeshRenderer>();
            originalColor = playerMaterial.material.color;
        }
        public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection, int TeamID)
        {
            if (damage >= health)
            {
                AudioManager.instance.Play("PlayerDeath");

            }
            playerMaterial.material.color = Color.red;
            Invoke("ResetColor", 0.5f);
            //TakeDamage(damage);
        }

        void ResetColor()
        {
            playerMaterial.material.color = originalColor;
        }
        /*
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.CompareTag("Enemy"))
            {

            }
        }*/
        // Update is called once per frame
        void Update()
        {
            //Move Input
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            Vector3 moveVelocity = moveInput.normalized * speed;
            controller.Move(moveVelocity);

            //Turn Input
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            //To get interesection point
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                //point of intersection between ray and ground plane
                Vector3 point = ray.GetPoint(rayDistance);
                Debug.DrawLine(ray.origin, point);

                controller.LookAt(point);
                gunController.Aim(new Vector3(point.x, transform.position.y, point.z));
            }


            //Shoot Input
            if (Input.GetMouseButton(0))
            {
                gunController.Shoot();
            }

        }
    }

}