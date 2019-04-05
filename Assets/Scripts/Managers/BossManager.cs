using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public WolfEnums wolfState = WolfEnums.idle;
    public GameObject boss;
    public string bossName;
    // Use this for initialization
    void Start()
    {
        GameManager.instance.GetBossManager(this);
        InvokeRepeating("RandomAbility", 3, 2);
    }

    // Update is called once per frame
    void RandomAbility()
    {
        if (Random.Range(1, 7) <= 3)
        {
            ChangeBossState("wolf", "jump");
        }
        else
        {
            ChangeBossState("wolf", "charge");
        }
    }
    //devuelve el valor del booleano
    public WolfEnums WolfState()
    {
        return wolfState;
    }
    //cambia el estado del boss
    public void ChangeBossState(string bossName, string state)
    {
        //tomamos el nombre del boss y lo pasamos a minúsculas asi no debemos preocuparnos de si pusimos la mayúscula o no
        switch (bossName.ToLower())
        {
            //en caso de que sea el lobo
            //tomamos el nombre del ataque y lo pasamos a minúsculas asi no debemos preocuparnos de si pusimos la mayúscula o no
            case "wolf":
                switch (state.ToLower())
                {
                    case "jump":
                        if (wolfState == WolfEnums.idle)
                        {
                            wolfState = WolfEnums.jumping;
                            boss.GetComponent<JumpToPlayer>().DoJump();
                        }
                        break;
                    case "charge":
                        if (wolfState == WolfEnums.idle)
                        {
                            //La carga funciona diferente, necesitamos saber el enfriamiento de la carga antes de usarla (se entiendo viendo el script Charge).
                            if (!boss.GetComponent<Charge>().ChargeCD())
                            {
                                wolfState = WolfEnums.charging;
                                //Se inicia el enfriamiento de la carga.
                                boss.GetComponent<Charge>().StartChargeCD();
                            }
                        }
                        break;

                    case "idle":
                  
                        wolfState = WolfEnums.idle;
                        break;
                }
                break;
            //en caso de que sea la serpiente
            case "snake":
                break;
            default:
                break;
        }
    }
}
