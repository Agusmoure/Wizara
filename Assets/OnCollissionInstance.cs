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
        CompositeCollider2D compositeCollider2D = collision.GetComponent<CompositeCollider2D>();
        Vector3 position;
        if (compositeCollider2D != null)
        {
            //position = compositeCollider2D.gameObject.transform.position - Vector3.right * Mathf.Abs(compositeCollider2D.offset.x) - Vector3.up * Mathf.Abs(compositeCollider2D.offset.y) + Vector3.up * (obtener tamaño / 2);
        }
        BoxCollider2D collider2D = collision.GetComponent<BoxCollider2D>();
        if (collider2D != null)
            position = collider2D.gameObject.transform.position - Vector3.right * Mathf.Abs(collider2D.offset.x) - Vector3.up * Mathf.Abs(collider2D.offset.y) + Vector3.up * (collider2D.size.y / 2);
        else position = new Vector3(0, 0, 0);
        GameObject newGOgbject = Instantiate(gameObjectToInstance, position, Quaternion.identity, null);
        newGOgbject.transform.Rotate(Vector3.forward, 90);
    }
    public void InstanceTileMap( ContactPoint2D contact, Tilemap tile)
    {
        //Tras recibir el punto de contacto inicializamos ahí el objeto y lo rotamos 90º en el eje z
        Vector3 position;
        position = contact.point /*+ Vector2.up * (tile.tileAnchor.y / 2) no sé si deberiamos sumarle el ancho del tilemap*/;
        GameObject newGOgbject = Instantiate(gameObjectToInstance, position, Quaternion.identity, null);
        newGOgbject.transform.Rotate(Vector3.forward, 90);
    }
}
