﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.T))
        {
            GameManager.instance.ActivateAll();
            GameManager.instance.ReturnUIManager().EnableAbilityIcons();
        }
        if (Input.GetKey(KeyCode.D))
        {
            GameManager.instance.SetLevelManager().OpenDoors();
        }
    }
}
