using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour {

    // Realiza daño al jugador al colisionar.
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Life>() != null && !GameManager.instance.GetInvulnerablePlayer()) collision.GetComponent<Life>().LoseLife(damage);
        }

        else if (collision.CompareTag("Boss") && collision.transform.parent.GetComponentInChildren<Life>() != null) collision.transform.parent.GetComponentInChildren<Life>().LoseLife(damage);

        else if (collision.GetComponent<Life>() != null) collision.GetComponent<Life>().LoseLife(damage);
    }
}
