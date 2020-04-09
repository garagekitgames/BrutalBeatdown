using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.Events;
namespace garagekitgames.shooter
{
    public class Gun : MonoBehaviour
    {
        public Transform muzzle;
        public Transform shellEject;
        public Projectile projectile;
        public Shell shell;
        public float msBetweenShots = 100;
        public float muzzleVelocity = 35;
        public CharacterThinker gunOwner;
        public UnityEvent OnGunFire;

        float nextShotTime;
        MuzzleFlash muzzleFlash;

        EZObjectPool bulletObjectPool = new EZObjectPool();
        public enum FireMode
        {
            Auto,
            Burst,
            Single
        }
        public void Shoot()
        {
            if (Time.time > nextShotTime)
            {

                GameObject bullet;
                bulletObjectPool.TryGetNextObject(muzzle.position, muzzle.rotation, out bullet);

                Projectile newProjectile = bullet.GetComponent<Projectile>();
                newProjectile.SetSpeed(muzzleVelocity);
                newProjectile.SetShotBy(gunOwner);
                nextShotTime = Time.time + (msBetweenShots / 1000);

                //Shell newshell = Instantiate(shell, shellEject.position, shellEject.rotation) as Shell;
                muzzleFlash.Activate();
                OnGunFire.Invoke();
                //AudioManager.instance.Play("gunShot");
                //AudioManager.instance.Play("CoinCollect");
                //CameraShaker.Instance.ShakeOnce(1, 4, 0.05f, 0.4f);
            }

        }

        public void Aim(Vector3 aimPoint)
        {
            transform.LookAt(aimPoint);
        }

        // Start is called before the first frame update
        void Start()
        {
            muzzleFlash = GetComponent<MuzzleFlash>();
            bulletObjectPool = EZObjectPool.CreateObjectPool(projectile.gameObject, projectile.name, 100, true, true, true);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
