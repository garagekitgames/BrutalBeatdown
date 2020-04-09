using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GemCollection : MonoBehaviour
{
    private string playerTag = "Box";
    public UnityEvent onGemCollected;
    public GameObject gemPrefab;

    public Transform targetCanvas;
    //public GameObject[] uiStars;
    public RectTransform gemIcon;

    public float animDuration = 10f;
    public Ease animEase = Ease.Linear;
    // Start is called before the first frame update
    void Start()
    {
        targetCanvas = GameObject.FindGameObjectWithTag("InGameMenu").transform;
        gemIcon = GameObject.FindGameObjectWithTag("UIGem").GetComponent<RectTransform>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(playerTag))
        {
            onGemCollected.Invoke();
            ExecutePickup();
            //particle effects
            Destroy(this.gameObject);
        }

    }

    public void ExecutePickup()
    {
        //LevelManager.Instance.Level.StarWon(1, true);
        RectTransform clone = Instantiate(gemPrefab, targetCanvas, false).GetComponent<RectTransform>();
        clone.anchorMin = Camera.main.WorldToViewportPoint(this.transform.position);
        clone.anchorMax = clone.anchorMin;

        //clone.anchoredPosition = clone.localPosition;

        //clone.anchorMin = new Vector2(0.5f, 0.5f);
        //clone.anchorMax = clone.anchorMax;
        clone.SetParent(gemIcon);
        clone.DOAnchorPos(Vector3.zero, animDuration).SetEase(animEase).Play();
        clone.DOAnchorMax(new Vector2(0.5f, 0.5f), animDuration).SetEase(animEase).Play();
        clone.DOAnchorMin(new Vector2(0.5f, 0.5f), animDuration).SetEase(animEase).Play();
        //clone.anchorMin = new Vector2(0.5f, 0.5f);
        //clone.anchorMax = clone.anchorMax;
        //OnStarOneCollect.Invoke();
        Debug.Log("CollectedGem");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
