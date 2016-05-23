using UnityEngine;
using System.Collections;

public class FireStick : MonoBehaviour {

    public float rotateSpeed = 120.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
	}

    void Rotate()
    {
        this.transform.transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));
    }
}
