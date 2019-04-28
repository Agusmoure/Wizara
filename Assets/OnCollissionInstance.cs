using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour {
    public GameObject gameObjectToInstance;
    ContactPoint2D[] contacts = new ContactPoint2D[1];
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.GetContacts(contacts);

    }
    public void InstanceThis() {
        Debug.Log(contacts[0].point);

        GameObject newGOgbject = Instantiate(gameObjectToInstance, contacts[0].point, Quaternion.identity, null);
        newGOgbject.transform.Rotate(Vector3.forward, 90);
    }
}
