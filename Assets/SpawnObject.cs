using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public GameObject[] objects;
    // Start is called before the first frame update
    private void Awake()
    {
        Instantiate(objects[Random.Range(0, objects.Length)], transform.position, Quaternion.identity);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
