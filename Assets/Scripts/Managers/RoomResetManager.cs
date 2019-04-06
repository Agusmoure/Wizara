using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomResetManager : MonoBehaviour {

    public GameObject rat, bat, enemyWizard, slime, movingPlatform, slimePlatform;
    
    [System.Serializable]
    struct ResettableObject
    {
        [System.Serializable]
        public struct Scale
        {
            public float x, y;
        }

        [System.Serializable]
        public struct Speed
        {
            public float x, y;
        }

        [SerializeField]
        public Scale enemyScale;
        [SerializeField]
        public Speed enemySpeed;
        public GameObject enemyObject;
        public Vector3 spawnPosition;
        public Vector3[] wayPointPositions;
    }

    [SerializeField]
    ResettableObject[] enemyArray;

    // Use this for initialization
    void Start ()
    {
        StoreInfo();
        DestroyEnemies();
    }

    void StoreInfo()
    {
        enemyArray = new ResettableObject[transform.childCount];

        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (transform.GetChild(i).gameObject.name.Contains("Rat")) enemyArray[i].enemyObject = rat;

            else if (transform.GetChild(i).gameObject.name.Contains("Wizard")) enemyArray[i].enemyObject = enemyWizard;

            else if (transform.GetChild(i).gameObject.name.Contains("Slime" ) && !transform.GetChild(i).gameObject.name.Contains("Slime"))
            {
                enemyArray[i].enemyObject = slime;
                StoreWaypoints(i, "Rotation");
            }

            else if (transform.GetChild(i).gameObject.name.Contains("Bat"))
            {
                enemyArray[i].enemyObject = bat;
                StoreWaypoints(i, "Point");
            }

            else if (transform.GetChild(i).gameObject.name == "MovingPlatform")
            {
                enemyArray[i].enemyObject = movingPlatform;
                StoreWaypoints(i, "Point");
            }

            else if (transform.GetChild(i).gameObject.name == "SlimeMovingPlatform")
            {
                enemyArray[i].enemyObject = slimePlatform;
                StoreWaypoints(i, "Point");
            }

            StoreScale(i);
            StoreSpeed(i);
            enemyArray[i].spawnPosition = transform.GetChild(i).transform.position;
        }
    }

    void StoreWaypoints(int i, string pointName)
    {
        int j = 0;

        for (int k = 0; k < transform.GetChild(i).transform.childCount; k++)
        {
            if (transform.GetChild(i).transform.GetChild(k).name.Contains(pointName)) j++;
        }

        enemyArray[i].wayPointPositions = new Vector3[j];
        j = 0;

        for (int k = 0; k < transform.GetChild(i).transform.childCount; k++)
        {
            if (transform.GetChild(i).transform.GetChild(k).name.Contains(pointName))
            {
                enemyArray[i].wayPointPositions[j] = transform.GetChild(i).transform.GetChild(k).transform.position;
                j++;
            }
        }
    }

    void StoreScale(int i)
    {
        if (transform.GetChild(i).name.Contains("Wizard"))
        {
            enemyArray[i].enemyScale.x = transform.GetChild(i).transform.localScale.x;
            enemyArray[i].enemyScale.y = transform.GetChild(i).transform.localScale.y;
        }

        else for (int j = 0; j < transform.GetChild(i).childCount; j++)
        {
            if (transform.GetChild(i).transform.GetChild(j).name.Contains("Rat") || transform.GetChild(i).transform.GetChild(j).name.Contains("Bat") 
                    || transform.GetChild(i).transform.GetChild(j).name.Contains("Slime") || transform.GetChild(i).transform.GetChild(j).name.Contains("Platform"))
            {
                enemyArray[i].enemyScale.x = transform.GetChild(i).transform.GetChild(j).transform.localScale.x;
                enemyArray[i].enemyScale.y = transform.GetChild(i).transform.GetChild(j).transform.localScale.y;
            }
        }
    }

    void StoreSpeed(int i)
    {
        if (transform.GetChild(i).GetComponentInChildren<AlternativePlatformMovement>() != null)
        {
            enemyArray[i].enemySpeed.x = transform.GetChild(i).GetComponentInChildren<AlternativePlatformMovement>().ReturnSpeed();
            enemyArray[i].enemySpeed.y = 0f;
        }

        else if (transform.GetChild(i).GetComponentInChildren<Move>() != null)
        {
            enemyArray[i].enemySpeed.x = transform.GetChild(i).GetComponentInChildren<Move>().ReturnSpeed();
            enemyArray[i].enemySpeed.y = 0f;
        }

        else if (transform.GetChild(i).GetComponentInChildren<MoveAroundPlatforms>() != null)
        {
            enemyArray[i].enemySpeed.x = transform.GetChild(i).GetComponentInChildren<MoveAroundPlatforms>().ReturnSpeed();
            enemyArray[i].enemySpeed.y = 0f;
        }

        else if (transform.GetChild(i).GetComponentInChildren<MoveFromAtoB>() != null)
        {
            enemyArray[i].enemySpeed.x = transform.GetChild(i).GetComponentInChildren<MoveFromAtoB>().ReturnSpeed().x;
            enemyArray[i].enemySpeed.y = transform.GetChild(i).GetComponentInChildren<MoveFromAtoB>().ReturnSpeed().y;
        }
    }

    public void RespawnEnemies()
    {
        DestroyEnemies();

        for (int i = 0; i < enemyArray.Length; i++)
        {
            GameObject enemySpawned = Instantiate(enemyArray[i].enemyObject, enemyArray[i].spawnPosition, Quaternion.identity, transform);
            enemySpawned.GetComponent<EnemyRespawn>().Respawn(enemyArray[i].wayPointPositions, enemyArray[i].enemyScale.x, enemyArray[i].enemyScale.y, enemyArray[i].enemySpeed.x, enemyArray[i].enemySpeed.y);
        }
    }

    public void DestroyEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
