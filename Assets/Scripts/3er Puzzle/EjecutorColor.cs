using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*----------------------------------------------------------------------------------------
 * Se trata de un Script utilizado por los objetos que van a interferir
 * en el movimiento de los cubos. Colabora en la misma misión que EjecutorTrigger:
 * - Dota al portador de la capacidad para cambiar el color del cubo que colisione 
 * con él (sólo si es de rayo y no está conectado). 'Collider col'
 * - Además le agrega esta capacidad.
 * -------------------------------------------------------------------------------------*/
public class EjecutorColor : MonoBehaviour
{ 
    public bool cambioColor;

    // Use this for initialization
    void Start ()
    {
        cambioColor = false;
    }

    public void habCambio() 
    {
        if (cambioColor == false)
            cambioColor = true; //Habilita.
        else
            cambioColor = false; //Deshabilita.
    }

    void OnCollisionEnter(Collision col)
    {
        if (cambioColor && !col.gameObject.GetComponent<VictimaColor>().Conecta())
        {
            col.gameObject.GetComponent<VictimaColor>().cambiarColor(); //Cambia el color.
            col.gameObject.GetComponent<Rigidbody>().isKinematic = true; //Bloquea el movimiento.
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (cambioColor && col.gameObject.GetComponent<VictimaColor>().Conecta()) //
        {
            col.gameObject.GetComponent<VictimaColor>().cambiarColor(); //Restaura el color.
            col.gameObject.GetComponent<Rigidbody>().isKinematic = false; //Desbloquea el movimiento.
        }
    }
}
