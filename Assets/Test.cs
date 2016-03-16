using System;
using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    void Awake()
    {
        this.AddObserver("BeginMethod",OnBegin);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Press");
            this.PostNotification("BeginMethod");
            
        }
    }
    

    void OnDestroy()
    {
        this.RemoveObserver("BeginMethod");
    }

    void OnBegin(object sender, EventArgs e)
    {
        Debug.Log("I am ABC!");
    }

}
