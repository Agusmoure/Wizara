using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMirror : MonoBehaviour
{
    int index;
    MovMirror[] mirror;
    public KeyCode keyRight, keyLeft;
	// Use this for initialization
	void Start ()
    {
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
        if (Input.GetKeyUp(keyRight) && (index + 1) < mirror.Length) //Selección right.
        {
            int aux = index;
            index++;
            mirror[aux].changeSelection();
            mirror[index].changeSelection();
        }
        else if (Input.GetKeyUp(keyLeft) && (index - 1) >= 0) //Selección left.
        {
            int aux = index;
            index--;
            mirror[aux].changeSelection();
            mirror[index].changeSelection();
        }
    }

    
}
