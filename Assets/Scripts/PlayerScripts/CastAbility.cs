using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAbility : MonoBehaviour {
    public GameObject fireBall,shield;
    public Transform projectilePool;
    public float fireBallCooldown, shieldCooldown, thunderboltCooldown;
    bool fireBallOnCD, shieldOnCD, thunderBoldOnCD;

    // Update is called once per frame
    void Update()
    {
        FireBallInput();
        ShieldInput();
        ThunderboltInput();
    }

    //Metodos que recogen el input de cada habilidad.
    public void FireBallInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GameManager.instance.GetAbility("Fireball") && !fireBallOnCD)
        {
            InstantiateFireBall();
            fireBallOnCD = true;
            Invoke("FireBallCD", fireBallCooldown);
        }
    }

    void ShieldInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && GameManager.instance.GetAbility("Shield") && !shieldOnCD)
        {
            InstantiateShield();
            shieldOnCD = true;
            Invoke("ShieldCD", shieldCooldown);
        }
    }

    void ThunderboltInput()
    {
     //   if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.GetAbility("Thunderbolt") && !thunderBoldOnCD)
        {
            InstantiateThunderbolt();
            thunderBoldOnCD = true;
            Invoke("ThunderboltCD", thunderboltCooldown);
        }
    }

    //Metodos para instanciar las habilidades.
    void InstantiateFireBall()
    {
        GameObject newFireball = Instantiate(fireBall, transform.position, Quaternion.identity, projectilePool);
        Vector2 newDirection = transform.lossyScale.x * transform.right;
        newFireball.GetComponent<FireBall>().ChangeDirection(newDirection);
    }

    void InstantiateShield()
    {
        //El escudo se hace hijo del jugador para seguir su movimiento.
        GameObject newShield = Instantiate(shield,transform.position, Quaternion.identity, gameObject.transform);
    }

    void InstantiateThunderbolt()
    {

    }

    //Metodos para control de tiempo de enfriamiento.
    void FireBallCD()
    {
        fireBallOnCD = !fireBallOnCD;
    }
    void ShieldCD()
    {
        shieldOnCD = !shieldOnCD;
    }
    void ThunderboltCD()
    {
        thunderBoldOnCD = !thunderBoldOnCD;
    }
}
