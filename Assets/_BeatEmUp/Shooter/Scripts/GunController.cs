using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace garagekitgames.shooter
{

    public class GunController : MonoBehaviour
    {
        //Equip a gun
        public Transform weaponHolder;
        public Gun startingGun;
        Gun equippedGun;
        public void EquipGun(Gun gunToEquip)
        {

            if (equippedGun != null)
            {
                Destroy(equippedGun.gameObject);
            }
            equippedGun = Instantiate(gunToEquip, weaponHolder.position, weaponHolder.rotation, weaponHolder) as Gun;
        }
        // Start is called before the first frame update
        void Start()
        {
            //Equipping a default Gun
            if (startingGun != null)
            {
                EquipGun(startingGun);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Shoot()
        {
            if (equippedGun != null)
            {
                equippedGun.Shoot();
            }

        }

        public void Aim(Vector3 aimPoint)
        {
            if (equippedGun != null)
            {
                equippedGun.Aim(aimPoint);
            }
        }
    }

}
