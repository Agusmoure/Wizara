using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;
    Rigidbody2D rigidbody2D;
    Animator animator;

	// Use this for initialization
	void Start () {

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        animator.Play("MoveRat");
	}
    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(speed,rigidbody2D.velocity.y);
    }

    public void ChangeDirection()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        speed = -speed;
    }
}
