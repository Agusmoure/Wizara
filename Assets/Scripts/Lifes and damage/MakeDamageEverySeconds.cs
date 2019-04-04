using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageEverySeconds : MonoBehaviour {

    public int damage;
    Collider2D collider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("esqueletinho");
        GetCollider2D(collision);
        Invoke("MakeDamage", 2);

    }
    void GetCollider2D(Collider2D collision)
    {
        collider = collision;
    }
    void MakeDamage()
    {
        if (collider.GetComponent<Life>() != null && !GameManager.instance.GetInvulnerablePlayer()) collider.GetComponent<Life>().LoseLife(damage);
    }
}
