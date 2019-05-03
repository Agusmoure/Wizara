using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderManager : MonoBehaviour {
    public GameObject girl, boy;
    public string scene;
    // Use this for initialization
    void Start () {
        if (GameManager.instance.GetGender())
        {
            Destroy(boy);
            girl.SetActive(true);

        }
        else {
            Destroy(girl);
            boy.SetActive(true);
        }
    }

    public void Boy()
    {
        GameManager.instance.AreYouAGirl(false);
        GameManager.instance.ChangeScene(scene);
    }
    public void Girl()
    {
        GameManager.instance.AreYouAGirl(true);
        GameManager.instance.ChangeScene(scene);

    }
}
