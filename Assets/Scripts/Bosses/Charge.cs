using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {
    public int speed;
    Rigidbody2D rigidbody2D;
	// Use this for initialization
	void Start () {
        rigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
