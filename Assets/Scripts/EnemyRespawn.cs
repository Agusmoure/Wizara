using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour {

    public void Respawn(Vector3[] wayPointPositions)
    {
        int currentWayPoint = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Point") || transform.GetChild(i).name.Contains("Rotation"))
            {
                transform.GetChild(i).gameObject.transform.position = wayPointPositions[currentWayPoint];
                currentWayPoint++;
            }
        }
    }
}
 