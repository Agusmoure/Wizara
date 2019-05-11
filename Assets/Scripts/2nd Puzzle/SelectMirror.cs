using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMirror : MonoBehaviour
{
    int index;
    MovMirror[] mirror;
	// Use this for initialization
	void Start ()
    {
        // Se obtiene el componente MovMirror de los hijos en un array.
        mirror = GetComponentsInChildren<MovMirror>();
        index = 0;
        Invoke("iniciar", 1);
    }

    void iniciar()
    {
        mirror[index].changeSelection();
    }
	
	// Cambia el espejo seleccionado.
	void Update ()
    {
        if (Input.GetKeyDown("down")) //Selección right.
        {
            int aux = index;
            index++;
            ChangeSelect(aux,ref index);
        }
        else if (Input.GetKeyDown("up")) //Selección left.
        {
            int aux = index;
            index--;
            ChangeSelect(aux,ref index);
        }
    }

    private void ChangeSelect(int aux,ref int index)
    {

        mirror[aux].changeSelection();
        if (index < 0) index = (index % mirror.Length) + mirror.Length;
        else index = index % mirror.Length;
        mirror[index].changeSelection();
    }

}
