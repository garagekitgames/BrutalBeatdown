using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using Pathfinding;

public class EjectMarker : MonoBehaviour
{
	public CharacterThinker character;
    public Transform vision;
    public GameObject TargetMarker;
    public GameObject proximity;
    public bool died;
    // Start is called before the first frame update
    void Start()
    {
		character = this.GetComponent<CharacterThinker>();
        vision = transform.FindDeepChild("VisionCone");
        character.health.DeathEvent.AddListener(Ondeath);
    }

    public void Ondeath()
    {
        if (!character.health.alive)
        {
            if (transform.FindDeepChild("PlayerTargetMarker(Clone)"))
            {
                transform.FindDeepChild("PlayerTargetMarker(Clone)").SetParent(null);
            }

            if (vision)
            {
                vision.GetComponent<DynamicGridObstacle >().setTag = 0;
            }

            //vision.gameObject.SetActive(false);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
