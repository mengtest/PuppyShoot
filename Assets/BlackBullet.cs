using UnityEngine;
using System.Collections;

public class BlackBullet : MonoBehaviour 
{

    public float speed = 5.0f;

    public float disappearDis = 30.0f;
    private Vector3 startPosition;
    private Vector3 moveDir;
    private bool bStartMove = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        IsOverDistance();
        StartMoving();
	}

    public void SetMoveDir(float angle,Vector3 startPosition)
    {
        this.transform.position = startPosition;
        this.transform.Rotate(new Vector3(0,0,angle));
        this.startPosition = startPosition;
        bStartMove = true;
    }


    void OnDisable()
    {
        bStartMove = false;
    }


    void StartMoving()
    {
        if(bStartMove)
        {
            this.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
    }

    void IsOverDistance()
    {
        if(!bStartMove)
        {
            return;
        }

        float distance = Vector3.Distance(this.transform.position, startPosition);
        if(distance > disappearDis)
        {
            Destory();
        }
    }

    void Destory()
    {

    }
}
