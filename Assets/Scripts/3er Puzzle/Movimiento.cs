using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public KeyCode right, left, up, down;
    Rigidbody rb;
    Vector2 mov; 
    BoxCollider bc;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        mov = new Vector2(0, 0);
        bc = gameObject.GetComponent<BoxCollider>();
    }
    
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        
        if (Input.GetKeyUp("right") && x < 1.4)
        {
            convertToTrigger();
            mov = new Vector2((float)0.01, 0);
            resetVelocidad();
            Invoke("convertToTrigger", 2);
        }
        if (Input.GetKeyUp("left") && x > -1.4)
        {
            convertToTrigger();
            mov = new Vector2((float)-0.01, 0);
            resetVelocidad();
            Invoke("convertToTrigger", 2);
        }
        if (Input.GetKeyUp("up") && y < 1.4)
        {
            convertToTrigger();            
            mov = new Vector2(0, (float) 0.01);
            resetVelocidad();
            Invoke("convertToTrigger", 2);
        }
        if (Input.GetKeyUp("down") && y > -1.4)
        {
            convertToTrigger();
            mov = new Vector2(0, (float) -0.01);
            resetVelocidad();
            Invoke("convertToTrigger", 2);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {        
        rb.AddForce(mov, ForceMode.Impulse);
        Debug.Log(rb.velocity);
    }
    
    void convertToTrigger()
    {
        if (bc.isTrigger == true)
            bc.isTrigger = false;
        else if (bc.isTrigger == false)
            bc.isTrigger = true;
    }

    void resetVelocidad()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
