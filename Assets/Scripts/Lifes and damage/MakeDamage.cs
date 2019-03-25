using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour {

    // Realiza daño al jugador al colisionar.
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Life>() != null) collision.GetComponent<Life>().LoseLife(damage);
    }
}
