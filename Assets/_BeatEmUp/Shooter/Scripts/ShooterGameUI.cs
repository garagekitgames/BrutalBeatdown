using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace garagekitgames.shooter
{

    public class ShooterGameUI : MonoBehaviour
    {
        public GameObject winText;
        public GameObject loseText;

        public GameObject waveUI;

        public Text waveNumberText;
        public Text enemyCountText;
        public bool gameOver;
        void ShowWinUI()
        {
            winText.SetActive(true);
            gameOver = true;
        }

        void ShowWaveUI(int waveNo, int enemyCount)
        {
            waveUI.SetActive(true);
            waveNumberText.text = "Wave : " + waveNo.ToString();
            enemyCountText.text = "Enemies : " + enemyCount.ToString();
            Invoke("HideWaveUI", 2);
        }

        void HideWaveUI()
        {
            waveUI.SetActive(false);
        }
        void ShowLoseUI()
        {
            loseText.SetActive(true);
            gameOver = true;
        }
        // Start is called before the first frame update
        void Start()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<LivingEntity>().OnDeath += OnPlayerDeath;
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().NoNewWave += OnWavesComplete;
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().OnNewWave += ShowWaveUI;
        }

        private void OnWavesComplete()
        {
            ShowWinUI();
        }

        private void OnPlayerDeath()
        {
            ShowLoseUI();
        }

        // Update is called once per frame
        void Update()
        {
            if (gameOver)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

}