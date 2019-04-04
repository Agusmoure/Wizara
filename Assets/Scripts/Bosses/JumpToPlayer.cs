using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpToPlayer : MonoBehaviour
{
    GameObject player;
    Vector2 playerPosition;
    public string bossName;
    public bool jumpOnCd = true;

    Rigidbody2D rigibody;
    public float angles;
    public float jumpCD = 4;
    // Use this for initialization
    void Start()
    {

        rigibody = GetComponent<Rigidbody2D>();
        Invoke("JumpCD", jumpCD);
    }

    public void DoJump()
    {
        jumpOnCd = true;
        Vector2 jump = Jump();
        rigibody.AddForce(jump, ForceMode2D.Impulse);
        Invoke("JumpCD", jumpCD);
    }
    Vector2 Jump()
    {
        player = GameManager.instance.ReturnPlayer();
        //guarda la posicion del jugador al inicio del salto.
        playerPosition = player.transform.position;
        //activa el CD del salto
        jumpOnCd = true;
        float angle, speed;
        CalculateValues(out angle, out speed);
        //asigna el vector de la fuerza que debe ejecutar teniendo en cuenta hacia donde la debe ejecutar
        Vector2 forces;
        if (playerPosition.x < transform.position.x)
            forces = new Vector2(-speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
        else
            forces = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
        return forces;
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

    método para calcular tanto ángulo como velocidad.
    */
    private void CalculateValues(out float angle, out float speed)
    {
        //calcula la distancia en X teniendo en cuenta las 3 posibilidades
        float playerX = playerPosition.x, thisX = transform.position.x, distance;
        if (playerX > 0 && thisX < 0 || thisX > 0 && playerX < 0)
        {
            distance = Mathf.Abs(playerX) + Mathf.Abs(thisX);
        }
        else if (Mathf.Abs(thisX) > Mathf.Abs(playerX)) distance = Mathf.Abs(thisX) - Mathf.Abs(playerX);
        else distance = Mathf.Abs(playerX) - Mathf.Abs(thisX);

        angle = GetAngleInRad(angles);
        //calcula la velocidad
        speed = Mathf.Sqrt((-(Physics2D.gravity.y) * distance) / Mathf.Sin(angle * 2)) * rigibody.mass;
    }

    //pasa el ángulo a Radianes
    float GetAngleInRad(float angleInGrades)
    {
        float angle = (angleInGrades * Mathf.PI) / 180;
        return angle;
    }
    //cambia el valor del booleano a falso;
    void JumpCD()
    {
        jumpOnCd = false;
    }
}
