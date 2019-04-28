using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Instance : MonoBehaviour
{
    public GameObject gameObjectToInstance;
    public void InstanceNoTileMap(GameObject collision)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        BoxCollider2D collider2D = collision.GetComponent<BoxCollider2D>();
        Vector3 position;
        if (collider2D != null)
            position = collider2D.gameObject.transform.position - Vector3.right * Mathf.Abs(collider2D.offset.x) - Vector3.up * Mathf.Abs(collider2D.offset.y) + Vector3.up * (collider2D.size.y / 2);
        else position = new Vector3(0, 0, 0);
        GameObject newGOgbject = Instantiate(gameObjectToInstance, position, Quaternion.identity, null);
        newGOgbject.transform.Rotate(Vector3.forward, 90);
    }
    public void InstanceTileMap( ContactPoint2D contact, Tilemap tile)
    {
        Vector3 position;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        position = contact.point + Vector2.up * (tile.tileAnchor.y / 2);
        GameObject newGOgbject = Instantiate(gameObjectToInstance, position, Quaternion.identity, null);
        newGOgbject.transform.Rotate(Vector3.forward, 90);
    }
}
