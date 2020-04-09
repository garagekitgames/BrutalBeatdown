using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using garagekitgames;
using UnityEngine.Events;
//using GameFramework.GameStructure.Levels.ObjectModel;
using GameFramework.GameStructure.Levels;
using DG.Tweening;

public class StarCollection : MonoBehaviour
{
    private string playerTag = "Box";
    public UnityEvent onStarCollected;
    public Transform targetCanvas;
    public GameObject[] uiStars;
    public RectTransform star1Icon;
    public RectTransform star2Icon;
    public RectTransform star3Icon;
    public GameObject starPrefab;

    public Camera mainCamera; 

    public float animDuration = 10f;
    public Ease animEase = Ease.Linear;

    public UnityEvent OnStarOneCollect;
    public UnityEvent OnStarTwoCollect;
    public UnityEvent OnStarThreeCollect;

    // Start is called before the first frame update
    void Start()
    {
        uiStars = GameObject.FindGameObjectsWithTag("UIStar");

        star1Icon = uiStars[0].GetComponentInChildren<RectTransform>();
        star2Icon = uiStars[1].GetComponentInChildren<RectTransform>();
        star3Icon = uiStars[2].GetComponentInChildren<RectTransform>();

        mainCamera = Camera.main;
    }


    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.CompareTag(playerTag))
            {
                onStarCollected.Invoke();
            ExecutePickup();
                //particle effects
                Destroy(this.gameObject);
            }
        
    }

    public void OneStar()
    {
        LevelManager.Instance.Level.StarWon(1, true);
    }

    public void TwoStar()
    {
        LevelManager.Instance.Level.StarWon(2, true);
    }

    public void ThreeStar()
    {
        LevelManager.Instance.Level.StarWon(3, true);
    }

    public void ExecutePickup()
    {
        int collectedStars = LevelManager.Instance.collectedStars;

        

        //clone.DOAnchorPos()

        if (collectedStars == 1)
        {
            //LevelManager.Instance.Level.StarWon(1, true);
            RectTransform clone = Instantiate(starPrefab, targetCanvas, false).GetComponent<RectTransform>();
            clone.anchorMin = mainCamera.WorldToViewportPoint(this.transform.position);
            clone.anchorMax = clone.anchorMin;

            //clone.anchoredPosition = clone.localPosition;

            //clone.anchorMin = new Vector2(0.5f, 0.5f);
            //clone.anchorMax = clone.anchorMax;
            clone.SetParent(star1Icon);
            clone.DOAnchorPos(Vector3.zero, animDuration).SetEase(animEase).Play().OnComplete(OneStar);
            clone.DOAnchorMax(new Vector2(0.5f, 0.5f), animDuration).SetEase(animEase).Play();
            clone.DOAnchorMin(new Vector2(0.5f, 0.5f), animDuration).SetEase(animEase).Play();
            //clone.anchorMin = new Vector2(0.5f, 0.5f);
            //clone.anchorMax = clone.anchorMax;
            OnStarOneCollect.Invoke();
            Debug.Log("CollectedFirstStar");
        }

        if (collectedStars == 2)
        {
            //LevelManager.Instance.Level.StarWon(2, true);
            RectTransform clone = Instantiate(starPrefab, targetCanvas, false).GetComponent<RectTransform>();
            clone.anchorMin = mainCamera.WorldToViewportPoint(this.transform.position);
            clone.anchorMax = clone.anchorMin;

            //clone.anchoredPosition = clone.localPosition;

            //clone.anchorMin = new Vector2(0.5f, 0.5f);
            //clone.anchorMax = clone.anchorMax;
            clone.SetParent(star2Icon);
            clone.anchorMin = new Vector2(0.5f, 0.5f);
            clone.anchorMax = clone.anchorMax;
            clone.DOAnchorPos(Vector3.zero, animDuration).SetEase(animEase).Play().OnComplete(TwoStar);
            clone.DOAnchorMax(new Vector2(0.5f, 0.5f), animDuration).SetEase(animEase).Play();
            clone.DOAnchorMin(new Vector2(0.5f, 0.5f), animDuration).SetEase(animEase).Play();

            OnStarTwoCollect.Invoke();
            Debug.Log("CollectedStar2");
        }

        if (collectedStars == 3)
        {
            //LevelManager.Instance.Level.StarWon(3, true);
            RectTransform clone = Instantiate(starPrefab, targetCanvas, false).GetComponent<RectTransform>();
            clone.anchorMin = mainCamera.WorldToViewportPoint(this.transform.position);
            clone.anchorMax = clone.anchorMin;

            //clone.anchoredPosition = clone.localPosition;

            //clone.anchorMin = new Vector2(0.5f, 0.5f);
            //clone.anchorMax = clone.anchorMax;
            clone.SetParent(star3Icon);
            //clone.anchorMin = new Vector2(0.5f, 0.5f);
            clone.anchorMax = clone.anchorMax;
            clone.DOAnchorPos(Vector3.zero, animDuration).SetEase(animEase).Play().OnComplete(ThreeStar);
            clone.DOAnchorMax(new Vector2(0.5f, 0.5f), animDuration).SetEase(animEase).Play();
            clone.DOAnchorMin(new Vector2(0.5f, 0.5f), animDuration).SetEase(animEase).Play();

            OnStarThreeCollect.Invoke();
            Debug.Log("CollectedStar3");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
