using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Localisation.ObjectModel;
using GameFramework.Preferences;
using GameFramework.GameObjects.Components;
using GameFramework.GameStructure.Characters.ObjectModel;
using GameFramework.GameStructure.GameItems.ObjectModel;
using GameFramework.GameStructure.Levels.ObjectModel;
using GameFramework.GameStructure.Players.ObjectModel;
using GameFramework.GameStructure.Worlds.ObjectModel;
using GameFramework.GameStructure.Variables.ObjectModel;
using GameFramework.GameStructure;

public class TestLevelVariableReader : MonoBehaviour
{

    public int testValue = 101;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("LoadPrefs", 1);
        testValue = GameManager.Instance.Levels.Selected.Variables.GetInt("test").Value;
        Debug.Log("Test : "+GameManager.Instance.Levels.Selected.Variables.GetInt("test").Value);

        //Invoke("ChangeValue", 2);
        //Invoke("UpdatePrefs", 10);
        

    }



    public void ChangeValue()
    {
        GameManager.Instance.Levels.Selected.Variables.GetInt("test").Value = 100;
    }

    public void UpdatePrefs()
    {
        GameManager.Instance.Levels.Selected.UpdatePlayerPrefs();
        Debug.Log("Test : " + GameManager.Instance.Levels.Selected.Variables.GetInt("test").Value);
    }


    // Update is called once per frame
    void Update()
    {

        GameManager.Instance.Levels.Selected.Variables.GetInt("test").Value = testValue;
        Debug.Log("Test : " + GameManager.Instance.Levels.Selected.Variables.GetInt("test").Value);


    }
}
