using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutipleLightings : MonoBehaviour {
    public GameObject lighting, pointA, pointB;
    public int numberOfLightings;
	// Use this for initialization
	void Start () {
        if (pointA.transform.position.x > pointB.transform.position.x)
        {
            float aux = pointA.transform.position.x;
            pointA.transform.position = new Vector2(pointB.transform.position.x, pointA.transform.position.y);
            pointB.transform.position= new Vector2(aux, pointA.transform.position.y);
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Storm()
    {
        float[] positionsAlreadyTaken = new float[numberOfLightings];
        for(int k = 0; k < numberOfLightings; k++)
        {
            positionsAlreadyTaken[k] = -9000;
        }
        for (int i = 0; i < numberOfLightings; i++)
        {
            Vector2 position;
            float positionX = Random.Range(pointA.transform.position.x, pointB.transform.position.x);
            int j = 0;
            while(j<numberOfLightings&&positionsAlreadyTaken[j]!=-9000)
            {
                if (positionX == positionsAlreadyTaken[j])
                {
                    positionX = -9000;
                }
                j++;
            }
            if (positionX != -9000)
            {
                position = new Vector2(positionX, pointA.transform.position.y);
                GameObject newLighting = Instantiate(lighting, position, Quaternion.identity);
            }
            else j--;
        }
    }
    public void StartStorm()
    {
        InvokeRepeating("Storm", 1, 5);
    }
    public void StopStorm()
    {
        CancelInvoke("Storm");
    }

}
