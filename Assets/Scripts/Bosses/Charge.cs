﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {
    public int speed, chargeCD=2;
    Rigidbody2D rigidB;
    bool chargeOnCD = false;
    Vector2 charge,starter,chargeDirection;
	// Use this for initialization
	void Start () {
        rigidB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        LooktoPlayer(out starter);
        Debug.DrawRay(starter, charge, Color.yellow);
    }
    void FixedUpdate()
    {
        if(GameManager.instance.ReturnBossManager().WolfState() == WolfEnums.charging)
        {
            rigidB.AddForce(chargeDirection * speed * rigidB.mass);
        }
    }
    //Cambia el CD
    void ChangeCD()
    {
        chargeOnCD = false;
    }
    //COmprueba hacia que lado esta el jugador y carga hacia él
    void LooktoPlayer(out Vector2 starter)
    {
        //obtenemos la layerMask del jugador
        int layerMask = 1 << 8;
        //establecemos desde donde saldrá el raycast
    starter = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2);
        //comprobamos en que lado esta el jugador y miramos y establecemos el lado de carga
        if (Physics2D.Raycast(starter, Vector2.right, 3000, layerMask))
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
            charge = Vector2.right;
        }
        else if (Physics2D.Raycast(starter, Vector2.left, 3000, layerMask)) 
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
            charge = Vector2.left;
        }
    }
    public void StartChargeCD()
    {
     
            chargeDirection = charge;
            chargeOnCD = true;
            Invoke("ChangeCD", chargeCD);
        
    }
    public bool ChargeCD()
    {
        return chargeOnCD;
    }
}
