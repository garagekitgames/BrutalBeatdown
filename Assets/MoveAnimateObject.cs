using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveAnimateObject : MonoBehaviour
{
	public Ease animEase = Ease.Linear;
	public Vector3 destinationPosition;
    public GameObject target;
	// Start is called before the first frame update
	void Start()
    {
        if(target == null)
        {
            target = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMovePosition()
    {
        target.transform.DOLocalMove(destinationPosition, 1);
    }
}
