using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using garagekitgames;

public class MusicalGround : MonoBehaviour
{

    //[SerializeField]
    //float xRotation;

    //[SerializeField]
    //float yRotation;

    //[SerializeField]
    //float zRotation;

    //[SerializeField]
    //float speed;

    //[SerializeField]
    //float rotationAngle;

    //[SerializeField]
    //float timeIntervalBetweenRotation;

    //[SerializeField]
    //bool stop;

    //[SerializeField]
    //bool randomizeRotaion;

    //[SerializeField]
    //AnimationCurve animationCurve;

    [SerializeField]
    float positionToMove;

    [SerializeField]
    float timeToBeTakenToMove;

    [SerializeField]
    float intervalToStay;

    public CharacterRuntimeSet enemyCharacterRuntimeSet;
    public CharacterRuntimeSet playerRuntimeSet;

    public AudioSource groundAudio;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(MovePlatform(positionToMove));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(xRotation, yRotation, zRotation * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //enemyCharacterRuntimeSet
            int i = 1;
            foreach (var item in enemyCharacterRuntimeSet.Items)
            {
                //AudioManager.instance.Play("Siren");
                //item.agent.avoidancePriority = i;
                playerRuntimeSet.Items[0].isSeen = true;
                item.transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>().Alert(collider.transform.position);
                i++;
            }
        }
    }

   
    //void LerpPos()
    //{
    //    DOTween.Sequence().Append(
    //    transform.DOMoveY(transform.position.y + positionToMove, timeToBeTaken)).Append(transform.DOMoveY(transform.position.y + 1, 4)).SetLoops(-1, LoopType.Yoyo);
    //    //transform.DOMoveY(transform.position.y + positionToMove, timeToBeTaken).SetLoops(-1, LoopType.Yoyo);
    //}

    IEnumerator MovePlatform(float _dist)
    {
        groundAudio.Play();
        transform.DOMoveY(transform.position.y + _dist, timeToBeTakenToMove);
        yield return new WaitForSeconds(timeToBeTakenToMove);
        groundAudio.Stop();
        yield return new WaitForSeconds(intervalToStay);
        StartCoroutine(MovePlatform((-1) * _dist));
    }

    //IEnumerator LerpPosition(float duration = 1.0f)
    //{
    //    Vector3 initialPos = transform.position;

    //    float elapsed = 0.0f;
    //    while (elapsed < duration)
    //    {
    //        transform.position = new Vector3(transform.position.x, 5 * animationCurve.Evaluate((Time.time % animationCurve.length * 1)), transform.position.z);

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }


    //    yield return new WaitForSeconds(timeIntervalBetweenRotation);
    //    transform.position = initialPos;

    //    if (!stop)
    //        StartCoroutine(LerpPosition(duration));
    //}

    //}
}
