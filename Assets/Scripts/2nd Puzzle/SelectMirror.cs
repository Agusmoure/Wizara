using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMirror : MonoBehaviour
{
    const int NUM_MIRRORS = 3;
    int index;
    public GameObject[] go = new GameObject[NUM_MIRRORS];
    public KeyCode keyRight, keyLeft;
	// Use this for initialization
	void Start ()
    {
        index = 0;
        Invoke("iniciar", 1);
    }

    void iniciar()
    {
        go[index].GetComponent<MovMirror>().changeSelection();
    }
	
	// Cambia el espejo seleccionado.
	void Update ()
    {
        if (Input.GetKeyUp(keyRight) && (index + 1) < go.Length) //Selección right.
        {
            int aux = index;
            index++;
            go[aux].GetComponent<MovMirror>().changeSelection();
            go[index].GetComponent<MovMirror>().changeSelection();
        }
        else if (Input.GetKeyUp(keyLeft) && (index - 1) >= 0) //Selección left.
        {
            int aux = index;
            index--;
            go[aux].GetComponent<MovMirror>().changeSelection();
            go[index].GetComponent<MovMirror>().changeSelection();
        }
    }

    
}
