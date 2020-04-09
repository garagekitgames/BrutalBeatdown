using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.Events;

namespace garagekitgames
{
    public class PlayerManager : MonoBehaviour
    {

        public LevelData levelData;
        public GameObject Player;
        public Vector3Variable playerSpawnPoint;
        public CharacterRuntimeSet mainPlayerCharacterSet;
        public CharacterThinker currentPlayer;


        private IEnumerator coroutine;

        public UnityEvent afterDeathEvent;

        public IntVariable cashValue;

        public IntVariable resurrectionCost;

        public UnityEvent updateCashUI;
        public UnityEvent notEnoughCash;
        // public bool destroy = false;

        private void Awake()
        {
            Player = levelData.Player;
            playerSpawnPoint = levelData.spawnPoint;
            Spawn();
        }
        // Use this for initialization
        void Start()
        {
            


        }



        public void SetStopDoingShit(bool value)
        {
            //this.stopDoingShit = value;
            //foreach (var enemy in RuntimeSet.Items)
            // {
            currentPlayer.SetStopDoingShit(value);
                //var AI = enemy.GetComponent<CharacterBasicAI>();
               // AI.SetStopDoingShit(value);
            //}
        }

        void Spawn()
        {
            // If the player has no health left...
            /*if (mainPlayerHealth.currentHP <= 0f)
            {
                // ... exit the function.
                return;
            }*/

            // Find a random index between zero and one less than the number of spawn points.
            //int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            //int enemyIndex = Random.Range(0, enemy.Length);

            //objectPools[enemyIndex].TryGetNextObject(spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
            //GameObject objectthatwasSpawned = pools[enemyIndex].InstantiateFromPool(spawnPoints[spawnPointIndex].transform.position);
            //poolArray[enemyIndex]
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(Player, playerSpawnPoint.value, Quaternion.identity);

            foreach (var character in mainPlayerCharacterSet.Items)
            {
                currentPlayer = character;
                //enemy.SetStopDoingShit(value);
               // var AI = enemy.GetComponent<CharacterBasicAI>();
               // AI.SetStopDoingShit(value);
            }
            //print("Spawned : " + objectthatwasSpawned.name+" | "+ " From Pool : "+ pools[enemyIndex].pooledItems.Count + " | " + " Pools Size : " + pools.Count);
            //spawnCounter = 0;
        }

        public void RespawnCurrentPlayer()
        {
            //currentPlayer.gameObject.SetActive(false);
            if(cashValue.value >= resurrectionCost.value)
            {
                cashValue.value = cashValue.value - resurrectionCost.value;
                updateCashUI.Invoke();
                PersistableSO.Instance.Save();
            }
            else
            {
                //Display other cash buy options
               // notEnoughCash.Invoke();
            }
            
            DisableEnableAfter(0.1f, false);
            ResurrectAfter(0.1f);
            DisableEnableAfter(0.1f, true);
            //currentPlayer.gameObject.SetActive(true);
        }

        public void DisableEnableAfter(float sec, bool value)
        {
            coroutine = LateCall(value, sec);
            StartCoroutine(coroutine);
        }

        public void ResurrectAfter(float sec)
        {
            coroutine = LateCallResurrectEffect(sec);
            StartCoroutine(coroutine);
        }

        //public void 
        IEnumerator LateCall(bool value, float sec)
        {

            yield return new WaitForSeconds(sec);
            afterDeathEvent.Invoke();

            
            
                currentPlayer.gameObject.SetActive(value);
            

            //Do Function here...
        }

        IEnumerator LateCallResurrectEffect(float sec)
        {

            yield return new WaitForSeconds(sec);

            EffectsController.Instance.CreateResurrectEffect(currentPlayer.gameObject.transform.position);


            // currentPlayer.gameObject.SetActive(value);


            //Do Function here...
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

