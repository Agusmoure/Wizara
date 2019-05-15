using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastAbility : MonoBehaviour {

    public GameObject fireBall, shield, lighting;
    bool fireBallOnCD, shieldOnCD, thunderBoldOnCD;
    public UIManager uIManager;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

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
        if (Input.GetKeyDown(KeyCode.Q) && GameManager.instance.ReturnAbilityValue("Fireball") && !fireBallOnCD && !GameManager.instance.IsOnMenu() && !GameManager.instance.IsOnDialogue())
        {
            if(!GameManager.instance.GetGender())
            anim.Play("PlayerShoot");
            else anim.Play("FemaleShot");
            InstantiateFireBall();
            fireBallOnCD = true;
            Invoke("FireBallCD", GameManager.instance.ReturnCooldown("Fireball"));
        }
    }

    void ShieldInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.ReturnAbilityValue("Shield") && !shieldOnCD && !GameManager.instance.IsOnMenu() && !GameManager.instance.IsOnDialogue())
        {
            InstantiateShield();
            shieldOnCD = true;
            Invoke("ShieldCD", GameManager.instance.ReturnCooldown("Shield"));
        }
    }

    void ThunderboltInput()
    {
      if (Input.GetKeyDown(KeyCode.W) && GameManager.instance.ReturnAbilityValue("Lightning") && !thunderBoldOnCD)
        {
            InstantiateThunderbolt();
            thunderBoldOnCD = true;
            Invoke("ThunderboltCD", GameManager.instance.ReturnCooldown("Lightning"));
        }
    }

    //Metodos para instanciar las habilidades.
    void InstantiateFireBall()
    {
        GameObject newFireball = Instantiate(fireBall, transform.position, Quaternion.identity, GameManager.instance.ReturnPoolManager().GetProjectilePool());
        Vector2 newDirection = transform.lossyScale.x * transform.right;

        newFireball.GetComponent<FireBall>().ChangeDirection(newDirection);
        uIManager.SetSliderValue(0f, "Fireball");
        //uIManager.PressButton("Fireball");
    }

    void InstantiateShield()
    {
        //El escudo se hace hijo del jugador para seguir su movimiento.
        GameObject newShield = Instantiate(shield, transform.position, Quaternion.identity, gameObject.transform);
        uIManager.SetSliderValue(0f, "Shield");
        //uIManager.PressButton("Shield");
    }

    void InstantiateThunderbolt()
    {
        int layerMask = 1 << 21;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position,Vector2.up,80,layerMask);
        //Si choca con algo 
        if (hit2D.collider != null)
        {
            GameObject newLighting = Instantiate(lighting, hit2D.point+Vector2.down*0.1f, Quaternion.identity, GameManager.instance.ReturnPoolManager().GetProjectilePool());

            //muestra en el editor una linea que cubre toda la pantalla
            Debug.DrawLine(hit2D.point, hit2D.point + 10 * Vector2.down, Color.yellow,5);
        }
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
