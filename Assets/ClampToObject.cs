using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClampToObject : MonoBehaviour
{

    public RectTransform rectTransform;

    public Transform targetObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 imagePos = Camera.main.WorldToScreenPoint(targetObject.position);

        rectTransform.position = imagePos;
    }
}
