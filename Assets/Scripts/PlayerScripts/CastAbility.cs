using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAbility : MonoBehaviour {
    public GameObject fireBall,shield;
    public Transform projectilePool;
    bool fireBallOnCD, shieldOnCD, thunderBoldOnCD;
    public UIManager uIManager;

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
        if (Input.GetKeyDown(KeyCode.Q) && GameManager.instance.ReturnAbilityValue("Fireball") && !fireBallOnCD)
        {
            InstantiateFireBall();
            fireBallOnCD = true;
            Invoke("FireBallCD", GameManager.instance.ReturnCooldown("Fireball"));
        }
    }

    void ShieldInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.ReturnAbilityValue("Shield") && !shieldOnCD)
        {
            InstantiateShield();
            shieldOnCD = true;
            Invoke("ShieldCD", GameManager.instance.ReturnCooldown("Shield"));
        }
    }

    void ThunderboltInput()
    {
     //   if (Input.GetKeyDown(KeyCode.W) && GameManager.instance.GetAbility("Thunderbolt") && !thunderBoldOnCD)
        {
            InstantiateThunderbolt();
            thunderBoldOnCD = true;
            Invoke("ThunderboltCD", GameManager.instance.ReturnCooldown("Lightning"));
        }
    }

    //Metodos para instanciar las habilidades.
    void InstantiateFireBall()
    {
        GameObject newFireball = Instantiate(fireBall, transform.position, Quaternion.identity, projectilePool);
        Vector2 newDirection = transform.lossyScale.x * transform.right;

        newFireball.GetComponent<FireBall>().ChangeDirection(newDirection);
        uIManager.SetSliderValue(0f, "Fireball");
    }

    void InstantiateShield()
    {
        //El escudo se hace hijo del jugador para seguir su movimiento.
        GameObject newShield = Instantiate(shield, transform.position, Quaternion.identity, gameObject.transform);
        uIManager.SetSliderValue(0f, "Shield");
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
