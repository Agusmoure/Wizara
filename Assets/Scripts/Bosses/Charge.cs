using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {
    public int speed;
    Rigidbody2D rigidB;
	// Use this for initialization
	void Start () {
        rigidB = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameManager.instance.ReturnBossManager().Executing())
        rigidB.AddForce(Vector2.left*speed,ForceMode2D.Force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
