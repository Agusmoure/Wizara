using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class AumentHeightEveryXseconds : MonoBehaviour
{
    //le pasamos cuanto aumenta y cada cuanto tiempo
    public float seconds, aument;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Aument", 0, seconds);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponentInChildren<ParticleSystem>() != null) GetComponentInChildren<ParticleSystem>().Play();  

        Instance inst = GetComponent<Instance>();
        if (inst != null)
        {
            if (collision.gameObject.GetComponent<TilemapCollider2D>()!=null)
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    Vector2 hitPoint = contact.point;
                    if (hitPoint.y<transform.position.y) {
                        //Instantiate(explosion, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
                        inst.Instantiate(hitPoint);
                    }
                    
                }
            else
            {
                ContactPoint2D[] contact = new ContactPoint2D[1];
                 collision.GetContacts(contact);
                inst.Instantiate(contact[0].point);
            }
                Destroy(gameObject);
        }

        else Destroy(gameObject);
    }
    void Aument()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float aumented = sprite.size.y + aument;
        sprite.size = new Vector2(sprite.size.x, aumented);
        transform.position = transform.position + Vector3.down * aument / 2;
    }
}
