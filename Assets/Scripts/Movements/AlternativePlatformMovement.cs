using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativePlatformMovement : MonoBehaviour {

    public GameObject platform;
    public float speedMovement;
    public Transform[] points; // trazado que realizará el movimiento
    Transform currentPoint; // punto actual al que se moverá la plataforma
    public int selector; //variable que indica a qué plataforma se moverá

	// Use this for initialization
	void Start () {
        currentPoint = points[selector];    //se indica el punto de inicio del movimiento
	}
	
	// Update is called once per frame
	void Update () {
        //La plataforma se moverá hasta el siguiente punto del trazado que corresponda
        platform.transform.position = Vector2.MoveTowards(platform.transform.position, currentPoint.transform.position, speedMovement * Time.deltaTime);  

        //comprueba si la plataforma ha llegado al punto
        if (platform.transform.position == currentPoint.transform.position)
        {
            //si es así, cambia al siguiente punto (la fórmula hace que si llega al máximo de puntos vuelve al primero)
            selector = (selector + 1) % points.Length;
            currentPoint = points[selector];

        }

	}
}
