using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControllerNew : MonoBehaviour
{
    private ObjectsPoolMechine objectPoolMechine;


    void Awake()
    {
        objectPoolMechine = this.transform.GetComponentInChildren<ObjectsPoolMechine>();
    }

    void Start()
    {
        if(objectPoolMechine!=null)
            objectPoolMechine.PoolAllObjects();
        else
            Debug.Log("Can't found ObjectPoolMechine");
    }

}
