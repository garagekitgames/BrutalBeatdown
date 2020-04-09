using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using garagekitgames;
using DG.Tweening;

public class LaserDetector : MonoBehaviour
{
    public BoxCollider laserCollider;
    public Animator laserAnimator;
    public float laserWaitDuration = 3;
    private WaitForSeconds waitSec;
    public AudioSource laserAudio;
    public bool laserState;
    public CharacterRuntimeSet enemyCharacterRuntimeSet;
    public CharacterRuntimeSet playerRuntimeSet;
    public ParticleSystem[] laserGlows;
    // Start is called before the first frame update
    void Start()
    {
        //laserCollider = this.GetComponent<BoxCollider>();

        waitSec = new WaitForSeconds(laserWaitDuration);
        StartCoroutine("LaserOnOff");


    }

    public IEnumerator LaserOnOff()
    {
        while(true)
        {
            LaserOn();

            yield return waitSec;

            LaserOff();

            yield return waitSec;
        }
        
    }

    public void LaserOn()
    {
        //transform.DOScaleZ(1, 1);
        //transform.DOMoveX(0f, 1);
        laserAnimator.SetTrigger("LaserOn");
        AudioManager.instance.Play("LaserOn");
        laserAudio.Play();
        foreach (var item in laserGlows)
        {
            item.Play();
        }
        laserState = true;
    }

    public void LaserOff()
    {
        //transform.DOScaleZ(0, 1);
        //transform.DOMoveX(1.5f, 1);
        laserAnimator.SetTrigger("LaserOff");
        AudioManager.instance.Play("LaserOff");
        laserAudio.Stop();
        foreach (var item in laserGlows)
        {
            item.Stop();
        }
        laserState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(laserState)
        {
            if(other.gameObject.CompareTag("Box"))
            {
                //enemyCharacterRuntimeSet
                int i = 1;
                foreach (var item in enemyCharacterRuntimeSet.Items)
                {
                    //AudioManager.instance.Play("Siren");
                    //item.agent.avoidancePriority = i;
                    playerRuntimeSet.Items[0].isSeen = true;
                    item.transform.FindDeepChild("Awareness").GetComponent<EnemyAwareness>().Alert(other.transform.position);
                    i++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
