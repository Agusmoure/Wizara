using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {
   public bool executingAttack=false;
	// Use this for initialization
	void Start () {
        GameManager.instance.GetBossManager(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //devuelve el valor del booleano
    public bool Executing()
    {
        return executingAttack;
    }
    //cambia el valor del booleano
    public void ChangeExecuting()
    {
        executingAttack = !executingAttack;
    }
}
