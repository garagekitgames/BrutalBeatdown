using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using garagekitgames;
using DG.Tweening;


[System.Serializable]
public enum Direction
{
    North=1,
    South=2,
    East=3,
    West=4
}

[System.Serializable]
public enum Side
{
    Straight=0,
    Left=1,
    Right=2
}

[System.Serializable]
public class FaceDirectionWait
{
    public Direction direction;
    public float wait;
}

[System.Serializable]
public class LookSideWait
{
    public Side side;
    public float wait;
}

[System.Serializable]
public class WaypointWait
{
    public Transform waypoint;
    public float wait;
}
public class SneakyEnemyAI : MonoBehaviour
{
	public CharacterThinker character;

    public Vector3 lookStraightPose;
    public Vector3 lookLeftPose;
    public Vector3 lookRightPose;
    
    // Start is called before the first frame update

    public BodyPartMono headPart;

    public int lookSwithTime =  3;

    public int switchCount = 0;

    public bool lookLeft = false;

    public List<WaypointWait> waypoints;
    public List<FaceDirectionWait> directions;
    public List<LookSideWait> lookSides;

    public Vector3 leftHeadRotation = new Vector3(45, -24, 0);
    public Vector3 rightHeadRotation = new Vector3(-45, -24, 0);

    public float headTurnDuration = 3f;
   // public Vector3 rightHeadRotation = new Vector3(-45, -24, 0);


    void Start()
    {
        DOTween.Init();
        character = this.GetComponent<CharacterThinker>();
        headPart = character.bpHolder.BodyPartsName[BodyPartNames.headName];

        //InvokeRepeating("LookLeft", 6, 3);
        //InvokeRepeating("LookRight", 3, 3);
        //InvokeRepeating("SwitchLook", 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchLook()
    {
        switchCount++;

        if(switchCount%lookSwithTime == 0)
        {
            if(lookLeft)
            {
                LookRight();
            }
            else
            {
                LookLeft();
            }
        }
    }
    public void LookLeft()
    {
        

        //BodyPartMono bodyPart = headPart;
        //JointDrive x = bodyPart.BodyPartConfigJoint.slerpDrive;
        //x.positionDamper = windupPose.jointDrive.x;
        //x.positionSpring = windupPose.jointDrive.y;
        //x.maximumForce = windupPose.jointDrive.z;
        //bodyPart.BodyPartConfigJoint.slerpDrive = x;

        //headPart.BodyPartConfigJoint.targetAngularVelocity = lookLeftPose;
        headPart.BodyPartTransform.DOLocalRotate(new Vector3(45, -24, 0), 4f);
        //headPart.bodypart
        lookLeft = true;
    }

    public void FaceDirection(int dir)
    {
        switch(dir)
        {
            case 1:
                character.target = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position + Vector3.forward;
                break;
            case 2:
                character.target = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position + Vector3.back;
                break;
            case 3:
                character.target = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position + Vector3.right;
                break;
            case 4:
                character.target = character.bpHolder.BodyPartsName[BodyPartNames.hipName].BodyPartTransform.position + Vector3.left;
                break;
        }
    }

    public void LookRight()
    {


        //BodyPartMono bodyPart = headPart;
        //JointDrive x = bodyPart.BodyPartConfigJoint.slerpDrive;
        //x.positionDamper = windupPose.jointDrive.x;
        //x.positionSpring = windupPose.jointDrive.y;
        //x.maximumForce = windupPose.jointDrive.z;
        //bodyPart.BodyPartConfigJoint.slerpDrive = x;

        //headPart.BodyPartConfigJoint.targetAngularVelocity = lookRightPose;
        headPart.BodyPartTransform.DOLocalRotate(new Vector3(-45, -24, 0), 4f);
        lookLeft = false;
    }
}
