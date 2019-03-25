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
            selector++; //si es así, se selecciona el siguiente

            //comprueba si ha llegado al último punto
            if (selector == points.Length) selector = 0; //si es así, vuelve al primero

            currentPoint = points[selector]; //se indica el siguiente punto al que se deberá mover la plataforma
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
