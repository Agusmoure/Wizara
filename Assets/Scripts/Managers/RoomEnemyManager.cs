using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemyManager : MonoBehaviour {

    public GameObject rat, bat, enemyWizard, slime;
    
    [System.Serializable]
    struct Enemy
    {
        public GameObject enemyObject;
        public Vector3 spawnPosition;
        public Vector3[] wayPointPositions;
    }

    [SerializeField]
    Enemy[] enemyArray;

    // Use this for initialization
    void Start ()
    {
        StoreInfo();
	}

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            RespawnEnemies();
            Debug.Log("Respawn");
        }
    }

    void StoreInfo()
    {
        enemyArray = new Enemy[transform.childCount];

        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (transform.GetChild(i).gameObject.name.Contains("Rat")) enemyArray[i].enemyObject = rat;

            else if (transform.GetChild(i).gameObject.name.Contains("Wizard")) enemyArray[i].enemyObject = enemyWizard;

            else if (transform.GetChild(i).gameObject.name.Contains("Slime"))
            {
                enemyArray[i].enemyObject = slime;
                StoreWaypoints(i, "Rotation");
            }

            if (transform.GetChild(i).gameObject.name.Contains("Bat"))
            {
                enemyArray[i].enemyObject = bat;
                StoreWaypoints(i, "Point");
            }

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

    public void RespawnEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < enemyArray.Length; i++)
        {
            GameObject enemySpawned = Instantiate(enemyArray[i].enemyObject, enemyArray[i].spawnPosition, Quaternion.identity, transform);
            enemySpawned.GetComponent<EnemyRespawn>().Respawn(enemyArray[i].wayPointPositions);
        }
    }
}
