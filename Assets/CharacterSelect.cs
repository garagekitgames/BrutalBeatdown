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
using SO;
using UnityEngine.Events;

public class CharacterSelect : MonoBehaviour
{
    public Character currentCharacter;
    public Character[] characters;
    public CharacterGameItemManager characterManager;
    //public int
    // Start is called before the first frame update
    void Start()
    {
        characterManager = GameManager.Instance.Characters;
        characters = characterManager.Items;
        currentCharacter = characterManager.Selected;
    }

    public void ToggleLeft()
    {
        characterManager.SelectPrevious();
        //selectedIndex.value--;
        //if (selectedIndex.value < 0)
        //{
        //    selectedIndex.value = levelCharacters.Count - 1;
        //}
        //UpdateSelectedLevel(selectedIndex.value);
        //Call Update UI
        //Update the camera target
        //set the selected character // set currentLevel = selectedLevel
    }

    public void ToggleRight()
    {
        characterManager.SelectNext();
        
        //selectedIndex.value++;
        //if (selectedIndex.value == levelCharacters.Count)
        //{
        //    selectedIndex.value = 0;
        //}
        //UpdateSelectedLevel(selectedIndex.value);
        //Call Update UI
        //Update the camera target
        //set the selected character // set currentLevel = selectedLevel
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
