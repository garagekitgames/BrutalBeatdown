using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class StartScreenEffects : MonoBehaviour
{
    [SerializeField]
    Text tapToStart;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("AnimateText", 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AnimateText()
    {
        tapToStart.material.DOTiling(Vector2.one, 1f);
    }
}
