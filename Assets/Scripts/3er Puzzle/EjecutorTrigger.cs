using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*----------------------------------------------------------------------------------------
 * Se trata de un Script utilizado por los objetos que NO van a interferir
 * en el movimiento de los cubos. Ya que colaboran en otra misión:
 * - Dota al portador de la capacidad para cambiar el color del cubo que colisione 
 * con él (sólo si es de rayo). 'Collider col'
 * - Además le agrega esta capacidad.
 * -------------------------------------------------------------------------------------*/
public class EjecutorTrigger : MonoBehaviour
{
    void OnTriggerEnter (Collider col)
    {

        if (col.tag == "Rayo")
        {
            col.gameObject.GetComponent<VictimaColor>().cambiarColor(); //Cambia el color.
            col.gameObject.GetComponent<EjecutorColor>().habCambio(); //Agrega la capacidad.
            //col.GetComponent<GameObject>().GetComponent<Rigidbody>().isKinematic = true; //Bloquea el movimiento.
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.tag == "Rayo")
        {
            col.gameObject.GetComponent<VictimaColor>().cambiarColor(); //Restaura el color.
            col.gameObject.GetComponent<EjecutorColor>().habCambio(); //Agrega la capacidad.
            //col.GetComponent<GameObject>().GetComponent<Rigidbody>().isKinematic = false; //Desbloquea el movimiento.
        }
    }
}
