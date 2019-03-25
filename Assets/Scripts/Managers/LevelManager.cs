﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //Instancia del jugador para obtener sus componentes.
    public GameObject camara;
    GameObject player;
    void Start()
    {
        //Indica al GameManager cual es el LevelManager.
        GameManager.instance.GetLevelManager(this);
        player = GameManager.instance.ReturnPlayer();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void GetCamera(GameObject thisCamera)
    {
        camara = thisCamera;
    }

    public void MoveCamera(Vector2 thisRoom)
    {
        camara.GetComponent<CameraMovement>().MoveCamera(thisRoom);
    }

    public void TeleportCamera(Vector2 thisRoom)
    {
        camara.GetComponent<CameraMovement>().TeleportCameraToRoom(thisRoom);
    }

    //Método que recoge la cantidad de daño que recibirá el jugador y llama al método del jugador que quita vida.
    public void PlayerGetDamage(int damage)
    {
        player.GetComponent<Life>().LoseLife(damage);
    }

    public void SpawnPlayer()
    {
        if (GameManager.instance.ReturnCurrentCheckpointPosition() != null && GameManager.instance.ReturnCurrentCheckpointRoomPosition() != null && player != null)
        {
            player.transform.position = new Vector2(GameManager.instance.ReturnCurrentCheckpointPosition().x, GameManager.instance.ReturnCurrentCheckpointPosition().y);
            MoveCamera(GameManager.instance.ReturnCurrentCheckpointRoomPosition());
            TeleportCamera(GameManager.instance.ReturnCurrentCheckpointRoomPosition());
        }
    }
}
