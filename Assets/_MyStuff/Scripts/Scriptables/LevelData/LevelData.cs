using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace garagekitgames
{
    [CreateAssetMenu(menuName = "GarageKitGames/LevelData")]
    public class LevelData : ScriptableObject
    {
        public List<GameObject> Enemies;
        public GameObject Player;
        public Vector3Variable spawnPoint;
        public List<EnemyGroup> enemyGroups;
        //public bool isUnlocked;
        //public string levelName;
        //public int levelPrice;

    }
}
