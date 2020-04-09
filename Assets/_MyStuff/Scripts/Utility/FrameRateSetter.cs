using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateSetter : MonoBehaviour {

    public int targetFrameRate2 = 30;

    void Awake()
    {
        if (targetFrameRate2 != Application.targetFrameRate)
        {
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate2;
        }
       // Screen.SetResolution(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, true);
    }
    // Use this for initialization
    void Start () {
        if (targetFrameRate2 != Application.targetFrameRate)
        {
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate2;
        }
       // Screen.SetResolution(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, true);
		
	}
	
	// Update is called once per frame
	void Update () {

       // if(targetFrameRate != Application.targetFrameRate)
       // {
       //     Application.targetFrameRate = targetFrameRate;
       // }
		
	}
}
