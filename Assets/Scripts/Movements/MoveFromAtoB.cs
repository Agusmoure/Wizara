using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromAtoB : MonoBehaviour {

    Rigidbody2D bat;
    public Transform pointA, pointB;
    public float speedX, speedY;
    Animator anime;

    // Use this for initialization
    void Start()
    {
        bat = GetComponent<Rigidbody2D>();
        transform.position = pointA.position;
        anime = GetComponent<Animator>();
    }

    private void Update()
    {
        ChangeXSpeed();
        ChangeYspeed();
        ChangeScale();
    }

    //fisicas
    private void FixedUpdate()
    {
        bat.velocity = new Vector2(speedX, speedY);
    }

    //si collisiona con algo !=jugador o != de su caca cambia la velocidad en X e Y
    public void ChangeBouthSpeed()
    {
   
            speedX = -speedX;
            speedY = -speedY;
    }

    //cambiamos la velocidad en X
    void ChangeXSpeed()
    {
            if (this.transform.position.x >= pointB.position.x)
            {
                speedX = -Mathf.Abs(speedX);
            }

            else if (this.transform.position.x <= pointA.position.x)
            {
                speedX = Mathf.Abs(speedX);
            }
    }

    //cambaimos la velocidad en Y
    void ChangeYspeed()
    {
        if (transform.position.y <= pointA.position.y )
        {
            speedY = Mathf.Abs(speedY);
        }
        else if (transform.position.y >= pointB.position.y)
        {
            speedY = -Mathf.Abs(speedY);
        }
    }

    //cambiamos la escala
    void ChangeScale()
    {
        float       scaleX = transform.localScale.x;
        if (speedX > 0) scaleX = Mathf.Abs(scaleX);
        else if (speedX < 0) scaleX = -Mathf.Abs(scaleX);
        transform.localScale = new Vector2(scaleX, transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
