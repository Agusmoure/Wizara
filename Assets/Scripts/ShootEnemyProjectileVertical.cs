using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyProjectileVertical : MonoBehaviour {

    public GameObject enemyProjectile;
    PoolManager pools;
    public float time;
    // Use this for initialization
    void Start()
    {
        pools = GameManager.instance.ReturnPoolManager();
        InvokeRepeating("Instantiate", 0, time);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //metodo que crea la caca
    void Instantiate()
    {
        GameObject newProjectile = Instantiate(enemyProjectile, transform.position, Quaternion.identity, pools.GetProjectilePool());
    }
}
