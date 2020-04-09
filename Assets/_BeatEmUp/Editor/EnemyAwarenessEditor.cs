using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EnemyAwareness))]
public class EnemyAwarenessEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyAwareness enemyAwareness = (EnemyAwareness)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyAwareness.transform.position, Vector3.up, Vector3.forward, 360, enemyAwareness.viewRadius);

        enemyAwareness.transform.GetComponent<SphereCollider>().radius = enemyAwareness.viewRadius;

        Vector3 viewAngleA = enemyAwareness.DirectionFromAngle(-enemyAwareness.fieldOfView / 2, false);
        Vector3 viewAngleB = enemyAwareness.DirectionFromAngle(enemyAwareness.fieldOfView / 2, false);

        Handles.DrawLine(enemyAwareness.transform.position, enemyAwareness.transform.position + viewAngleA * enemyAwareness.viewRadius);
        Handles.DrawLine(enemyAwareness.transform.position, enemyAwareness.transform.position + viewAngleB * enemyAwareness.viewRadius);
    }


}
