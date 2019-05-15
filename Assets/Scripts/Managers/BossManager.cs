using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public WolfEnums wolfState = WolfEnums.idle;
    public WizardEnums wizardState = WizardEnums.idle;
    public GameObject boss;
    public string bossName;
    public float bossWaitTime = 5;
    Life bossLife;
    // Use this for initialization
    void Start()
    {
        GameManager.instance.GetBossManager(this);
        bossLife = boss.GetComponentInChildren<Life>();
        if (bossName == "wolf")
        {
            //Inicia habilidades en 3 segundos. Cada 2 segundos intenta ejecutar una habilidad.
            InvokeRepeating("RandomAbility", bossWaitTime, 3);
            //Para evitar que se quede bloqueado.
            InvokeRepeating("WolfToIdle", 0, 3.5f);
        }
        else WizardAbilities();
        

    }
    // Update is called once per frame
    void RandomAbility()
    {
        if(bossLife.GetActualLife()>0)
        if (Random.Range(1, 7) <= 3)
        {
            ChangeBossState("wolf", "jump");
        }
        else
        {
            ChangeBossState("wolf", "charge");
        }
    }
    void WizardAbilities()
    {
        InvokeRepeating("CreateFireball", 1, 2);
    }
    void CreateFireball()
    {
       ChangeBossState("wizard", "fireball");
        Debug.Log(bossLife.GetActualLife());
       if (bossLife.GetActualLife() <= 90) CancelInvoke("CreateFireball");
    }
    //Comprueba que esta saltando, si es asi, espera un par de segundos para que aterrice y vuelve al estado Idle.
    void WolfToIdle()
    {
        if (wolfState == WolfEnums.jumping)
        {
            Invoke("WolfIdleWait", 2);
        }
    }
    void WolfIdleWait()
    {
        wolfState = WolfEnums.idle;
    }
    //devuelve el valor del enum
    public WizardEnums WizardState()
    {
        return wizardState;
    }
    //devuelve el valor del enum
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
            //en caso de que sea el mago
            case "wizard":
                switch (state.ToLower())
                {
                    case "storm":
                        if (wizardState == WizardEnums.idle)
                        {
                        boss.GetComponent<MutipleLightings>().Storm();
                        wizardState = WizardEnums.storm;
                        }
                        break;
                    case "flying":
                        if (wizardState == WizardEnums.idle)
                        {
                            boss.GetComponent<EnemyLighting>().SoltarRayo();
                            wizardState = WizardEnums.flying;
                        }
                        break;
                    case "fireball":
                        if (wizardState == WizardEnums.idle)
                        {
                            boss.GetComponent<BossFireball>().Create();
                            wizardState = WizardEnums.fireball;
                        }
                        break;
                    case "idle":
                        wizardState = WizardEnums.idle;
                        break;
                }
                break;
            default:
                break;
        }
    }
}