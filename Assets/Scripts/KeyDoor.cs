using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour {
    bool locked = true;
	public void Unlock()
    {
        locked = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el método Unlock ha sido llamado por la llave y se cambió el booleano locked, la puerta se podrá abrir en colisión.
        if (!locked)
        {
            Destroy(gameObject);
        }
    }
}
