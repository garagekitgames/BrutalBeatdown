using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using DG.Tweening;



public class WeaponScript : MonoBehaviour
{
    public CharacterThinker character;
    public BodyPartMono myBP;
    public BodyPartMono weaponHolderPart;

    public AttackData[] weaponAttack;
    public FixedJoint test;

    //----------------------------//


    //Weapon States
    public enum WeaponStates
    {
        Stationary,
        Rolling,
        Thrown,
        InHand
    }

    public WeaponStates weaponState;

    public int thrownBy;

    public CharacterThinker ownerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        myBP = GetComponent<BodyPartMono>();
        myBP.BodyPartRb.maxAngularVelocity = 300000;
    }

    // Update is called once per frame
    void Update()
    {
        //if()
    }

    public void rotateWeapon()
    {
        transform.DOLocalRotate(new Vector3(0f, -90f, -90f), .25f).SetUpdate(true);
    }
    public void MakeJoint()
    {
        //var test = gameObject.AddComponent<ConfigurableJoint>();
        //// childConfigJoint.connectedBody = collision.rigidbody;
        ////grabSuccess = true;
        //test.connectedBody = weaponHolderPart.BodyPartRb;

        ////test.connectedBody = collision.rigidbody;
        ////this.actor.controlHandeler.leftCanClimb = true;
        //test.xMotion = ConfigurableJointMotion.Locked;
        //test.yMotion = ConfigurableJointMotion.Locked;
        //test.zMotion = ConfigurableJointMotion.Locked;

        //test.angularXMotion = ConfigurableJointMotion.Locked;
        //test.angularYMotion = ConfigurableJointMotion.Locked;
        //test.angularZMotion = ConfigurableJointMotion.Locked;

        ////test.xMotion = ConfigurableJointMotion.Limited;
        ////test.yMotion = ConfigurableJointMotion.Limited;
        ////test.zMotion = ConfigurableJointMotion.Limited;
        ////SoftJointLimit testLinearLimit = new SoftJointLimit();
        ////testLinearLimit.limit = 0.001f;
        ////testLinearLimit.bounciness = 1f;
        ////test.linearLimit = testLinearLimit;

        ////SoftJointLimitSpring testLinearLimitSpring = new SoftJointLimitSpring();
        ////testLinearLimitSpring.spring = 1000;
        ////testLinearLimitSpring.damper = 1;
        ////test.linearLimitSpring = testLinearLimitSpring;

        //test.enablePreprocessing = false;
        //test.projectionDistance = 0.1f;
        //test.projectionAngle = 180f;
        //test.projectionMode = JointProjectionMode.PositionAndRotation;
        //test.enableCollision = false;




        if(weaponHolderPart != null && character != null)
        {
            test = gameObject.AddComponent<FixedJoint>();
            test.connectedBody = weaponHolderPart.BodyPartRb;

            myBP.BodyPartRb.isKinematic = false;
            myBP.BodyPartRb.interpolation = RigidbodyInterpolation.Interpolate;
            myBP.bodyPartCollider.isTrigger = false;
        }

        

        
    }
    public void Pickup(BodyPartMono weaponHolder, CharacterThinker _character)
    {


        


        character = _character;
        weaponHolderPart = weaponHolder;
        myBP.AddToBodyParts(_character);
        //
        //if (!active)
        //    return;

        //SuperHotScript.instance.weapon = this;
        //ChangeSettings();


        myBP.BodyPartRb.velocity = Vector3.zero;
        myBP.BodyPartRb.angularVelocity = Vector3.zero;

        //myBP.BodyPartRb.angularDrag = 0.05f;
        //myBP.BodyPartRb.drag = 0;
        //myBP.BodyPartRb.mass = 1;
        //ballRB.isKinematic = true;

        //myBP.BodyPartRb.useGravity = false;

        myBP.BodyPartRb.WakeUp();


        myBP.BodyPartRb.isKinematic = true;
        myBP.BodyPartRb.interpolation = RigidbodyInterpolation.None;
        myBP.bodyPartCollider.isTrigger = true;

        transform.parent = weaponHolder.bodyPartTransform;

        transform.DOLocalMove(new Vector3(-0.1f, 0f, 0f), .25f).SetEase(Ease.OutBack).SetUpdate(true);
            
        transform.DOLocalRotate(new Vector3(0f, -90f, -90f), .25f).SetUpdate(true).OnComplete(MakeJoint);


        //transform.DOLocalMove(new Vector3(-0.1f, 0f, 0f), .25f).SetEase(Ease.OutBack).SetUpdate(true);
        //transform.DOLocalRotate(new Vector3(0f, -90f, -90f), .25f).SetUpdate(true);

        //myBP.BodyPartRb.isKinematic = true;
        //transform.Get


    }

    public void Attack()
    {
        if (!character.attacking)
        {

            //character.targetting = true;
            character.currentAttack = weaponAttack[UnityEngine.Random.Range(0, weaponAttack.Length)];
            IEnumerator coroutine = character.DoSimpleAttack(0.2f);
            StartCoroutine(coroutine);
            //character.attack = true;

        }
    }

    public void BreakJoint()
    {
        Destroy(test);

    }

    public void DestroyReference()
    {
        myBP.RemoveFromBodyParts(character);

        character = null;
        weaponHolderPart = null;

    }


    public void Throw()
    {

        var throwDirection = (character.target - character.bpHolder.bodyPartsName[BodyPartNames.hipName].transform.position).normalized;
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(transform.position - transform.forward, .01f)).SetUpdate(true);
        s.AppendCallback(() => transform.parent = null);
        s.AppendCallback(() => BreakJoint());
        //s.AppendCallback(() => transform.position = Camera.main.transform.position + (Camera.main.transform.right * .1f));
        //s.AppendCallback(() => ChangeSettings());
        s.AppendCallback(() => myBP.BodyPartRb.AddForce(throwDirection * 25, ForceMode.Impulse));
        //s.AppendCallback(() => myBP.BodyPartRb.AddTorque(transform.transform.right * 5, ForceMode.Impulse));
        s.AppendCallback(() => DestroyReference());
    }

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    collider = GetComponent<Collider>();
    //    renderer = GetComponent<Renderer>();

    //    ChangeSettings();
    //}

    //void ChangeSettings()
    //{
    //    if (transform.parent != null)
    //        return;

    //    rb.isKinematic = (SuperHotScript.instance.weapon == this) ? true : false;
    //    rb.interpolation = (SuperHotScript.instance.weapon == this) ? RigidbodyInterpolation.None : RigidbodyInterpolation.Interpolate;
    //    collider.isTrigger = (SuperHotScript.instance.weapon == this);
    //}

    //public void Shoot(Vector3 pos, Quaternion rot, bool isEnemy)
    //{
    //    if (reloading)
    //        return;

    //    if (bulletAmount <= 0)
    //        return;

    //    if (!SuperHotScript.instance.weapon == this)
    //        bulletAmount--;

    //    GameObject bullet = Instantiate(SuperHotScript.instance.bulletPrefab, pos, rot);

    //    if (GetComponentInChildren<ParticleSystem>() != null)
    //        GetComponentInChildren<ParticleSystem>().Play();

    //    if (SuperHotScript.instance.weapon == this)
    //        StartCoroutine(Reload());

    //    Camera.main.transform.DOComplete();
    //    Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true);

    //    if (SuperHotScript.instance.weapon == this)
    //        transform.DOLocalMoveZ(-.1f, .05f).OnComplete(() => transform.DOLocalMoveZ(0, .2f));
    //}

    //public void Throw()
    //{
    //    Sequence s = DOTween.Sequence();
    //    s.Append(transform.DOMove(transform.position - transform.forward, .01f)).SetUpdate(true);
    //    s.AppendCallback(() => transform.parent = null);
    //    s.AppendCallback(() => transform.position = Camera.main.transform.position + (Camera.main.transform.right * .1f));
    //    s.AppendCallback(() => ChangeSettings());
    //    s.AppendCallback(() => rb.AddForce(Camera.main.transform.forward * 10, ForceMode.Impulse));
    //    s.AppendCallback(() => rb.AddTorque(transform.transform.right + transform.transform.up * 20, ForceMode.Impulse));
    //}

    //public void Pickup()
    //{
    //    if (!active)
    //        return;

    //    SuperHotScript.instance.weapon = this;
    //    ChangeSettings();

    //    transform.parent = SuperHotScript.instance.weaponHolder;

    //    transform.DOLocalMove(Vector3.zero, .25f).SetEase(Ease.OutBack).SetUpdate(true);
    //    transform.DOLocalRotate(Vector3.zero, .25f).SetUpdate(true);
    //}

    //public void Release()
    //{
    //    active = true;
    //    transform.parent = null;
    //    rb.isKinematic = false;
    //    rb.interpolation = RigidbodyInterpolation.Interpolate;
    //    collider.isTrigger = false;

    //    rb.AddForce((Camera.main.transform.position - transform.position) * 2, ForceMode.Impulse);
    //    rb.AddForce(Vector3.up * 2, ForceMode.Impulse);

    //}

    //IEnumerator Reload()
    //{
    //    if (SuperHotScript.instance.weapon != this)
    //        yield break;
    //    SuperHotScript.instance.ReloadUI(reloadTime);
    //    reloading = true;
    //    yield return new WaitForSeconds(reloadTime);
    //    reloading = false;
    //}

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("Enemy") && collision.relativeVelocity.magnitude < 15)
    //    {
    //        BodyPartScript bp = collision.gameObject.GetComponent<BodyPartScript>();

    //        if (!bp.enemy.dead)
    //            Instantiate(SuperHotScript.instance.hitParticlePrefab, transform.position, transform.rotation);

    //        bp.HidePartAndReplace();
    //        bp.enemy.Ragdoll();
    //    }

    //}
}
