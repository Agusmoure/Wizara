using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionChangeExecuting : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameManager.instance.ReturnBossManager().Executing())
        GameManager.instance.ReturnBossManager().ChangeExecuting();
    }
}
