using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.GameStructure;
using GameFramework.GameStructure.Worlds.ObjectModel;
using GameFramework.GameObjects.Components;
using GameFramework.GameStructure;
using GameFramework.GameStructure.Levels;
using GameFramework.Localisation;
using GameFramework.Social;
using GameFramework.UI.Other;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using GameFramework.GameStructure.Game;
using GameFramework.GameStructure.Players.Messages;
using GameFramework.Messaging;
using GameFramework.Preferences;
using GameFramework.Billing.Messages;
using GameFramework.UI.Dialogs.Components;

public class SetContinueSceneName : MonoBehaviour
{
    GameOver gameOver;
    public bool worldUnlocked;
    public bool nextLevelUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Messenger.AddListener<LevelPurchasedMessage>(OnLevelPurchased);
        gameOver = FindObjectOfType<GameOver>();
        //GameManager.Instance.Worlds.Unlocked += OnWorldUnlocked;


    }

    bool OnLevelPurchased (BaseMessage message)
    {
        var levelPurchaseMessage = message as LevelPurchasedMessage;

        return true;
    }

    public void OnWorldUnlocked(World world)
    {
        if (GameManager.Instance.Worlds.GetNextItem().Number == world.Number)
        {
            worldUnlocked = true;
            gameOver.ContinueScene = "World";
        }
    }

    public void OnGameWon()
    {
        if (GameManager.Instance.Levels.GetNextItem() != null)
        {
            if (GameManager.Instance.Levels.GetNextItem().IsUnlocked)
            {
                nextLevelUnlocked = true;
                gameOver.ContinueScene = "Game_";
                GameManager.Instance.Levels.SetSelected(GameManager.Instance.Levels.GetNextItem());
            }
        }
    }

    public void OnGameLost()
    {
        if (GameManager.Instance.Levels.GetNextItem() != null)
        {
            if (GameManager.Instance.Levels.GetNextItem().IsUnlocked)
            {
                nextLevelUnlocked = true;
                gameOver.ContinueScene = "Game_";
                GameManager.Instance.Levels.SetSelected(GameManager.Instance.Levels.GetNextItem());
            }
            else
            {
                nextLevelUnlocked = false;
                gameOver.ContinueScene = "Game_";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.Levels.GetNextItem() != null)
        //{
        //    if (GameManager.Instance.Levels.GetNextItem().IsUnlocked)
        //    {
        //        nextLevelUnlocked = true;
        //        gameOver.ContinueScene = "Game_";
        //        GameManager.Instance.Levels.SetSelected(GameManager.Instance.Levels.GetNextItem());
        //        //GameManager.Instance.Characters.GetNextItem().un
        //    }
        //    else
        //    {
        //        nextLevelUnlocked = false;
        //        gameOver.ContinueScene = "Game_";
        //        //GameManager.Instance.Levels.SetSelected(GameManager.Instance.Levels.GetNextItem());
        //    }
        //}

        //if(GameManager.Instance.Worlds.GetNextItem() != null)
        //{
        //    if (GameManager.Instance.Worlds.GetNextItem().IsUnlocked)
        //    {
        //        worldUnlocked = true;
        //        gameOver.ContinueScene = "World";
        //        //GameManager.Instance.Characters.GetNextItem().un
        //    }
        //}

    }
}
