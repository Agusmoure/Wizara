﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentHeightEveryXseconds : MonoBehaviour
{
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
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 hitPoint = contact.point;
                //Instantiate(explosion, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
                inst.Instantiate(hitPoint);
            }
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
