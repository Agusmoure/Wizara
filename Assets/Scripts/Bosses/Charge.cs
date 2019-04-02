using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {
    public int speed, time;
    Rigidbody2D rigidB;
    bool cD = true;
    Vector2 charge,starter;
	// Use this for initialization
	void Start () {
        rigidB = GetComponent<Rigidbody2D>();
        Invoke("ChangeCD", 2);
    }
    private void Update()
    {
        LooktoPlayer(out starter);
        Debug.DrawRay(starter, charge, Color.yellow);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!GameManager.instance.ReturnBossManager().Executing() && !cD)
        {
            //cambia el CD y lo activa, le dice al BossManager que esta atacando y carga en la direccion dada
            ChangeCD();
            GameManager.instance.ReturnBossManager().ChangeExecuting();
            // rigidB.AddForce(Vector2.left * speed*rigidB.mass, ForceMode2D.Impulse);
            rigidB.velocity = charge * speed;
            Invoke("ChangeCD",time);
        }
    }
    //para la carga
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (GameManager.instance.ReturnBossManager().Executing())
        GameManager.instance.ReturnBossManager().ChangeExecuting();
        rigidB.velocity = new Vector2(0,0);
    }
    //Cambia el CD
    void ChangeCD()
    {
        cD = !cD;
    
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
}
