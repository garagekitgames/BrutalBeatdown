using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOPunchScale(new Vector3(2, 2, 2), 1, 10, 1).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
  
    }
}
