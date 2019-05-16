using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class AumentHeightEveryXseconds : MonoBehaviour
{
    //le pasamos cuanto aumenta y cada cuanto tiempo
    public float seconds, aument;
    bool noCollision = true;
    // Use this for initialization
    void Start()
    {
        if (GetComponent<AudioToPlay>() != null) GetComponent<AudioToPlay>().SendAudioToPlay();
        StartCoroutine(Aument());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponentInChildren<ParticleSystem>() != null) GetComponentInChildren<ParticleSystem>().Play();
        noCollision = false;

        InstantiateObject inst = GetComponent<InstantiateObject>();
        if (inst != null)
        {
            if (collision.gameObject.GetComponent<TilemapCollider2D>() != null)
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    Vector2 hitPoint = new Vector2 (contact.point.x, contact.point.y + 0.2f);
                    if (hitPoint.y < transform.position.y)
                    {
                        //Instantiate(explosion, new Vector3(hitPoint.x, hitPoint.y, 0), Quaternion.identity);
                        inst.Instantiate(hitPoint);
                        Invoke("DestroyThis", 0.4f);
                    }
                }
            }

            else
            {
                ContactPoint2D[] contact = new ContactPoint2D[1];
                 collision.GetContacts(contact);
                inst.Instantiate(new Vector2 (contact[0].point.x, contact[0].point.y + 0.4f));
                Destroy(gameObject);
            }
        }

        else Destroy(gameObject);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

    IEnumerator Aument()
    {
        while (noCollision)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            float aumented = sprite.size.y + aument;
            sprite.size = new Vector2(sprite.size.x, aumented);
            transform.position = transform.position + Vector3.down * aument / 2;
            yield return new WaitForSeconds(seconds);
        }
    }
}
