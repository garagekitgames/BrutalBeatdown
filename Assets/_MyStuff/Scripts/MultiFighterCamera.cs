using UnityEngine;
using System.Collections;
using garagekitgames;
using System.Linq;
public class MultiFighterCamera : MonoBehaviour
{
    public float updateFrequency = 0.2f;

    [SerializeField]
    private Vector3 offset2 = new Vector3(0,6.3f,-5);

    private Vector3 lastOffset;

    public Vector3 targetsCenter;

    public float minZoom = 10f;
    public float maxZoom = 10f;

    public float zoom = 1f;

    public Transform[] targets;
    public Transform player;

    private Transform thisTransform;

    private Vector3 lastLookAtPosition;

    private Vector3 velocity1;

    private Vector3 velocity2;

    public float smoothTime = 1f;

    public bool orbit;

    public float orbitSpeed = 6f;

    
    public bool lookAtTarget = true;

    public bool dontTrackX = true;

    private bool toggle;

    //public GameObject controlsScreen;

    public Vector3 lookAtPosition;

    public Vector3 cameraPosition;

    public bool initialized;

    public Collider bounds;

    [MinMaxRange(-100f, 100f)]
    public RangedFloat xBound;

    [MinMaxRange(-100f, 100f)]
    public RangedFloat zBound;

    [MinMaxRange(-100f, 100f)]
    public RangedFloat yBound;

    public Transform enemyTarget;

    public bool trackExit;

    public bool trackEnemy;

    public Transform exit;

    private void Start()
    {
        
        this.thisTransform = base.transform;
        this.RefreshCameraTargets();
        if (this.targets.Length > 0)
        {
            this.UpdateCenter();
        }
        this.lastLookAtPosition = this.targetsCenter;
    }

    private void LateUpdate()
    {
        
            this.UpdateCamera();
       
    }

    private void UpdateCamera()
    {
        this.RefreshCameraTargets();
        if (this.targets.Length > 0)
        {
            this.UpdateCenter();
            this.UpdateZoom();
            if (this.initialized)
            {
                this.UpdatePosition();
            }
            else
            {
                this.SetPosition();
            }
        }
    }

    private void RefreshCameraTargets()
    {
        player = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
        GameObject[] array = GameObject.FindGameObjectsWithTag("CamTarget");
        GameObject[] array1 = GameObject.FindGameObjectsWithTag("CamTarget_Enemy");
        exit = GameObject.FindGameObjectWithTag("Exit").transform;
        this.targets = new Transform[0];
        bool flag = false;
        /*GameObject[] array2 = array;
        for (int i = 0; i < array2.Length; i++)
        {
            GameObject gameObject = array2[i];
            Actor component = gameObject.transform.root.GetComponent<Actor>();
            if (component != null && component.isLocalPlayer)
            {
                this.targets = new Transform[1];
                this.targets[0] = gameObject.transform;
                flag = true;
            }
        }*/


        var enemy = array1.Where(t => t.transform.root.GetComponent<CharacterHealth>().alive == true).OrderBy(t => Vector3.Distance(player.position, t.transform.position)).FirstOrDefault();
        if(enemy != null)
        {
            enemyTarget = enemy.transform;
        }
        else
        {
            enemyTarget = null;
        }

        if (trackExit)
        {
            if (enemy != null && trackEnemy)
            {
                this.targets = new Transform[3];

                this.targets[0] = player;
                this.targets[1] = enemy.transform;
                this.targets[2] = exit;
            }
            else
            {
                this.targets = new Transform[2];

                this.targets[0] = player;
                this.targets[1] = exit;
                //this.targets[1] = enemy.transform;
            }
        }
        else
        {
            if (enemy != null && trackEnemy)
            {
                this.targets = new Transform[2];

                this.targets[0] = player;
                this.targets[1] = enemy.transform;
            }
            else
            {
                this.targets = new Transform[1];

                this.targets[0] = player;
                //this.targets[1] = enemy.transform;
            }
        }




        //if (!flag && array.Length > 0)
        //{
        //    this.targets = new Transform[array.Length];
        //    for (int j = 0; j < array.Length; j++)
        //    {
        //        this.targets[j] = array[j].transform;
        //    }
        //}
    }

    private void UpdateCenter()
    {
        this.targetsCenter = Vector3.zero;
        if (this.targets.Length > 1)
        {
            for (int i = 0; i < this.targets.Length; i++)
            {
                this.targetsCenter += this.targets[i].position;
            }
            this.targetsCenter /= (float)this.targets.Length;
        }
        else
        {
            this.targetsCenter = this.targets[0].position;
        }
    }

    private void UpdateZoom()
    {
        float num = 1f;
        float num2 = 1f;
        if (this.targets.Length > 1)
        {
            for (int i = 0; i < this.targets.Length; i++)
            {
                num2 = Vector3.Distance(this.targets[i].position, this.targetsCenter);
                if (num2 > num)
                {
                    num = num2;
                }
            }
        }
        this.zoom = Mathf.Clamp(num2 / 5f, this.minZoom, this.maxZoom);
    }

    private void UpdatePosition()
    {
        Vector3 a = Vector3.Lerp(this.lastOffset, this.offset2, this.smoothTime);
        Vector3 target = Vector3.zero;
        //Vector3 newTarget = 
        if(dontTrackX)
        {
            target = this.targetsCenter + a * this.zoom;
            target = new Vector3(Mathf.Clamp(target.x, xBound.minValue, xBound.maxValue), target.y, Mathf.Clamp(target.z, zBound.minValue, zBound.maxValue));
        }
        else
        {
             target = this.targetsCenter + a * this.zoom;
            target = new Vector3(Mathf.Clamp(target.x, xBound.minValue, xBound.maxValue), target.y, Mathf.Clamp(target.z, yBound.minValue, yBound.maxValue));
        }
        this.lookAtPosition = Vector3.SmoothDamp(this.lastLookAtPosition, this.targetsCenter, ref this.velocity1, this.smoothTime);
        this.cameraPosition = Vector3.SmoothDamp(this.thisTransform.position, target, ref this.velocity2, this.smoothTime);
        if (!this.orbit)
        {
            this.thisTransform.position = this.cameraPosition;
        }
        else
        {
            this.thisTransform.RotateAround(this.lookAtPosition, Vector3.up, this.orbitSpeed * Time.deltaTime);
            Vector3 position = new Vector3(this.thisTransform.position.x, this.cameraPosition.y, this.thisTransform.position.z);
            this.thisTransform.position = position;
        }
        if(lookAtTarget)
        {
            base.transform.LookAt(this.lookAtPosition);
            this.lastLookAtPosition = this.lookAtPosition;
        }

        this.lastOffset = a;
    }

    private void SetPosition()
    {
        Vector3 a = this.offset2;
        Vector3 vector = this.targetsCenter + a * this.zoom;
        this.lookAtPosition = this.targetsCenter;
        this.cameraPosition = vector;
        if (!this.orbit)
        {
            this.thisTransform.position = this.cameraPosition;
        }
        else
        {
            this.thisTransform.RotateAround(this.lookAtPosition, Vector3.up, this.orbitSpeed);
            Vector3 position = new Vector3(this.thisTransform.position.x, this.cameraPosition.y, this.thisTransform.position.z);
            this.thisTransform.position = position;
        }
        if(lookAtTarget)
        {
            base.transform.LookAt(this.lookAtPosition);
            this.lastLookAtPosition = this.lookAtPosition;
        }

        this.lastOffset = a;
        this.initialized = true;
    }
}

