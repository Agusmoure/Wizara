using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMirror : MonoBehaviour
{
    public Material select, defau;
    MeshRenderer mesh;
    bool selected;

    // Use this for initialization
    void Start ()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material = defau;
        selected = false;
    }

    // Rota el espejo seleccionado.
    void Update()
    {
        if (selected)
            RotationInput(Input.GetAxis("Vertical"));
    }

    //Rota el espejo en el Z para controlar el rayo.
    void RotationInput(float axis)
    {
        transform.Rotate(0, 0, axis);
    }

    //Cambia el material a seleccionado o no seleccionado.
    public void changeSelection()
    {
        selected = !selected;
        if (selected)
            mesh.material = select;
        else if (!selected)
            mesh.material = defau;
    }
}
