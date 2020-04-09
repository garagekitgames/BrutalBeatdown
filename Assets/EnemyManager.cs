using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using garagekitgames;

public class EnemyManager : MonoBehaviour
{
    public CharacterRuntimeSet enemyRuntimeSet;
    public CharacterRuntimeSet playerRuntimeSet;

    public bool playerBeganChased;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SetEnemyPriority", 2);
        //SetEnemyPriority();
    }

    public void SetEnemyPriority()
    {
        int i = 1;
        foreach (var item in enemyRuntimeSet.Items)
        {
            item.agent.avoidancePriority = i;
            //item.transform.FindDeepChild("EnemyAwareness").GetComponent<EnemyAwareness>().Alert();
            i++;
        }
    }

    public void CheckIfPlayerCanBeSeen()
    {
        int i = 1;
        if(playerRuntimeSet.Items[0].isSeen && !playerBeganChased)
        {
            AudioManager.instance.Play("Siren");
            //AudioManager.instance.FadeInCaller("BGM2", 0.1f, 0.5f);
            //AudioManager.instance.FadeOutCaller("BGM1", 0.1f);
            playerBeganChased = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerCanBeSeen();
    }
}
