using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using System.Linq;
using EZCameraShake;
using UnityEngine.SceneManagement;
//using DoozyUI;
using UnityEngine.Events;
using TMPro;

namespace garagekitgames
{
    public class MainMenuManager : UnitySingleton<MainMenuManager>
    {
        public List<LevelInfo> levels;

        public List<LevelData> levelData = new List<LevelData>();

        public List<GameObject> levelCharacters = new List<GameObject>();

        public List<bool> levelLocks = new List<bool>();

        public LevelData currentLevel;

        public IntVariable selectedIndex;

        private int numObjects = 10;
        public float distanceBetweenCharacter = 2;
        public float radius = 10;
        public GameObject prefab;
        public bool lookingAtCamera = false;
        public CameraLookAt mainCam;

        public BodyPart lookAtPart;

        public GameObject playButton;
        public GameObject buyButton;

        public GameObject lockImage;

        public Sprite lockSprite;
        public Sprite unlockSprite;

        public IntVariable cashValue;

        /* public UIButton playUIButton;
         public UIButton buyUIButton;*/

        public UnityEvent updateCashUI;

        public TextMeshProUGUI buttonCashValueUI;
        // Use this for initialization
        void Awake()
        {
            //base.Awake();
            mainCam = GameObject.FindObjectOfType<CameraLookAt>();
            foreach (var level in levels)
            {
                //uncomment this 
                // levelData.Clear();
                levelData.Add(Resources.Load<LevelData>("LevelData/" + level.levelName));
                levelLocks.Add(level.isUnlocked ? true : false);

            }
            numObjects = levels.Count;

            Vector3 center = transform.position;
            for (int i = 0; i < numObjects; i++)
            {

                // Quaternion rot = Quaternion.FromToRotation(Vector3.up, center - pos);
                radius = (distanceBetweenCharacter * numObjects) / (2 * Mathf.PI);
                float theta = (2 * Mathf.PI / numObjects) * i;
                Vector3 pos = RandomCircle2(center, radius, i, theta);
                var playerGameObject = Instantiate(levelData[i].Player, pos, Quaternion.identity);
                CharacterThinker character = playerGameObject.GetComponent<CharacterThinker>();
                character.target = new Vector3(0, 1, 0);
                //character.SetStopDoingShit(true);
                levelCharacters.Add(playerGameObject);
            }

            playButton.SetActive(true);
            //buyUIButton.
            buyButton.SetActive(false);
            lockImage.SetActive(false);

            updateCashUI.Invoke();
            //playButton.
            /* for (int i = 0; i < numObjects; ++i)
             {
                 float theta = (2 * Mathf.PI / numObjects) * i;
                 x = cos(theta);
                 z = sin(theta);
             }*/

        }


        private void OnDisable()
        {
            //CameraShaker.Instance.StopAllCoroutines();
        }

        private void Start()
        {

            //CameraShaker.Instance.Shake(CameraShakePresets.HandheldCamera).StartFadeIn(0.5f);
            //CameraShakeInstance c = new CameraShakeInstance(1f, 0.25f, 5f, 10f);
            //c.PositionInfluence = Vector3.zero;
            //c.RotationInfluence = new Vector3(1, 0.5f, 0.5f);
            CameraShaker.Instance.StartShake(1f, 0.25f, 5f);
            UpdateSelectedLevel(selectedIndex.value);
            AudioManager.instance.FadeInCaller("BGM2", 0.1f, 0.3f);
            AudioManager.instance.FadeOutCaller("BGM1", 0.1f);
            /*foreach (var characterGO in levelCharacters)
            {
                CharacterThinker character = characterGO.GetComponent<CharacterThinker>();
                character.SetStopDoingShit(true);
            }*/
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (!lookingAtCamera)
            {
                foreach (var characterGO in levelCharacters)
                {
                    CharacterThinker character = characterGO.GetComponent<CharacterThinker>();
                    character.stopAttacking = true;
                    character.stopLooking = true;
                }
                lookingAtCamera = true;
            }


        }

        public void ToggleLeft()
        {
            selectedIndex.value--;
            if (selectedIndex.value < 0)
            {
                selectedIndex.value = levelCharacters.Count - 1;
            }
            UpdateSelectedLevel(selectedIndex.value);
            //Call Update UI
            //Update the camera target
            //set the selected character // set currentLevel = selectedLevel
        }

        public void ToggleRight()
        {
            selectedIndex.value++;
            if (selectedIndex.value == levelCharacters.Count)
            {
                selectedIndex.value = 0;
            }
            UpdateSelectedLevel(selectedIndex.value);
            //Call Update UI
            //Update the camera target
            //set the selected character // set currentLevel = selectedLevel
        }

        public void UpdateSelectedLevel(int index)
        {
            var cT = new CharacterThinker();
            Debug.Log("Level Updated Clicked");
            //currentLevel = levelData[index];
            currentLevel.Enemies = levelData[index].Enemies;
            currentLevel.Player = levelData[index].Player;
            currentLevel.spawnPoint = levelData[index].spawnPoint;
            currentLevel.enemyGroups = levelData[index].enemyGroups;
            mainCam.target = levelCharacters[index].transform.GetComponent<CharacterThinker>().bpHolder.bodyParts[lookAtPart].BodyPartTransform;
            buttonCashValueUI.text = levels[index].levelPrice.ToString();

            if (levels[index].isUnlocked)
            {
                playButton.SetActive(true);
                buyButton.SetActive(false);
                lockImage.SetActive(false);
            }
            else
            {
                playButton.SetActive(false);
                buyButton.SetActive(true);
                lockImage.SetActive(true);
            }

        }

        public void OnPlayClick()
        {
            //Debug.Log("Play Clicked");
            //Change this to load scene from level info
            SceneManager.LoadScene("Level4");
            AudioManager.instance.FadeOutCaller("BGM2", 0.1f);
            //Application.LoadLevel("Level1");
        }

        public void AttemptBuy()
        {
            if (cashValue.value > levels[selectedIndex.value].levelPrice)
            {
                cashValue.value = cashValue.value - levels[selectedIndex.value].levelPrice;
                levels[selectedIndex.value].isUnlocked = true;
                updateCashUI.Invoke();

                PersistableSO.Instance.Save();
                UpdateSelectedLevel(selectedIndex.value);

            }
            else
            {
                //Display Buy Coins Panel
            }

        }

        Vector3 RandomCircle(Vector3 center, float radius, int i)
        {
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y;
            pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            return pos;
        }


        Vector3 RandomCircle2(Vector3 center, float radius, int i, float theta)
        {
            //float ang = Random.value * 360;

            //float theta = (2 * Mathf.PI / numObjects) * i;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Cos(theta);
            pos.y = center.y;
            pos.z = center.z + radius * Mathf.Sin(theta);
            return pos;
        }
    }
}

