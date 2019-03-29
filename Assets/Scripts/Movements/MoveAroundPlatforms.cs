﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundPlatforms : MonoBehaviour {

    public float speed;
    Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        //Mueve el gameObject en su dirección derecha.
        rigidbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el trigger tiene el mismo padre, cambia su rotación.
        if (collision.transform.parent == this.transform.parent)
        {
            //Con speed positiva la rotación disminuye 90º, en caso contrario aumenta.
            if(speed>0) transform.Rotate(new Vector3(0, 0, 1), -90);
            else transform.Rotate(new Vector3(0, 0, 1), 90);
        }
    }
}
