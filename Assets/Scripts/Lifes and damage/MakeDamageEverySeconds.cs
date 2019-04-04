using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageEverySeconds : MonoBehaviour {

    public int damage;
    Collider2D collider;
    bool stay = false, alreadyInvoke = false;   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("esqueletinho");
        stay = true;
        GetCollider2D(collision);
        if(!alreadyInvoke)
        InvokeRepeating("MakeDamage", 0, 1);
        alreadyInvoke = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        stay = false;
    }
    void GetCollider2D(Collider2D collision)
    {
        collider = collision;
    }
    void MakeDamage()
    {
        if (stay) {
            if (collider.gameObject.GetComponent<Life>() != null && !GameManager.instance.GetInvulnerablePlayer()) collider.gameObject.GetComponent<Life>().LoseLife(damage);
        }

    }
}
