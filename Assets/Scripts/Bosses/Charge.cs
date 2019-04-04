using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {
    public int speed, time;
    public string bossName;
    Rigidbody2D rigidB;
    bool ChargeCD = false;
    Vector2 charge,starter;
	// Use this for initialization
	void Start () {
        rigidB = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        LooktoPlayer(out starter);
        Debug.DrawRay(starter, charge, Color.yellow);
    }

    //para la carga
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (GameManager.instance.ReturnBossManager().WolfState()==WolfEnums.charging)
        GameManager.instance.ReturnBossManager().ChangeBossState(bossName, "stop");
        rigidB.velocity = new Vector2(0,0);
    }
    //Cambia el CD
    void ChangeCD()
    {
        ChargeCD = false;
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
    public void DoCharge()
    {
        if (!ChargeCD)
        {
            ChargeCD = true;
            rigidB.AddForce(charge * speed * rigidB.mass, ForceMode2D.Force);
            Invoke("ChangeCD", 2);
        }
    }
}
