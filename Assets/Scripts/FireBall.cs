using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public float speed;
    Rigidbody2D fireB;
    Animator anime;
    // Use this for initialization
    void Start()
    {
        //cacheo de componentes
        fireB = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        AudioToPlay audio = GetComponent<AudioToPlay>();
        if (audio != null) audio.SendAudioToPlay();
    }

    //movemos la bola de fuego en todo momento
    void FixedUpdate()
    {
        fireB.velocity = transform.right * speed;
    }
    //Cuando coincide con un obstaculo lo destruye, y acto seguido se destruye ella misma si choca con cualquier otra cosa != jugador se destruye
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle")) Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
    //cambia la direccion del disparo
    public void ChangeDirection(Vector2 newDir)
    {
        transform.right = newDir;
    }
}
