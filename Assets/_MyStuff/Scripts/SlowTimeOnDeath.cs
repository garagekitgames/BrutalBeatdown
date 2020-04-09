using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeOnDeath : MonoBehaviour {

    public float timeslowDuration = 2;
	// Use this for initialization
	void Start () {
		
	}
	
    public void onDeath()
    {
        //EffectsController.Instance.slowDownTime(4, 0.02f, 2);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
