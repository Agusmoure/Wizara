using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAbility : MonoBehaviour {
    public FireBall fireBall;
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
      //  if (Input.GetKeyDown(KeyCode.W) && GameManager.instance.GetAbility("Shield") && !shieldOnCD)
        {
            InstantiateShield();
            shieldOnCD = true;
            Invoke("ShieldCD", GameManager.instance.ReturnCooldown("Shield"));
        }
    }

    void ThunderboltInput()
    {
     //   if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.GetAbility("Thunderbolt") && !thunderBoldOnCD)
        {
            InstantiateShield();
            thunderBoldOnCD = true;
            Invoke("ThunderboltCD", GameManager.instance.ReturnCooldown("Lightning"));
        }
    }

    //Metodos para instanciar las habilidades.
    void InstantiateFireBall()
    {
        FireBall newFireBall = Instantiate(fireBall.GetComponent<FireBall>(), transform.position, Quaternion.identity, projectilePool);
        Vector2 newDirection = transform.lossyScale.x * transform.right;
        newFireBall.ChangeDirection(newDirection);
        uIManager.SetSliderValue(0f, "Fireball");
    }

    void InstantiateShield()
    {

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
