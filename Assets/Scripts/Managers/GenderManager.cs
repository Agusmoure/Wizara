using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderManager : MonoBehaviour {
    public GameObject girl, boy;

    // Use this for initialization
    void Start () {
        if (GameManager.instance.GetGender())
        {
            Destroy(boy);
        }
        else
            Destroy(girl);
    }

    public void Boy()
    {
        GameManager.instance.AreYouAGirl(false);
    }
    public void Girl()
    {
        GameManager.instance.AreYouAGirl(true);
    }
}
