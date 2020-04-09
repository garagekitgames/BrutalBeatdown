using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace garagekitgames
{
    public class GameManager : UnitySingletonPersistent<GameManager>
    {
        public IntVariable noOfTimesPlayed;
        public override void Awake()
        {
            base.Awake();
            
        }
        // Use this for initialization
        void Start()
        {
            //
            PersistableSO.Instance.LoadVersion();
            noOfTimesPlayed.value = noOfTimesPlayed.value + 1;
            Debug.Log("Game Launch : " + noOfTimesPlayed.value);
            if (noOfTimesPlayed.value > 0)
            {
                PersistableSO.Instance.Load();
                Debug.Log("Game Launch Times : " + noOfTimesPlayed.value);
                PersistableSO.Instance.SaveVersion();
            }
            else
            {
                

                Debug.Log("Game Launch First Time : " + noOfTimesPlayed.value);
                PersistableSO.Instance.Save();
                PersistableSO.Instance.SaveVersion();
            }
            
        }

        private void OnEnable()
        {
            //PersistableSO.Instance.Load();
        }

        private void OnDisable()
        {
           // PersistableSO.Instance.Save();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

