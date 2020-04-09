using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace garagekitgames
{
    public class Preloader : MonoBehaviour
    {

        private CanvasGroup fadeGroup;
        private float loadTime;
        private float minimumLogoTime = 3.0f; 

        // Use this for initialization
        void Start()
        {
            fadeGroup = FindObjectOfType<CanvasGroup>();
            fadeGroup.alpha = 1;
            //Preload the game, either from server or local 

            if(Time.time < minimumLogoTime)
            {
                loadTime = minimumLogoTime;
            }
            else
            {
                loadTime = Time.time;
            }
        }

        // Update is called once per frame
        void Update()
        {

            //Fade In 
            if(Time.time < minimumLogoTime)
            {
                fadeGroup.alpha = 1 - Time.time;
            }

            //Fade out 
            if (Time.time > minimumLogoTime && loadTime != 0)
            {
                fadeGroup.alpha = Time.time - minimumLogoTime;
                if(fadeGroup.alpha >= 1)
                {
                    SceneManager.LoadScene("MainMenu");
                    //Application.LoadLevel("MainMenu");
                }
            }

        }
    }
}

