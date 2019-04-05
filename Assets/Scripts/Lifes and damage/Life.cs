using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

    public int lifePoints;
 int actualLife;

    private void Start()
    {
        SetLife(lifePoints);
    }

    //Método que quita vida al jugador.
    public void LoseLife(int damage)
    {
        actualLife -= damage;

        //Cuando la vida sea 0 o menor, el jugador muere.
        if (actualLife <= 0) Dead();

        if (tag == "Player") GameManager.instance.ReturnUIManager().UpdateLifeUI();
    }

    //Método que destruye al jugador al morir (es llamado por GM).
    public void Dead()
    {
        DropObjectOnDeath drop = GetComponent<DropObjectOnDeath>();
        if (drop != null) drop.DropObject();
        DestroyParent destroy = GetComponent <DestroyParent>();
        if (destroy != null) destroy.DestroyP();
        if (tag == "Player") GameManager.instance.Respawn();
        else Destroy(gameObject);
    }

    //Aumenta la vida del jugador 
    public void IncreaseLife(int increase)
    {
        if ((actualLife + increase) > lifePoints)
        {
            actualLife = lifePoints;
        }

        else actualLife += increase;

        if (tag == "Player") GameManager.instance.ReturnUIManager().UpdateLifeUI();
    }

    //devuelve la vida actual
    public int GetActualLife()
    {
        return actualLife;
    }

    //set actual life se hace en el GM para que no se ponga full vida siempre que cambie de pantalla.
    public void SetLife(int life)
    {
        actualLife = life;
    }
}
