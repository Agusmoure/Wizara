using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public WolfEnums wolfState = WolfEnums.idle;
    // Use this for initialization
    void Start()
    {
        GameManager.instance.GetBossManager(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //devuelve el valor del booleano
    public WolfEnums WolfState()
    {
        return wolfState;
    }
    //cambia el estado del boss
    public void ChangeBossState(string bossName, string attack)
    {
        //tomamos el nombre del boss y lo pasamos a minúsculas asi no debemos preocuparnos de si pusimos la mayúscula o no
        switch (bossName.ToLower())
        {
            //en caso de que sea el lobo
            //tomamos el nombre del ataque y lo pasamos a minúsculas asi no debemos preocuparnos de si pusimos la mayúscula o no
            case "wolf":
                switch (attack.ToLower())
                {
                    case "jump":
                        wolfState = WolfEnums.jumping;
                        break;
                    case "charge":
                        wolfState = WolfEnums.charging;
                        break;
                    default:
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
