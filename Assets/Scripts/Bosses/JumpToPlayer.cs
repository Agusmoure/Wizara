using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToPlayer : MonoBehaviour {
    public GameObject player;
    Vector2 playerPosition;
    bool jumpInCd=false;
    Rigidbody2D rigibody;
    public float angles;
	// Use this for initialization
	void Start () {
        rigibody = GetComponent<Rigidbody2D>();
    }
   
    // Update is called once per frame
    void Update () {

	}
    private void FixedUpdate()
    {
        if (!jumpInCd)
            rigibody.AddForce(Jump(), ForceMode2D.Impulse);
    }
    //método que obtiene la posición del jugador
    public void GetPlayerPosition()
    {
        playerPosition = player.transform.position;
    }
    /*Método que devuelve la fuerza que se le debe aplicar al objeto para realizar el movimiento parábolico para saltar hacia el jugador
    Fórmulas a tener en cuenta
    Nomenclatura:
    altura máxima(H)
    Velocidad(V)
    Velocidad en X(Vx)
    Velocidad en Y(Vy)
    gravedad(g)
    tiempo final(Tf)
    tiempo inicial(T0)
    tiempo de H (Ta)
    alcance (A)
    angulo(W)
    Fórmulas:
    H=(V^2* sen^2(W))/2g
    A=(V^2 *sen(2W)/g
    Tf=(2V*sen(W))/g
    Vx=V*cos(W)
    Vy=V*sen(W)
    Ta=Vy/g
    */
    Vector2 Jump()
    {
        GetPlayerPosition();
        //activa el CD del salto
        jumpInCd = true;
        //calcula la distancia en X
        float distance = Mathf.Abs(playerPosition.x) + Mathf.Abs(transform.position.x);
       float angle= GetAngleInRad(angles);
        //calcula la velocidad
        float speed = Mathf.Sqrt((-(Physics2D.gravity.y) * distance) / Mathf.Sin(angle*2));
        //asigna el vector de la fuerza que debe ejecutar
        Vector2 forces = new Vector2(-speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
        return forces;
    }
    //pasa el ángulo a Radianes
    float GetAngleInRad(float angleInGrades)
    {
        float angle = (angleInGrades * Mathf.PI) / 180;
        return angle;
    }
}
