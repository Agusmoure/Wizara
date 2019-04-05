using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour {
    public float animationTime;
    public int speed, chargeCDTime=2;
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
        //Realizará la carga siempre que el estado del boss sea charging.
        if(GameManager.instance.ReturnBossManager().WolfState() == WolfEnums.charging)
        {
            rigidB.AddForce(chargeDirection * speed * rigidB.mass);
        }
    }
    //Comprueba hacia que lado esta el jugador para dirigir su mirada hacia él y saber el lado de la carga.
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
    //Método llamado por el bossManager que inicia el tiempo de enfriamiento.
    public void StartChargeCD()
    {
        //Aqui se inicia la animación.
            Invoke("AnimationTime",animationTime);
            chargeDirection = charge;
            chargeOnCD = true;
            Invoke("ChangeCD", chargeCDTime);
    }
    //Cambia el CD para que la habilidad vuelva a estar disponible tras el tiempo "chargeCDTime".
    void ChangeCD()
    {
        chargeOnCD = false;
    }
    //Este método permite ver al bossManager si la carga está en cooldown, para no empezar otra al mismo tiempo.
    public bool ChargeCD()
    {
        return chargeOnCD;
    }
}
