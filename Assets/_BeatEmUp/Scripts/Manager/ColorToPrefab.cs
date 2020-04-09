using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefab
{
    public Color color;
    public GameObject prefab;
    public bool parent;
    public PrefabType prefabType;
    public enum PrefabType
    {
        Obstacle,
        HidingSpot,
        Waypoint,
        Enemy
    }
}
