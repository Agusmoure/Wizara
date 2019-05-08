using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float velocity, jumpForce, dashForce, maxFallSpeed, yVelocity,xVelocity;
   public int maxJump = 1;
    int jump;
    float inputX;
    public bool movRestrictionL = false, movRestrictionR = false;
    bool dash = true;
    Rigidbody2D player;
    Vector3 scale;
    Animator anime;
    ContactPoint2D[] contact = new ContactPoint2D [1];


    // Use this for initialization
    void Start ()
    {

        player = gameObject.GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        jump = maxJump;
        wallJump = maxWallJump;
        anime = GetComponent<Animator>();
        if (GameManager.instance.ReturnAbilityValue("DoubleJump")) DoubleJumpActive();
    }

	// Update is called once per frame
	void Update ()
    {
        inputX = Input.GetAxis("Horizontal");
        PlayerDirection();
        ChangeVelocity();
        anime.SetFloat("VelocityX",Mathf.Abs(player.velocity.x));
        anime.SetFloat("VelocityY",Mathf.Abs(player.velocity.y));
        JumpInput();
        DashInput();

        if (player.velocity.y < -maxFallSpeed) player.velocity = new Vector2(player.velocity.x, -maxFallSpeed);
        yVelocity = player.velocity.y;
        xVelocity = player.velocity.x;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.GetContacts(contact);

        if (contact[0].normal.x > 0.9 && contact[0].normal.x < 1.1)
        {
            movRestrictionL = true;
        }
        else if (contact[0].normal.x < -0.9 && contact[0].normal.x > -1.1)
        {
            movRestrictionR = true;
        }
        if (contact[0].normal.y > 0.9 && contact[0].normal.y < 1.1)
        {
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

         movRestrictionL = false;
         movRestrictionR = false;

        //comprueba si el jugador deja de estar en contacto con una plataforma en movimiento para que ya no tenga que estar sobre ella
        if (collision.gameObject.tag == "MovingPlatform")
        {
            player.transform.parent = null;
        }
    }


    void ChangeVelocity()
    {
        if(!movRestrictionL && !movRestrictionR) player.velocity = new Vector2(velocity * inputX, player.velocity.y);

        else if (movRestrictionL && inputX > 0)
        {
           player.velocity = new Vector2(velocity * inputX, player.velocity.y);
        }
        else if (movRestrictionR && inputX < 0)
        {
            player.velocity = new Vector2(velocity * inputX, player.velocity.y);
        }
    }

    void PlayerDirection()
    {
        if (inputX < 0 && !noInput)
        {
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        else if (inputX > 0 && !noInput)
        {
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

    }

    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jump != maxJump && wallJump > 0 && !GameManager.instance.IsOnMenu() && !GameManager.instance.IsOnDialogue() && movmentRestriction && !noInput)
        {
            StartCoroutine(NoInput());
            player.velocity = new Vector2(0, 0);
            player.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpForce, wallJumpForce), ForceMode2D.Impulse);
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            wallJump--;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && jump > 0 && !GameManager.instance.IsOnMenu() && !GameManager.instance.IsOnDialogue() && !noInput)
        {
            player.velocity = new Vector2(player.velocity.x, 0);
            player.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump--;
        }
    }
    public void JumpReset()
    {
        jump = maxJump;
    }

    void DashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dash && !GameManager.instance.IsOnMenu() && !GameManager.instance.IsOnDialogue() && GameManager.instance.ReturnAbilityValue("Dash") && !noInput)
        {
            //Frena el movimiento horizontal del jugador.
            player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            //Pequeño impulso hacia arriba para levantar al jugador del suelo.
            player.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            //Retraso de 0.04 para que de tiempo a levantar a jugador.
            Invoke("DoDash", 0.04f);
        }
    }

    void DoDash()
    {
        anime.SetBool("Dash", true);
        player.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        player.AddForce(new Vector2(scale.x * dashForce, 0), ForceMode2D.Impulse);

        // de esta forma se comprueba hacia el lado al que mira el jugador para saber que movimiento debe restringir
        if (scale.x < 0)
            movRestrictionL = true;
        else movRestrictionR = true;
        dash = false;
        AudioToPlay audio = GetComponent<AudioToPlay>();
        if (audio != null) audio.SendAudioToPlay();
        //Tiempo en realizar el dash 0.15.
        Invoke("DashCast", 0.15f);
    }

    void DashCast()
    {
        //Tras finalizar el dash.
        anime.SetBool("Dash", false);
        movRestrictionL = false;
        movRestrictionR = false;
        player.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    //Método que pasa el maxJump a 2
    public void DoubleJumpActive()
    {
        maxJump = 2;
    }

    IEnumerator NoInput()
    {
        noInput = true;
        yield return new WaitForSeconds(0.5f);
        noInput = false;
    }
}
