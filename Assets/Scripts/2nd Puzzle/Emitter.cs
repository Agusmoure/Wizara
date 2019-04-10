using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public int reflections = 5;
    public Material End;
    LineRenderer lineRen;
    bool stop;
    
    //the number of points at the line renderer
    void Start()
    {
        //get the attached LineRenderer component  
        lineRen = GetComponent<LineRenderer>();
        lineRen.SetPosition(0, transform.position);
    }
    void Update()
    {
        stop = false;
        ray = new Ray(transform.position, Vector3.right);
        //start with just the origin
        lineRen.positionCount = 1;
        //Bucle para reflexion de "reflections" veces.
        for (int i = 0; i < reflections && !stop; i++)
        {
            Debug.Log("ray.direction" + ray.direction);
            Debug.Log("ray.origin" + ray.origin);
            //Se detecta si hay colision en el raycast con "hit".
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 10))
            {
                //we hit, update line renderer
                lineRen.positionCount++;
                lineRen.SetPosition(lineRen.positionCount - 1, hit.point);
                //Se crea el vector de reflexion.
                Vector3 relfection = Vector3.Reflect(ray.direction, hit.normal);
                //Se crea el rayo que se refleja.
                ray = new Ray(hit.point, relfection);
                //Si no tocamos un objeto con tag "Mirror", detenemos bucle.
                if (hit.collider.tag != "Mirror")
                    stop = true;
                //Comprueba si ha finalizado.
                if (hit.transform.tag == "EndMirror")
                {
                    MeshRenderer mesh = hit.transform.GetComponent<MeshRenderer>();
                    mesh.material = End;
                }
            }
            else
            {
                // We didn't hit anything, draw line to end of ramainingLength
                lineRen.positionCount++;
                lineRen.SetPosition(lineRen.positionCount - 1, ray.origin + ray.direction * 20);
                stop = true;
            }
        }
    }
}
