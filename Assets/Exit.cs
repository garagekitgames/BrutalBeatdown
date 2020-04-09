using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.GameStructure.Levels;

namespace garagekitgames
{
    public class Exit : MonoBehaviour
    {
        public string targetTag = "CamTarget_Player";
        public UnityEvent OnExitReached;
        public UnityEvent DisplayHint;
        //public GameObject gate;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Contains("Player")&& LevelManager.Instance.collectedStars >= 3)
            {
                OnExitReached.Invoke();
            }
            else if(other.gameObject.tag.Contains("Player") && !(LevelManager.Instance.collectedStars >= 3))
            {
                DisplayHint.Invoke();
            }
        }
    }

}
