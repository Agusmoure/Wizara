using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLighting : MonoBehaviour
{
    public GameObject este;
    public bool cosaka;
    public Transform puntako;
    public float speed, timeCd;
    bool cd=false;
    Vector3 initialPosition, target;
    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;
        target = puntako.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (cosaka)
            SoltarRayo();
    }
    void SoltarRayo()
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
            if (target == initialPosition) target = puntako.position;
            else target = initialPosition;
        }


    }
    void InvokeCd()
    {
        cd = false;
    }
}
