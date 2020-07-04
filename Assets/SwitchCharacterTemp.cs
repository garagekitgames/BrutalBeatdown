using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCharacterTemp : MonoBehaviour
{
    public CinemachineTargetGroup camTargetGroup;

    public bool switchChar = false;
    public GameObject Player1;
    public GameObject Player2;
    public Transform p1CamTarget;
    public Transform p2CamTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SwitchCharacter()
    {
        switchChar = !switchChar;

        if(switchChar)
        {
            Player1.SetActive(true);
            Player2.SetActive(false);
            camTargetGroup.m_Targets = new CinemachineTargetGroup.Target[1];
            camTargetGroup.m_Targets[0].target = p1CamTarget;
            camTargetGroup.m_Targets[0].weight = 1;
            camTargetGroup.m_Targets[0].radius = 0;
        }
        else
        {
            Player1.SetActive(false);
            Player2.SetActive(true);
            camTargetGroup.m_Targets = new CinemachineTargetGroup.Target[1];
            camTargetGroup.m_Targets[0].target = p2CamTarget;
            camTargetGroup.m_Targets[0].weight = 1;
            camTargetGroup.m_Targets[0].radius = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
