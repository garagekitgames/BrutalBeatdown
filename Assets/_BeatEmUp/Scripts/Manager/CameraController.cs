using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;
using garagekitgames;
using DG.Tweening;
public class CameraController : UnitySingleton<CameraController>
{

    public CinemachineBrain camBrain;
    public CinemachineVirtualCamera camVcam;
    public CinemachineConfiner camConfiner;
    public CinemachineTargetGroup camTargetGroup;
    public CinemachineTargetGroup playerTargetGroup;

    public GameObject Player;
    public List<GameObject> Enemies;
    public float enemyWeight = 0.05f;

    public float enemyRadius = 0;

    public float playerWeight = 1;
    public float playerRadius = 1;

    public List<GameObject> camBounds;
    List<CinemachineTargetGroup.Target> enemyTargets = new List<CinemachineTargetGroup.Target>();
    public List<float> previousWeights = new List<float>();

    public bool showPlayerAndExit = true;

    public GameObject exit;
    public float smoothTime = 0.3f;
    public float yVelocity = 0.0f;

    public bool noEnemy = true;
    // Start is called before the first frame update
    void Start()
    {
        camBrain = GameObject.FindObjectOfType<CinemachineBrain>();
        camVcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        camConfiner = GameObject.FindObjectOfType<CinemachineConfiner>();
        //camTargetGroup = GameObject.FindObjectOfType<CinemachineTargetGroup>();

        Player = GameObject.FindGameObjectWithTag("CamTarget_Player");
        Enemies = GameObject.FindGameObjectsWithTag("CamTarget_Enemy").ToList<GameObject>();
        camBounds = GameObject.FindGameObjectsWithTag("CamBounds").ToList<GameObject>();


        playerTargetGroup.m_Targets = new CinemachineTargetGroup.Target[3];
        playerTargetGroup.m_Targets[0].target = Player.transform;
        playerTargetGroup.m_Targets[0].weight = playerWeight;
        playerTargetGroup.m_Targets[0].radius = playerRadius;

        exit = GameObject.FindGameObjectWithTag("Exit");

        DOTween.Init();
        if (showPlayerAndExit)
        {
            List<CinemachineTargetGroup.Target> targs = new List<CinemachineTargetGroup.Target>();

            if(exit)
            {
                targs.Add(new CinemachineTargetGroup.Target(exit.transform, 0.8f, 0));

            }
            SetupTarget(targs);
            InvokeRepeating("SetupCamTarget", 0.1f, 0.5f);
        }
        else
        {
            foreach (var item in Enemies)
            {
                enemyTargets.Add(new CinemachineTargetGroup.Target(item.transform, enemyWeight, enemyRadius));
                
                previousWeights.Add(enemyWeight);
            }

            SetupTarget(enemyTargets);
            InvokeRepeating("SetTargetWeights", 0.1f, 0.5f);
            //SetTargetWeights();
            SetupCameraBounds(0);
        }
        

        //InvokeRepeating("SetTargetWeights", 0.1f, 0.2f);
    }

    public void SetupTarget(List<CinemachineTargetGroup.Target> targets)
    {
        targets.Add(new CinemachineTargetGroup.Target(Player.transform, playerWeight, playerRadius));
        previousWeights.Add(playerWeight);
        camTargetGroup.m_Targets = targets.ToArray();
        //camTargetGroup.m_Targets.Clear();
        //camTargetGroup.m_Targets.Add(new CinemachineTargetGroup.Target(Player.transform, 1, 0));

        //foreach (var item in targets)
        //{
        //    camTargetGroup.m_Targets.Add(item);
        //}

        //camTargetGroup.m_Targets.Clear();
        //camTargetGroup.m_Targets.Insert(0, new CinemachineTargetGroup.Target(Player.transform, 1, 0));
        //camTargetGroup.m_Targets.Insert(1, new CinemachineTargetGroup.Target(ClosestEnemy().transform, enemyWeight, 0));


    }

    public void SetupCamTarget()
    {
        camTargetGroup.m_Targets = new CinemachineTargetGroup.Target[3];

        camTargetGroup.m_Targets[0].target = Player.transform;
        camTargetGroup.m_Targets[0].weight = playerWeight;
        camTargetGroup.m_Targets[0].radius = playerRadius;

        if (!noEnemy)
        {
            camTargetGroup.m_Targets[1].target = ClosestEnemy();
            camTargetGroup.m_Targets[1].weight = enemyWeight;
            camTargetGroup.m_Targets[1].radius = enemyRadius;
        }
        

        if(exit)
        {
            camTargetGroup.m_Targets[2].target = exit.transform;
            camTargetGroup.m_Targets[2].weight = 1f;
            camTargetGroup.m_Targets[2].radius = 0;
        }
        
    }

    public void SetTargetWeights()
    {
        //float prevDist = 999;
        //for (int i = 0; i < camTargetGroup.m_Targets.Length; i++)
        //{
        //    if(!(Player.transform != camTargetGroup.m_Targets[i].target))
        //    {
        //        var dist = (Player.transform.position - camTargetGroup.m_Targets[0].target.position).sqrMagnitude;

        //        if(dist < prevDist)
        //        {
        //            camTargetGroup.m_Targets[i].weight = 1 / i + 1;
        //        }
        //        //if (((Player.transform.position - camTargetGroup.m_Targets[0].target.position).sqrMagnitude) > )
        //    }
        //}

        Array.Sort<CinemachineTargetGroup.Target>(camTargetGroup.m_Targets, (x, y) => Vector3.Distance(Player.transform.position, x.target.transform.position)
                                                                                    .CompareTo(Vector3.Distance(Player.transform.position, y.target.transform.position)));

        //Array.Sort<CinemachineTargetGroup.Target>(camTargetGroup.m_Targets, (x, y) => Vector3.Distance(Player.transform.position, x.target.transform.position)
        //                                                                            .CompareTo(Vector3.Distance(Player.transform.position, y.target.transform.position)));
        

        for (int i = 0; i < camTargetGroup.m_Targets.Length; i++)
        {
            if(i == 0)
            {
                //camTargetGroup.m_Targets[i].weight = camTargetGroup.m_Targets.Length - 1;
            }
            
            else if(!camTargetGroup.m_Targets[i].target.transform.root.GetComponent<CharacterThinker>().health.alive)
            {
                camTargetGroup.m_Targets[i].weight = 0;
            }
            else
            {
                //Debug.Log("new Float value " + (float)1 / (float)(i + 5));
                //float newWeight = Mathf.SmoothDamp(camTargetGroup.m_Targets[i].weight, (float)1 / (float)(i + 5), ref yVelocity, smoothTime);
                //DOTween.To(() => camTargetGroup.m_Targets[i].weight, x => camTargetGroup.m_Targets[i].weight = x, (float)1 / (float)(i + 5), 1f);
                camTargetGroup.m_Targets[i].weight = (float)1 / (float)(i + 5);
                //previousWeights[i] = camTargetGroup.m_Targets[i].weight;

            }
            
        }

    }

    public void SetupCameraBounds(int index)
    {
       // camConfiner.m_BoundingVolume = camBounds[index].GetComponent<Collider>();
    }

    public Transform ClosestEnemy()
    {
        //Transform enemy;
        if(Enemies.Count > 0)
        {
            return Enemies.OrderBy(t => Vector3.Distance(Player.transform.position, t.transform.position)).FirstOrDefault().transform;

        }
        else
        {
            return Player.transform;
        }


    }
    // Update is called once per frame
    void Update()
    {

        

    }

    private void LateUpdate()
    {
        
    }
}
