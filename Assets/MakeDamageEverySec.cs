using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageEverySec : MonoBehaviour {

    public int damage;
    public float seconds;
    Collider2D collider;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("xd");
        GetCollider(collision);
        InvokeRepeating("Damage", 0, seconds);
    }
    void Damage()
    {
        if (collider.GetComponent<Life>() != null && !GameManager.instance.GetInvulnerablePlayer()) collider.GetComponent<Life>().LoseLife(damage);
    }
    void GetCollider(Collider2D coll) {
        collider = coll;
    }
}