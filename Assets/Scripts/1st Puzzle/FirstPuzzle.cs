using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPuzzle : MonoBehaviour
{
    //Escena a la que cambia al terminar el puzle
    public string scene;

    // Distancia diagonal entre piezas
    public Vector2 diagonalDistance;
    PieceMovement[] pieces;

    // Use this for initialization

    void Start()
    {
        pieces = GetComponentsInChildren<PieceMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        // Se escoge el vector inputVec del metodo CheckSpot dependiendo de la flecha pulsada
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CheckSpot(new Vector2(-Mathf.Abs(diagonalDistance.x), 0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CheckSpot(new Vector2(Mathf.Abs(diagonalDistance.x), 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckSpot(new Vector2(0, Mathf.Abs(diagonalDistance.y)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CheckSpot(new Vector2(0, -Mathf.Abs(diagonalDistance.y)));
        }
    }

    // Metodo para analizar si es posible el movimiento de pieza
    void CheckSpot(Vector2 inputVec)
    {
        // Se empieza desde 1 porque i=0 es la pieza "Empty" en la jerarquia.
        int i = 1;
        bool found = false;

        while (i < pieces.Length && !found)
        {
            // Si las coordenadas de la pieza "Empty" + el vector input coinciden con la posicion de una de las piezas del array, intercambia la posicion entre ellas 
            if ((Vector2)pieces[0].transform.position + inputVec == (Vector2)pieces[i].transform.position)
            {
                SwapPosition(i, inputVec);
                CheckSolved();
                found = true;
            }

            i++;
        }
    }

    // Metodo para realizar el cambio de posicion entre dos piezas
    void SwapPosition(int i, Vector2 inputVec)
    {
        pieces[i].MovePieceTo(pieces[0].transform.position);
        pieces[0].MovePieceTo((Vector2)pieces[0].transform.position + inputVec);
    }

    // Metodo para revisar si se ha completado
    void CheckSolved()
    {
        int i = 0;

        while (i < pieces.Length && pieces[i].CorrectPosition())
        {
            i++;
        }

        if (i == pieces.Length)
        {
            Debug.Log("Completado!");
            GameManager.instance.SetAbilityTrue("Fireball");
            GameManager.instance.Respawn();
        }
    }
}
