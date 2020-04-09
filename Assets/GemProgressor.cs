using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.Progress;
using SO;
using garagekitgames;

public class GemProgressor : MonoBehaviour
{
    public Progressor gemProgressor;
    public float gemIncrement = 1;
    public float keyIncrement = 11.0f;

    public float totalValue = 100;
    public int totalcoins;
    public float currentValue = 0;


    private void Awake()
    {
        totalcoins = GameObject.FindGameObjectsWithTag("Gem").Length;
        gemProgressor = this.gameObject.GetComponent<Progressor>();
    }
    // Start is called before the first frame update
    void Start()
    {

        keyIncrement = 1.0f / 9.0f;
        gemIncrement = 2.0f / (3.0f * totalcoins);
        gemProgressor.SetProgress(currentValue);
    }

    public void OnGemCollected()
    {
        currentValue += gemIncrement;
        gemProgressor.SetProgress(currentValue);


    }

    public void OnKeyCollected()
    {
        currentValue += keyIncrement;
        gemProgressor.SetProgress(currentValue);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
