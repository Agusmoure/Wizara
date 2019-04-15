using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour {

    public void Respawn(Vector3[] wayPointPositions, float scaleX, float scaleY, float speedX, float speedY)
    {
        if (name.Contains("Rat")) transform.GetChild(0).localScale = new Vector3(scaleX, scaleY, transform.localScale.z);

        else if (name.Contains("Platform")) transform.GetChild(0).localScale = new Vector3(scaleX, scaleY, transform.localScale.z);

        else transform.localScale = new Vector3(scaleX, scaleY, transform.localScale.z);
        UpdateSpeed(speedX, speedY);

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

    void UpdateSpeed(float speedX, float speedY)
    {
        if (gameObject.GetComponentInChildren<AlternativePlatformMovement>() != null) gameObject.GetComponentInChildren<AlternativePlatformMovement>().UpdateSpeed(speedX);

        else if (gameObject.GetComponentInChildren<Move>() != null) gameObject.GetComponentInChildren<Move>().UpdateSpeed(speedX);

        else if (gameObject.GetComponentInChildren<MoveAroundPlatforms>() != null) gameObject.GetComponentInChildren<MoveAroundPlatforms>().UpdateSpeed(speedX);

        else if (gameObject.GetComponentInChildren<MoveFromAtoB>() != null) gameObject.GetComponentInChildren<MoveFromAtoB>().UpdateSpeed(speedX, speedY);


    }
}
 