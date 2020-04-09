using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.GameStructure;
using System.IO;

public class SneakyGameLevelSetup : MonoBehaviour
{
	public Transform player;
	public Transform playerStart;
	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("PlayerHipRoot").transform;
		playerStart = GameObject.FindGameObjectWithTag("StartPosition").transform;

        player.transform.position = playerStart.position;
        //Invoke("SetupPlayerStart", 1f);

	}

    public void SetupPlayerStart()
	{
        player.transform.position = playerStart.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
