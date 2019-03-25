using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadWizard : MonoBehaviour
{
    public int VAR;
    public int fireBallCooldown;
    public Transform projectilePool;
    bool cerca;
    GameObject go;
    // Use this for initialization
    void Update ()
    {
        go = GameObject.Find("Player");
        cerca = Mathf.Abs(transform.position.x - go.transform.position.x) <= go.transform.localScale.x * VAR;
        cerca = true; //quitar
	}
	
	// Update is called once per frame
	void Cast ()
    {
        if(cerca)
            FireBallInput();
    }
    void FireBallInput()
    {
        InstantiateFireBall();
        Invoke("FireBallCD", fireBallCooldown);
    }
    void InstantiateFireBall()
    {
        FireBall newFireBall = Instantiate(go.GetComponent<FireBall>(), transform.position, Quaternion.identity, projectilePool);
        Vector2 newDirection = transform.lossyScale.x * transform.right;
        newFireBall.ChangeDirection(newDirection);
    }
}
