using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    [SerializeField]private GameObject followTarget;
    private float x_offset;

    void Awake()
    {
        this.x_offset = this.transform.position.x - followTarget.transform.position.x;
    }

    void LateUpdate()
    {
        this.transform.position=new Vector3(followTarget.transform.position.x+this.x_offset,this.transform.position.y,this.transform.position.z);
    }


}
