using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using System;
using garagekitgames;
using System.Linq;

public class EnemyAwareness : MonoBehaviour
{
    [Range(0, 20)]
    public float viewRadius = 3.1f;
    [Range(0, 360)]
    public float fieldOfView = 90.0f; // Object within this angle are seen.
                                      //public float closeFieldDistance = 1.0f; // Objects below this distance is always seen.


    [Range(0, 20)]
    public float alertRadius = 10f;

    public List<Collider> colliders = new List<Collider>();
    public List<GameObject> visibles = new List<GameObject>();
    public List<GameObject> visibleTargets = new List<GameObject>();
    public List<GameObject> allies = new List<GameObject>();
    public List<GameObject> hidingSpots = new List<GameObject>();
    public List<GameObject> patrolSpots = new List<GameObject>();
    // Start is called before the first frame update

    public EnemyAIBase enemyAI;
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public LayerMask obstacleMask;
    public LayerMask targetMask;
    public LayerMask allyAndHideMask;

    public string allyTag = "allyAwareness";
    public string hideSpotTag = "HideSpot";
    public string patrolSpotTag = "PatrolSpot";

    public bool stopNow;

    public Vector3 playerLastSeen;
    public Vector3 defaultLastSeen;
    public Vector3 privateLastSeen;
    public Vector3 previousLastSeen;

    public bool canSeePlayer;
    public bool hasLosToPlayer;
    public bool facingPlayer;
    public Transform player;

    public float playerLastSeenDuration;
    public float playerLastSeenCounter;

    public Sprite suspicious;
    public Sprite alert;
    public Sprite dead;
    public SpriteRenderer emote;

    public enum EmoteState { idle, suspicious, alert};

    public EmoteState emotion;
    public BehaviorTree behaviorTree;

    public MeshRenderer viewMeshRenderer;

    public CharacterThinker character;
    public Transform vision;

    public CharacterThinker targetCharacter;

    public bool checkForPlayerWalking;

    public float distanceBetweenPlayer;

    public bool playerCaught;

    public void Awake()
    {
        enemyAI = this.transform.root.GetComponent<EnemyAIBase>();
        behaviorTree = this.transform.root.GetComponent<BehaviorTree>();
        character = this.transform.root.GetComponent<CharacterThinker>();
        vision = character.transform.FindDeepChild("VisionCone");
        
        
    }

    public void playerSeenNow()
    {
        AudioManager.instance.FadeInCaller("BGM2", 0.1f, 0.5f);
        AudioManager.instance.FadeOutCaller("BGM1", 0.1f);
    }

    public void SetPlayerCaught(bool value)
    {
        playerCaught = value;
    }
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        playerLastSeen = new Vector3(-1000, -1000, -1000);
        defaultLastSeen = new Vector3(-1000, -1000, -1000);
        player = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
        targetCharacter = player.root.GetComponent<CharacterThinker>();
        hidingSpots = GameObject.FindGameObjectsWithTag("HideSpot").ToList<GameObject>();
        patrolSpots = GameObject.FindGameObjectsWithTag(patrolSpotTag).ToList<GameObject>();
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(fieldOfView * meshResolution);
        float stepAngleSize = fieldOfView / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - fieldOfView / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            Debug.DrawLine(transform.position, transform.position + DirectionFromAngle(angle, true) * viewRadius);


            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }






            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }



    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirectionFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (stopNow)
        {
            canSeePlayer = false;
            hasLosToPlayer = false;
            facingPlayer = false;
            emote.enabled = false;
            if (vision)
            {
                vision.GetComponent<Pathfinding.DynamicGridObstacle>().setTag = 0;
            }
            return;
        }
        
        //UpdateVisibility();

        //FindVisibleTargets();
        //FindAlliesAndHidingSpot();
        FindTargetAlliesAndHidingSpot();

        //HasLosToPlayer();

        //CanSeePlayer();

        ResetPlayerLastSeen();
        //FacingPlayer();
        SetBehaviorVariables();
        GetBehaviorVariables();

        if(emotion == EmoteState.alert)
        {
            
            emote.enabled = true;
            emote.sprite = alert;
            if (vision)
            {
                vision.GetComponent<Pathfinding.DynamicGridObstacle>().setTag = 0;
            }
            character.agent.speed = 6f;//4f;//6f;
                                       //viewMeshRenderer.mat

            //if (distanceBetweenPlayer < 4)
            //{
            //    character.GetComponent<Jump>().JumpNow();
            //}
        }
        else if(emotion == EmoteState.suspicious)
        {
            emote.enabled = true;
            emote.sprite = suspicious;
            //emote.enabled = false;
            if (vision)
            {
                vision.GetComponent<Pathfinding.DynamicGridObstacle>().setTag = 2;
            }
            character.agent.speed = 6f;//4f;//6f;

        }
        else
        {
            emote.enabled = false;
            if (vision)
            {
                vision.GetComponent<Pathfinding.DynamicGridObstacle>().setTag = 2;
            }
            character.agent.speed = 2.3f;//2.8f;
        }

        if (playerLastSeen != defaultLastSeen)
        {
            if (playerLastSeen != privateLastSeen)
            {
                //var playerLastSeenVar = (SharedVector3)GlobalVariables.Instance.GetVariable("GlobalLastSeen");
                //playerLastSeen = playerLastSeenVar.Value;
                privateLastSeen = playerLastSeen;
            }
        }

        


    }

    private void SetBehaviorVariables()
    {
        var isPlayerVisible = (SharedBool)behaviorTree.GetVariable("isPlayerVisible");
        isPlayerVisible.Value = canSeePlayer;

        var hasLineOfSightToPlayer = (SharedBool)behaviorTree.GetVariable("hasLineOfSightToPlayer");
        hasLineOfSightToPlayer.Value = hasLosToPlayer;

        var isPlayerLastSeen = (SharedBool)behaviorTree.GetVariable("isPlayerLastSeen");
        isPlayerLastSeen.Value = (playerLastSeen != defaultLastSeen);


        var playerLastSeenVariable = (SharedVector3)behaviorTree.GetVariable("playerLastSeen");
        playerLastSeenVariable.Value = playerLastSeen;

        var privateLastSeenVariable = (SharedVector3)behaviorTree.GetVariable("privateLastSeen");
        privateLastSeenVariable.Value = privateLastSeen;

        var distToPlayer = (SharedFloat)behaviorTree.GetVariable("distToPlayer");
        distToPlayer.Value = distanceBetweenPlayer;

        var playerCaughtVariable = (SharedBool)behaviorTree.GetVariable("playerCaught");
        playerCaughtVariable.Value = playerCaught;
    }

    private void GetBehaviorVariables()
    {
        //throw new NotImplementedException();

        var myIntVariable = (SharedInt)behaviorTree.GetVariable("emotion");
        emotion = (EnemyAwareness.EmoteState)myIntVariable.Value;

        var fovVariable = (SharedFloat)behaviorTree.GetVariable("fieldOfView");
        fieldOfView = fovVariable.Value;

        var viewRadiusVariable = (SharedFloat)behaviorTree.GetVariable("viewRadius");
        viewRadius = viewRadiusVariable.Value;
        alertRadius = viewRadiusVariable.Value;
    }

    public void ResetPlayerLastSeen()
    {
        if (playerLastSeen != defaultLastSeen)
        {
            playerLastSeenCounter -= Time.deltaTime;
        }
        if (playerLastSeenCounter <= 0)
        {
            playerLastSeen = defaultLastSeen;
            playerLastSeenCounter = playerLastSeenDuration;
        }
    }
    public bool CanSeePlayer()
    {
        //Vector3 playerPosition = Vector3.
        for (int i = 0; i < visibleTargets.Count; i++)
        {
            GameObject item = visibleTargets[i];
            if (item.transform == player.transform)
            {
                canSeePlayer = true;
                //playerPosition = item.transform.position;
                this.playerLastSeen = item.transform.position;
                this.Alert(playerLastSeen);
                playerLastSeenCounter = playerLastSeenDuration;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        if (visibleTargets.Count <= 0)
        {
            canSeePlayer = false;
        }

        //if (canSeePlayer)
        //{
        //    this.playerLastSeen = playerPosition;
        //    this.Alert(playerPosition);
        //}
        //else
        //{

        //}
        return canSeePlayer;
    }

    public bool HasLosToPlayer()
    {
        //var target = player.position;
        Vector3 target = player.position;
        Vector3 dirToTarget = (target - transform.position).normalized;
        //if (Vector3.Angle(transform.forward, dirToTarget) < 1.0f)
        //{
        float dstToTarget = Vector3.Distance(transform.position, target);
        if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
        {
            //hasLosToPlayer = true;
            hasLosToPlayer = true;
            if (playerLastSeen != defaultLastSeen)
            {
                this.playerLastSeen = player.position;
                //playerLastSeenCounter = playerLastSeenDuration;
                this.Alert(playerLastSeen);
            }

        }
        else
        {
            hasLosToPlayer = false;
        }
        return hasLosToPlayer;
        //}
        // else
        //{
        //   hasLosToPlayer = false;
        //}
    }

    public bool FacingPlayer()
    {
        //var target = player.position;
        Vector3 target = player.position;
        Vector3 dirToTarget = (target - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < 10.0f)
        {
            float dstToTarget = Vector3.Distance(transform.position, target);
            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            {
                //hasLosToPlayer = true;
                facingPlayer = true;
                if (playerLastSeen != defaultLastSeen)
                {
                    this.playerLastSeen = player.position;
                    playerLastSeenCounter = playerLastSeenDuration;
                    this.Alert(playerLastSeen);
                }

            }
            else
            {
                facingPlayer = false;
            }

        }
        else
        {
            facingPlayer = false;
        }

        return facingPlayer;
    }

    private void LateUpdate()
    {
        if (stopNow)
        {
            viewMeshFilter.gameObject.SetActive(false);
            return;
        }

        DrawFieldOfView();
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < fieldOfView / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target.gameObject);


                }
            }

        }
    }

    void FindAlliesAndHidingSpot()
    {
        allies.Clear();
        //hidingSpots.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, alertRadius, allyAndHideMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {

            Transform target = targetsInViewRadius[i].transform;
            //Vector3 dirToTarget = (target.position - transform.position).normalized;
            //if (Vector3.Angle(transform.forward, dirToTarget) < fieldOfView / 2)
            //{
            //float dstToTarget = Vector3.Distance(transform.position, target.position);
            //if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            //{
            //allies.Add(target.gameObject);
            //}
            //}


            if (target.CompareTag(allyTag))
            {
                allies.Add(target.gameObject);
            }
            else if (target.CompareTag(hideSpotTag))
            {
               // hidingSpots.Add(target.gameObject);
            }
            


        }
    }


    void FindTargetAlliesAndHidingSpot()
    {
        allies.Clear();
        //hidingSpots.Clear();
        visibleTargets.Clear();

        canSeePlayer = false;
        hasLosToPlayer = false;
        facingPlayer = false;

        float dstToTarget = Vector3.Distance(transform.position, player.transform.position);
        distanceBetweenPlayer = dstToTarget;
        //Transform target = targetsInViewRadius[i].transform;
        Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
        if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
        {
            hasLosToPlayer = true;
            if (playerLastSeen != defaultLastSeen)
            {
                //this.playerLastSeen = player.position;
                //playerLastSeenCounter = playerLastSeenDuration;
                //this.Alert(playerLastSeen);

                //globalLastSeen = player.position;
                //var globalLastSeenVar = (SharedVector3)GlobalVariables.Instance.GetVariable("GlobalLastSeen");
                //globalLastSeenVar.SetValue(globalLastSeen);
            }
        }
        else
        {
            hasLosToPlayer = false;
        }
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, alertRadius, allyAndHideMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {

            Transform target = targetsInViewRadius[i].transform;
            //Vector3 dirToTarget = (target.position - transform.position).normalized;
            //if (Vector3.Angle(transform.forward, dirToTarget) < fieldOfView / 2)
            //{
            //float dstToTarget = Vector3.Distance(transform.position, target.position);
            //if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            //{
            //allies.Add(target.gameObject);
            //}
            //}


            if (target.CompareTag(allyTag))
            {
                allies.Add(target.gameObject);
            }
            else if (target.CompareTag(hideSpotTag))
            {
               // hidingSpots.Add(target.gameObject);
            }
            else if (target.transform == player.transform)
            {

                float dstToTarget2 = Vector3.Distance(transform.position, target.position);
                //Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget2 = (target.position - transform.position).normalized;
                if (hasLosToPlayer)
                {
                    //hasLosToPlayer = true;
                    //if (playerLastSeen != defaultLastSeen)
                    //{
                    //    this.playerLastSeen = player.position;
                    //    //playerLastSeenCounter = playerLastSeenDuration;
                    //    this.Alert(playerLastSeen);
                    //}
                    //Transform target = targetsInViewRadius[i].transform;
                    if (dstToTarget2 < viewRadius)
                    {
                        if(checkForPlayerWalking)
                        {
                            if ((Vector3.Angle(transform.forward, dirToTarget2) < fieldOfView / 2))
                            {
                                targetCharacter.canBeSeen = true;
                            }
                            else
                            {
                                targetCharacter.canBeSeen = false;
                            }
                            if ((Vector3.Angle(transform.forward, dirToTarget2) < fieldOfView / 2) && targetCharacter.walking) //if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                            {
                                targetCharacter.isSeen = true;
                                facingPlayer = true;
                                visibleTargets.Add(target.gameObject);
                                canSeePlayer = true;
                                //playerPosition = item.transform.position;
                                this.playerLastSeen = target.transform.position;
                                this.Alert(playerLastSeen);
                                playerLastSeenCounter = playerLastSeenDuration;
                                privateLastSeen = target.transform.position;
                                //var globalLastSeenVar = (SharedVector3)GlobalVariables.Instance.GetVariable("GlobalLastSeen");
                                //globalLastSeenVar.SetValue(privateLastSeen);



                            }

                            else
                            {
                                targetCharacter.isSeen = false;
                                canSeePlayer = false;
                                facingPlayer = false;
                            }
                        }
                        else
                        {
                            if ((Vector3.Angle(transform.forward, dirToTarget2) < fieldOfView / 2)) //if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                            {
                                targetCharacter.isSeen = true;
                                facingPlayer = true;
                                visibleTargets.Add(target.gameObject);
                                canSeePlayer = true;
                                //playerPosition = item.transform.position;
                                this.playerLastSeen = target.transform.position;
                                this.Alert(playerLastSeen);
                                playerLastSeenCounter = playerLastSeenDuration;
                                privateLastSeen = target.transform.position;
                                //var globalLastSeenVar = (SharedVector3)GlobalVariables.Instance.GetVariable("GlobalLastSeen");
                                //globalLastSeenVar.SetValue(privateLastSeen);

                            }

                            else
                            {
                                targetCharacter.isSeen = false;
                                canSeePlayer = false;
                                facingPlayer = false;
                            }
                        }

                        
                    }
                    else
                    {
                        //not close enough
                    }
                }
                //else
                //{
                //    hasLosToPlayer = false;
                //}
            }

            
        }
    }


    public void Alert(Vector3 playerLocation)
    {
        foreach (var item in allies)
        {
            item.GetComponent<EnemyAwareness>().playerLastSeen = playerLocation;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        var triggerType = other.GetComponent<TriggerType>();
        if (triggerType != null && triggerType.collidesWithVision && !colliders.Contains(other))
        {
            colliders.Add(other);
            colliders.RemoveAll((c) => c == null);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (colliders.Contains(other))
        {
            colliders.Remove(other);
            colliders.RemoveAll((c) => c == null);
        }
    }

    void UpdateVisibility()
    {
        visibles.Clear();
        foreach (var c in colliders)
        {
            if (c == null)
                continue;

            var go = c.attachedRigidbody != null ? c.attachedRigidbody.gameObject : null;

            bool isVisible = false;

            if (go != null)
            {

                float angle = Vector3.Angle(this.transform.forward, go.transform.position - this.transform.position);

                //bool isInClosedField = Vector3.Distance(go.transform.position, this.transform.position) <= closeFieldDistance;
                bool isInFieldOfView = Mathf.Abs(angle) <= fieldOfView * 0.5f;


                isVisible = (isInFieldOfView && HasLoS(go.gameObject));

            }

            if (isVisible && !visibles.Contains(go))
            {
                //var bullet = go.GetComponent<Bullet>();
                //if (bullet != null)
                //{
                //	var attacker = bullet.shooter != null ? bullet.shooter.GetComponent<Unit>() : null;
                //	if (attacker != null && attacker.team != shooter.team)
                //		lastBulletSeenTime = Time.time;
                //}
                visibles.Add(go);
            }

        }
    }

    bool HasLoS(GameObject target) // Line of sight test.
    {
        bool has = false;
        var targetDirection = (target.transform.position - this.transform.position).normalized;

        Ray ray = new Ray(this.transform.position, targetDirection);
        var hits = Physics.RaycastAll(ray, float.PositiveInfinity);

        float minD = float.PositiveInfinity;
        GameObject closest = null;

        foreach (var h in hits)
        {
            var ct = h.collider.GetComponent<TriggerType>();
            if (ct == null || !ct.collidesWithVision)
                continue;

            float d = Vector3.Distance(h.point, this.transform.position);
            var o = h.collider.attachedRigidbody != null ? h.collider.attachedRigidbody.gameObject : h.collider.gameObject;
            if (d <= minD && o != this.gameObject)
            {
                minD = d;
                closest = o;
            }
        }

        has = closest == target;

        return has;
    }
}
