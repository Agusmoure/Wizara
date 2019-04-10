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
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("Click");
            selected = !selected;
            //mesh.material = select;
            if (!selected)
                mesh.material = select;
            else if (selected)
                mesh.material = defau;
        }
	}
}
