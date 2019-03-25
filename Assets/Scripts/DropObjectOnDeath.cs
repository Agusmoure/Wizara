using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectOnDeath : MonoBehaviour {

    public int probability;
    public GameObject droppedObject;
    public Transform objectPool;

    public void DropObject()
    {
        int aleatority = Random.Range(0, 101);

        if (aleatority <= probability)
        {
            GameObject newObject = Instantiate(droppedObject, transform.position, transform.rotation, objectPool);
        }
    }
}
