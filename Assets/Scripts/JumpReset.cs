using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpReset : MonoBehaviour {
    PlayerMovement player;
    // Use this for initialization
    void Start () {
        player = GetComponentInParent<PlayerMovement>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        if (contact.normal.y > 0.9 && contact.normal.y < 1.1)
        {
            player.JumpReset();
        }
    }
}
