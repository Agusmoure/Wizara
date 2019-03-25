using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour {

    //Room1: Izquierda. Room2: Derecha.
    public GameObject room;

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        GameManager.instance.SetLevelManager().MoveCamera(new Vector2 (room.transform.position.x, room.transform.position.y));
    }
}
