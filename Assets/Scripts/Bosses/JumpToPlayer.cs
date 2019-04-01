using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToPlayer : MonoBehaviour {
    GameObject player;
    Vector2 playerPosition;
    bool jumpInCd=false;
    Rigidbody2D rigibody;
    public float angles;
    public float jumpCD=4;
	// Use this for initialization
	void Start () {
        player = GameManager.instance.ReturnPlayer();
        rigibody = GetComponent<Rigidbody2D>();
    }
   
    private void FixedUpdate()
    {
        if (!jumpInCd)
        {
            rigibody.AddForce(Jump(), ForceMode2D.Impulse);
            Invoke("JumpCD",jumpCD);
        }
            
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
        //guarda la posicion del jugador al inicio del salto.
        playerPosition = player.transform.position;
        Debug.Log(playerPosition);
        //activa el CD del salto
        jumpInCd = true;
        float angle, speed;
        CalculateValues(out angle, out speed);
        //asigna el vector de la fuerza que debe ejecutar
        Vector2 forces = new Vector2(-speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
        return forces;
    }

    //método para calcular tanto ángulo como velocidad.
    private void CalculateValues(out float angle, out float speed)
    {
        //calcula la distancia en X
        float distance = Mathf.Abs(playerPosition.x) + Mathf.Abs(transform.position.x);
        angle = GetAngleInRad(angles);
        //calcula la velocidad
        speed = Mathf.Sqrt((-(Physics2D.gravity.y) * distance) / Mathf.Sin(angle * 2));
    }

    //pasa el ángulo a Radianes
    float GetAngleInRad(float angleInGrades)
    {
        float angle = (angleInGrades * Mathf.PI) / 180;
        return angle;
    }
    void JumpCD()
    {
        jumpInCd = false;
    }
}
