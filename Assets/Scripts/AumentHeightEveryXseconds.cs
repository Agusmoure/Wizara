using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Instance inst = GetComponent<Instance>();
        if (inst != null)
        {
            Vector2 hitPoint= new Vector2(0,0);
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if(hitPoint==new Vector2(0,0))
                 hitPoint = contact.point;

            }
            inst.Instantiate(hitPoint);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Aument()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float aumented = sprite.size.y + aument;
        transform.position = transform.position + Vector3.down * aument / 2;

        sprite.size = new Vector2(sprite.size.x, aumented);
    }
}
