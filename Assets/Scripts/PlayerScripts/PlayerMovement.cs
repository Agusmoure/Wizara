using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float velocity, jumpForce, dashForce;
    int maxJump=1;
    int jump;
    float inputX;
    public bool movmentRestriction = false;
    bool dash = true;
    Rigidbody2D player;
    Vector3 scale;
    Animator anime;
    ContactPoint2D[] contact = new ContactPoint2D [1];


    // Use this for initialization
    void Start ()
    {
        player = gameObject.GetComponent<Rigidbody2D>();
        GameManager.instance.GetPlayer(gameObject);
        scale = transform.localScale;
        jump = maxJump;
        anime = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        inputX = Input.GetAxis("Horizontal");
        PlayerSpriteAnimation();
        ChangeVelocity();

        JumpInput();

        DashInput();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.GetContacts(contact);

        if ((contact[0].normal.x > 0.9 && contact[0].normal.x < 1.1) || (contact[0].normal.x < -0.9 && contact[0].normal.x > -1.1))
        {
            movmentRestriction = true;
        }

        else if (contact[0].normal.y > 0.9 && contact[0].normal.y < 1.1)
        {
            jump = maxJump;
            movmentRestriction = false;
            dash = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Mantiene al jugador sobre una plataforma en movimiento y le mantiene sobre ella
        if (collision.gameObject.tag == "MovingPlatform")
        {
            player.transform.parent = collision.transform;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        collision.GetContacts(contact);

        if ((contact[0].normal.x > 0.9 && contact[0].normal.x < 1.1) || (contact[0].normal.x < -0.9 && contact[0].normal.x > -1.1)) movmentRestriction = false;
        else if (contact[0].normal.y > 0.9 && contact[0].normal.y < 1.1) jump--;

        //comprueba si el jugador deja de estar en contacto con una plataforma en movimiento para que ya no tenga que estar sobre ella
        if (collision.gameObject.tag == "MovingPlatform")
        {
            player.transform.parent = null;
        }
    }

    void ChangeVelocity()
    {
        if(!movmentRestriction) player.velocity = new Vector2(velocity * inputX, player.velocity.y);

        if (movmentRestriction)
        {
            if (contact[0].normal.x < 0 && inputX < 0) player.velocity = new Vector2(velocity * inputX, player.velocity.y);

            else if (contact[0].normal.x > 0 && inputX > 0) player.velocity = new Vector2(velocity * inputX, player.velocity.y);
        }
    }

    void PlayerSpriteAnimation()
    {
        if (inputX < 0)
        {
            if (jump == 0) anime.Play("Walk");

            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        else if (inputX > 0)
        {
            if (jump == 0) anime.Play("Walk");

            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        else anime.Play("Static");
    }

    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jump > 0 && !GameManager.instance.IsPaused())
        {
            player.velocity = new Vector2(player.velocity.x, 0);
            player.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump--;
        }
    }
    
    void DashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dash && !GameManager.instance.IsPaused())
        {
            //Frena el movimiento horizontal del jugador.
            player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            //Pequeño impulso hacia arriba para levantar al jugador del suelo.
            player.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
            //Retraso de 0.03 para que de tiempo a levantar a jugador.
            Invoke("DoDash", 0.02f);
        }
    }

    void DoDash()
    {
        player.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        player.AddForce(new Vector2(scale.x * dashForce, 0), ForceMode2D.Impulse);
        movmentRestriction = true;
        dash = false;
        //Tiempo en realizar el dash 0.15.
        Invoke("DashCast", 0.15f);
    }

    void DashCast()
    {
        //Tras finalizar el dash.
        movmentRestriction = false;
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    //Método que pasa el maxJump a 2
    public void DoubleJumpActive()
    {
        maxJump = 2;
    }
}
