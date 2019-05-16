using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLighting : MonoBehaviour
{
    public GameObject este;
    public Transform puntoInicial,puntoFinal;
    public float speed, timeCd;
    bool cd=false,ray=false;
    Vector3 initialPosition, target;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ray) SoltarRayo();
    }
    public void SoltarRayo()
    {
        int layerMask = 1 << 8;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Physics2D.Raycast(transform.position, Vector2.down, 900, layerMask)&&!cd)
        {
            Instantiate(este, transform.position, Quaternion.identity);
            cd = true;
            Invoke("InvokeCd", timeCd);
        }
        if (transform.position == target)
        {
            if (target == initialPosition) target = puntoFinal.position;
            else target = initialPosition;
        }


    }
    void InvokeCd()
    {
        cd = false;
    }
    public void LightingOn()
    {
        initialPosition = puntoInicial.position;
        target = puntoFinal.position;
        while (transform.position != initialPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
        }
        ray = true;
    }
    public void LightingOff()
    {
        ray = false;
    }
}
