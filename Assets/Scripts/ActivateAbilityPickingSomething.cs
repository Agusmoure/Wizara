using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAbilityPickingSomething : MonoBehaviour {
    public string abilityName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.SetAbilityTrue(abilityName);
        Destroy(gameObject);    
    }
}
