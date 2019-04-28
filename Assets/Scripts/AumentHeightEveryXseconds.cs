using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AumentHeightEveryXseconds : MonoBehaviour
{
    public float seconds, aument;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Aument", 0, seconds);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instance inst = GetComponent<Instance>();
        if (inst != null)
        {
            //Guardamos el tilemap collider del objeto con el que collisionamos
            TilemapCollider2D tilemapCollider = collision.gameObject.GetComponent<TilemapCollider2D>();
            //inicializamos el tilemap y los puntos de contacto
            Tilemap tile = null;
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            if (tilemapCollider != null)
            {
                tile = tilemapCollider.gameObject.GetComponent<Tilemap>();
                contacts = new ContactPoint2D[1];
                tilemapCollider.GetContacts(contacts);
                Debug.Log(contacts[0].point);
                inst.InstanceTileMap(contacts[0], tile);
            }
            else
            inst.InstanceNoTileMap(collision.gameObject);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Aument()
    {

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float aumented = sprite.size.y + aument;
        sprite.size = new Vector2(sprite.size.x, aumented);
        transform.position = transform.position + Vector3.down * aument / 2;
    }
}
