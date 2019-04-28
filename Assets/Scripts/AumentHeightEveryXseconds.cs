using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentHeightEveryXseconds : MonoBehaviour {
    public float seconds, aument;
	// Use this for initialization
	void Start () {
        InvokeRepeating("Aument", 0, seconds);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionStay2D(Collision2D collision)
    {
        Instance inst = GetComponent<Instance>();
        if (inst != null)
        {
            inst.InstanceThis(collision.gameObject);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Instance inst = GetComponent<Instance>();
        if (inst != null)
        {
            inst.InstanceThis(collision.gameObject);
            Destroy(gameObject);
        }
        else
        Destroy(gameObject);
    }
    void Aument()
    {
    
        SpriteRenderer sprite = GetComponent < SpriteRenderer > ();
        float aumented = sprite.size.y + aument;
        sprite.size = new Vector2(sprite.size.x, aumented);
        transform.position = transform.position + Vector3.down * aument / 2;
    }
}
