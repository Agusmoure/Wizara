﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderManager : MonoBehaviour {
    public GameObject girl, boy;
    public string scene;
    // Use this for initialization
    void Start () {
        if (GameManager.instance.GetGender())
        {
            Destroy(boy);
            girl.SetActive(true);
            GameManager.instance.GetPlayer(girl);


        }
        else {
            Destroy(girl);
            boy.SetActive(true);
            GameManager.instance.GetPlayer(boy);

        }
    }
}
