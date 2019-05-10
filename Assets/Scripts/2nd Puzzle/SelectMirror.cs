using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMirror : MonoBehaviour
{
    const int NUM_MIRRORS = 3;
    int index, aux;
    public GameObject[] go = new GameObject[NUM_MIRRORS];
    public KeyCode keyRight, keyLeft;
	// Use this for initialization
	void Start ()
    {
        index = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        RotationInput(Input.GetAxis("Vertical"));
        if (Input.GetKeyUp(keyRight) && (index + 1) < go.Length) //right
        {
            index++;
            go[index].GetComponent<MovMirror>().changeColor();
        }
        else if (Input.GetKeyUp(keyLeft) && (index - 1) >= 0) //left
        {
            index--;
            go[index].GetComponent<MovMirror>().changeColor();
        }
    }

    void RotationInput(float axis)
    {
        go[index].transform.Rotate(0, 0, axis);
    }
}
