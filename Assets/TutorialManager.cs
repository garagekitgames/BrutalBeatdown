using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.GameStructure;

public class TutorialManager : UnitySingleton<TutorialManager>
{

	public MultiFighterCamera mycam;
    public Transform target;
    public ClampToObject clamp;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Levels.Selected.Number == 1)
        {
            if(mycam.enemyTarget != null)
            {
                target = mycam.enemyTarget;

                clamp.targetObject = target;

                clamp.transform.gameObject.SetActive(true);
            }
            else
            {
                clamp.transform.gameObject.SetActive(false);
            }
            
        }
        else
        {
            clamp.transform.gameObject.SetActive(false);
        }
    }
}
