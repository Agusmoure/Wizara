using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageEverySec : MonoBehaviour {

    public int damage;
    public float seconds;
    Collider2D triggerCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetCollider(other);
            InvokeRepeating("Damage", 0, seconds);
        }

    }
    //Método para realizar daño (también afecta a enemigos).
    void Damage()
    {
        Debug.Log("xd");
        if (triggerCollider.GetComponent<Life>() != null && !GameManager.instance.GetInvulnerablePlayer()) triggerCollider.GetComponent<Life>().LoseLife(damage);
    }
    //Se guarda other detectado por el trigger para que Damage lo pueda usar, ya que en Invoke no se puede pasar parámetros a los métodos.
    void GetCollider(Collider2D collider) {
        triggerCollider = collider;
    }
    //Al salir del ácido se detiene el invoke, permitiendo que se inicie otro si se entra de nuevo (de esta forma no se superponen).
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CancelInvoke("Damage");
        }
    }
}