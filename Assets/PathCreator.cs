using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using GameFramework.GameStructure.GameItems.Components.AbstractClasses;
using GameFramework.GameStructure.Characters.ObjectModel;
using UnityEngine;
using GameFramework.GameStructure;
using GameFramework.GameStructure.GameItems.ObjectModel;
using garagekitgames;
using GameManager = GameFramework.GameStructure.GameManager;
using UnityEngine.Events;

public enum PathType
{
    Yellow,
    Red,
    Green,
    Blue,
    White
}
public class PathCreator : MonoBehaviour
{
    public LayerMask raycastLayermask;
   // public LayerMask startLayermask;
   // public LayerMask endLayermask;
    private LineRenderer lineRenderer;

    private List<Vector3> points = new List<Vector3>();

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };

    public float pathResolution = 1f;

    public bool startPath;

    public bool pathDone;

    public Transform destination;

    public Character myCharacter;

    public GameObject myCharacterGo;

    public CharacterThinker character;
    public PathMover characterMover;

    public PathType pathType;

    public UnityEvent OnPathStarted;
    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        myCharacter = GameManager.Instance.Characters.Selected;
        myCharacterGo = myCharacter.InstantiatePrefab(GameItem.LocalisablePrefabType.InGame, null, null, false);

        
        character = myCharacterGo.GetComponent<CharacterThinker>();
        character.bpHolder.BodyPartsName["hip"].BodyPartTransform.position = this.transform.position;
        characterMover = myCharacterGo.GetComponent<PathMover>();
        characterMover.myPath = this;

    }

    // Update is called once per frame
    void Update()
    {
        CheckInput(); 
    }

    public void ResetPlayer()
    {
        characterMover.OnResetPlayer.Invoke();
        characterMover.hipPart.position = this.transform.position;
        characterMover.StopMove();

    }
    void CheckInput()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            points.Clear();

            Ray mouseRay = GenerateMouseRay();
            RaycastHit hit;

            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100, raycastLayermask))
            {
                if(hit.transform == this.transform)
                {
                    OnPathStarted.Invoke();
                    startPath = true;
                    //characterMover.OnResetPlayer.Invoke();
                    //characterMover.
                    characterMover.OnResetPlayer.Invoke();
                    characterMover.hipPart.position = this.transform.position;
                    pathDone = false;
                }
                else
                {
                    startPath = false;
                }
                
            }
            else
            {

                startPath = false;
                //return;
            }

        }

        if (UnityEngine.Input.GetMouseButton(0))
        {
            Ray mouseRay = GenerateMouseRay();
            RaycastHit hit;

            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit, 100, raycastLayermask) && startPath  && !pathDone)
            {
                if(DistanceToLastPoint(hit.point) > pathResolution)
                {
                    points.Add(hit.point);

                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());


                    Ray mouseRay2 = GenerateMouseRay();
                    RaycastHit hit2;

                    if (Physics.Raycast(mouseRay2.origin, mouseRay2.direction, out hit2, 100, raycastLayermask))
                    {
                        if(hit.transform == destination)
                        {
                            pathDone = true;
                            OnNewPathCreated(points);
                        }
                        else
                        {

                        }

                        
                        
                        //return;
                    }
                    else
                    {
                        //pathDone = false;
                    }

                    //if(hit.point)
                }
            }
        }
        else if(UnityEngine.Input.GetMouseButtonUp(0))
        {
            if(pathDone)
            {
                // OnNewPathCreated(points);
                Debug.Log("PathDone");
            }
            else
            {
                points.Clear();
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPositions(points.ToArray());
            }

            //pathDone = false;
            //startPath = false;

        }
    }

    public void UndoPath()
    {
        
            points.Clear();
        pathDone = false;
        lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        //characterMover.currentWaypoint = 0;
        //characterMover.path.Clear();
        
        //currentWaypoint = 0;

    }
    private float DistanceToLastPoint(Vector3 point)
    {
        if(!points.Any())
        {
            return Mathf.Infinity;
        }
        return Vector3.Distance(points.Last(), point);

    }

    Ray GenerateMouseRay()
    {
        Vector3 mousePosFar = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 mousePosFarW = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosNearW = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mouseRay = new Ray(mousePosNearW, mousePosFarW - mousePosNearW);

        return mouseRay;



    }
}
