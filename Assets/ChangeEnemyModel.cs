using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using SO;
using GameFramework.GameStructure;

public class ChangeEnemyModel : MonoBehaviour
{
    public SkinnedMeshRenderer myMeshRenderer;
    public Mesh[] enemyMeshes;
    // Start is called before the first frame update
    void Start()
    {
        myMeshRenderer = this.transform.Find("Cylinder_000").GetComponent<SkinnedMeshRenderer>();
        Random.InitState(GameFramework.GameStructure.GameManager.Instance.Levels.Selected.Number);

        //wallMaterial.color = wallColors[Random.Range(0, wallColors.Length)];// new Color(Random.value, Random.value, Random.value, 1.0f);

        myMeshRenderer.sharedMesh = enemyMeshes[Random.Range(0, enemyMeshes.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
