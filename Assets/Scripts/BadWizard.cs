using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadWizard : MonoBehaviour
{
    public float distance;
    public int Cooldown;
    public Transform projectilePool;
    public FireBall fireBall;
    int layermask;
    bool isActive;
    SpriteRenderer sp;
    Vector2 direction;

    // Use this for initialization
    void Start()
    {
        layermask = 1 << 8;
        isActive = false;
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Flip();
        Shoot();
    }

    void InstantiateFireBall()
    {
        FireBall newFireBall = Instantiate(fireBall.GetComponent<FireBall>(), transform.position, Quaternion.identity, projectilePool);
        newFireBall.ChangeDirection(direction);
    }

    void Shoot()
    {
        if (!sp.flipX) // Mira a la Izquierda
        {
            if (Physics2D.Raycast(transform.position, -transform.right, distance, layermask) && !isActive)
            {
                direction = -transform.right;
                InstantiateFireBall();
                isActive = true;
                Invoke("FireBallCD", Cooldown);
            }
        }
        if (sp.flipX) // Mira a la Derecha
        {
            if (Physics2D.Raycast(transform.position, transform.right, distance, layermask) && !isActive)
            {
                direction = transform.right;
                InstantiateFireBall();
                isActive = true;
                Invoke("FireBallCD", Cooldown);
            }
        }
    }

    void FireBallCD()
    {
        isActive = !isActive;
    }

    void Flip()
    {

        if (Physics2D.Raycast(transform.position, transform.right, distance, layermask) && !sp.flipX)
        {
            sp.flipX = true;

        }
        else if (Physics2D.Raycast(transform.position, -transform.right, distance, layermask) && sp.flipX)
        {
            sp.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 towards = new Vector2(10, -10);
        if (Mathf.Abs(Vector2.Angle(collision.GetContact(0).normal, Vector2.up) - 180) < 0.5)
        {
            if (sp.flipX)
                towards = new Vector2(towards.x, towards.y * collision.GetContact(0).normal.y);
            else if (!sp.flipX)
                towards = new Vector2(towards.x, towards.y * collision.GetContact(0).normal.y);

            collision.gameObject.GetComponent<Bounce>().PlayKnockback(towards); //Reproduce el knockback. 
        }
    }
}