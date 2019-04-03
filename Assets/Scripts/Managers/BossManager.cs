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
    //cambia el valor del booleano
    public void ChangeBossState(string bossName, string attack)
    {
        switch (bossName.ToLower())
        {
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
        }
    }
}
