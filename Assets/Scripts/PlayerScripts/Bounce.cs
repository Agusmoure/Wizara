using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public float bounceForce;
    Rigidbody2D player;

    //Se asigna a la variable "player" el Rigidbody2D de este objeto
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    //Método que aplica la fuerza de rebote.
    public void BounceTo(float x, float y)
    {
        player.AddForce(new Vector2(x, y) * bounceForce, ForceMode2D.Impulse);
    }
}
