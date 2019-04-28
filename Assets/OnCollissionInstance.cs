using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instance : MonoBehaviour {
    public GameObject gameObjectToInstance;
    public void InstanceThis(GameObject collision) {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        BoxCollider2D collider2D = collision.GetComponent<BoxCollider2D>();
        Vector3 position;
        if (collider2D != null)
            position = collider2D.gameObject.transform.position-Vector3.right*Mathf.Abs(collider2D.offset.x)-Vector3.up* Mathf.Abs(collider2D.offset.y) + Vector3.up * (collider2D.size.y / 2);
        else
        {
            if (collider2D = null)
            {
                CompositeCollider2D compositeCollider2D = collision.GetComponent<CompositeCollider2D>();
                position = new Vector3(0, 0, 0);
            }
            else position = new Vector3(0, 0, 0);
        }

        GameObject newGOgbject = Instantiate(gameObjectToInstance, position, Quaternion.identity, null);
        newGOgbject.transform.Rotate(Vector3.forward, 90);
        //collider2D.size.y
    }
}
