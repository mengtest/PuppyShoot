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

    public void SetMoveDir(Vector3 moveDir,Vector3 startPosition)
    {
        this.moveDir = moveDir;
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
            GetComponent<Rigidbody2D>().velocity = moveDir * speed * Time.deltaTime;
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
