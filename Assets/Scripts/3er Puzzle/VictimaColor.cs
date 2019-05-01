using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimaColor : MonoBehaviour
{
    public GameObject cubo;
    public Material rayo;
    public Material conecta;
    bool change; //Estado actual del cubo.
    Renderer mesh; //Variable privada del objeto.


    void Start ()
    {
        mesh = cubo.GetComponent<Renderer>();
        mesh.material = rayo;
        change = false;
    }

    public bool Conecta() //Devuelve 'true' si conecta.
    {
        return change;
    }
	
    public void cambiarColor() //Efectua el cambio de color correspondiente.
    {
        if (change == false)
            cambiaConecta();
        else
            cambiaRayo();
    }

    void cambiaConecta() //Cambia a mesh Conecta.
    {
        mesh.material = conecta;
        change = true;
    }

    void cambiaRayo() //Cambia a mesh Rayo.
    {
        mesh.material = rayo;
        change = false;
    }
}
