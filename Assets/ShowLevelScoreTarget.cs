using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.GameStructure;
using UnityEngine.UI;
public class ShowLevelScoreTarget : MonoBehaviour
{
    public Text textToUpdate;
    // Start is called before the firsß frame update
    void Start()
    {
        textToUpdate.text = LevelGenerator.Instance.enemyCount.ToString(); // GameManager.Instance.Levels.Selected.ScoreTarget.ToString();

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
