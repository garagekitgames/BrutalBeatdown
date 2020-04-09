using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.Events;
using System;

namespace garagekitgames
{
    public class GamePlayManager : UnitySingleton<GamePlayManager>
    {
        ///This Guy Controls
        ///game start, game end, game running, raises events for game start, end and every other event that occurs in the game play
        ///Events : 
        ///1. Game start
        ///2. Game End
        ///3. Player Hit
        ///4. Player Died
        ///5. Enemy Hit
        ///6. Enemy Died
        ///7. Display Pause Menu
        ///8. Go To Main Menu
        public UnityEvent OnGameStart;
        public UnityEvent OnGameRestart;
        public UnityEvent OnGameEnd;
        public UnityEvent OnGamePause;
        public UnityEvent OnGameLaunch;

        public bool gameRestarted;
        public bool gameStarted;
        public bool gamePaused;
        public bool gameEnded;

        public Animator gameplayStateMachine;

        public BoolVariable showTutorialOnStart;

        public List<GameObject> tutorialTarget;
        public BoolVariable tutorialSuccess;
        public TestManager enemyManager;
        // Use this for initialization
        private void Awake()
        {
            
            gameplayStateMachine = this.GetComponent<Animator>();
            tutorialTarget = new List<GameObject>(GameObject.FindGameObjectsWithTag("tutorialTarget"));
            enemyManager = GameObject.FindObjectOfType<TestManager>();
        }


        void Start()
        {

            AudioManager.instance.FadeInCaller("BGM1", 0.1f, 0.3f);
            AudioManager.instance.FadeOutCaller("BGM2", 0.1f);
            OnGameLaunch.Invoke();
            //enemyManager.stopDoingShit = true;
            /*if(tutorialTarget[1] && showTutorialOnStart.value)
            {
                EffectsController.Instance.CreateFloatingTextEffectBasic(tutorialTarget[0].transform.position, "Tap on Target to attack", Color.white, tutorialTarget[0].transform, (Vector3.up * 2.5f), 0.5f);
            }*/

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public BoolVariable IsTutorialSuccess()
        {
            
            return tutorialSuccess;
        }



        public void OnTutorialSuccess()
        {
            OnGameStart.Invoke();
            //enemyManager.stopDoingShit = false;
            /* tutorialSuccess.value = true;

             if (tutorialSuccess.value)
             {
                 EffectsController.Instance.CreateFloatingTextEffectBasic(tutorialTarget[1].transform.position, "Hold the tap longer for a stronger and accurate attack !", Color.white, tutorialTarget[1].transform, (Vector3.up * 2.5f), 0);
                 //Show dialogue box for showing tutorial on start
             }*/
        }

        public void OnGamePaused()
        {
            OnGamePause.Invoke();
            gamePaused = true;
        }

        public void OnPlayerDeath()
        {
            OnGameEnd.Invoke();
            AudioManager.instance.FadeOutCaller("BGM1", 0.1f);
            gameEnded = true;
            //uncomment this
            PersistableSO.Instance.Save();
        }

        public void OnPlayerSpawned()
        {

        }

        public void OnPlayerReSpawned()
        {
            //AudioManager.instance.FadeInCaller("BGM1", 0.1f, 0.3f);
        }

        public void OnEnemyDeath()
        {

        }

        public void OnEnemySpawn()
        {

        }

        public void DisplayPauseMenu()
        {

        }

        public void DisplayGameOverMenu()
        {


        }
    }
}

