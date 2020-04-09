using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace garagekitgames
{
    [CreateAssetMenu(menuName = "GarageKitGames/LevelInfo")]
    public class LevelInfo : ScriptableObject
    {
        //public List<GameObject> Enemies;
        //public GameObject Player;
        //public Vector3Variable spawnPoint;
        public bool isUnlocked;
        public string levelName;
        public int levelPrice;


    }
}
