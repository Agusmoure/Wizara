using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyProjectileVertical : MonoBehaviour {

    public GameObject enemyProjectile;
    public Transform projectilePool;
    public float time;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Instantiate", 0, time);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //metodo que crea la caca
    void Instantiate()
    {
        GameObject newProjectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity, projectilePool);
    }
}
