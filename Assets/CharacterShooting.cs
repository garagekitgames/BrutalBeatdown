using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using EZObjectPools;
using Panda;
using garagekitgames.shooter;
public class CharacterShooting : MonoBehaviour
{

	public CharacterThinker character;
    public float damage = 10f;
    public float range = 100f;
    public float speed = 100f;

    public Transform shootFrom;
    public LayerMask targetLayer;

    public GameObject bulletPrefab;
    EZObjectPool bulletObjectPool = new EZObjectPool();

    // Start is called before the first frame update

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
        equippedGun.gunOwner = character;
    }

    void Start()
    {
		character = this.GetComponent<CharacterThinker>();
        //bulletObjectPool = EZObjectPool.CreateObjectPool(bulletPrefab, bulletPrefab.name, 10, true, true, true);

        //Equipping a default Gun
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(character.health.alive)
        {
            //character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartFaceDirection.enabled = true;
            //character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartFaceDirection.enabled = true;
            //character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartFaceDirection.enabled = true;

            //character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartFaceDirection.enabled = true;
            //character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartFaceDirection.enabled = true;
            //character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartFaceDirection.enabled = true;

            character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
            character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
            character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartFaceDirection.facingDirection = character.targetDirection;

            character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
            character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
            character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartFaceDirection.facingDirection = character.targetDirection;
        }
        else
        {
            character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartFaceDirection.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartFaceDirection.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartFaceDirection.enabled = false;

            character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartFaceDirection.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartFaceDirection.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartFaceDirection.enabled = false;


            character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartMaintainHeight.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartMaintainHeight.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartMaintainHeight.enabled = false;

            character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartMaintainHeight.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartMaintainHeight.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartMaintainHeight.enabled = false;

            character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartUpright.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartUpright.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartUpright.enabled = false;

            character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartUpright.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartUpright.enabled = false;
            character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartUpright.enabled = false;

        }
        //character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
        //character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
        //character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartFaceDirection.facingDirection = character.targetDirection;

        //character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
        //character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
        //character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartFaceDirection.facingDirection = character.targetDirection;
    }

    //[Task]
    public void Shoot()
    {
        if (equippedGun != null)
        {
            character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartFaceDirection.enabled = true;
            character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartFaceDirection.enabled = true;
            character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartFaceDirection.enabled = true;

            character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartFaceDirection.enabled = true;
            character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartFaceDirection.enabled = true;
            character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartFaceDirection.enabled = true;

            character.bpHolder.bodyPartsName[BodyPartNames.larmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
            character.bpHolder.bodyPartsName[BodyPartNames.lfarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
            character.bpHolder.bodyPartsName[BodyPartNames.lhandName].BodyPartFaceDirection.facingDirection = character.targetDirection;

            character.bpHolder.bodyPartsName[BodyPartNames.rarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
            character.bpHolder.bodyPartsName[BodyPartNames.rfarmName].BodyPartFaceDirection.facingDirection = character.targetDirection;
            character.bpHolder.bodyPartsName[BodyPartNames.rhandName].BodyPartFaceDirection.facingDirection = character.targetDirection;

            equippedGun.Aim(character.target);
            equippedGun.Shoot();


            //Task.current.Succeed();
            
        }
        else
        {
            //Task.current.Fail();
        }

    }

    [Task]
    public void FireBullet()
    {
        //GameObject bullet;
        //bulletObjectPool.TryGetNextObject(shootFrom.position, Quaternion.identity, out bullet);

        //var bulletRb = bullet.GetComponent<Rigidbody>();
        //bulletRb.AddForce(character.targetDirection * speed);



        Task.current.Succeed();
    }
}
