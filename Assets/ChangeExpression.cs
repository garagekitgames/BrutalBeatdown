using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeExpression : MonoBehaviour
{
	public SkinnedMeshRenderer skinnedMeshRenderer;
	public Mesh skinnedMesh;
    int blendShapeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
		skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
		blendShapeCount = skinnedMesh.blendShapeCount;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeath()
	{
		if (blendShapeCount > 0)
		{
			skinnedMeshRenderer.SetBlendShapeWeight(0, 100);
		}
	}
}
