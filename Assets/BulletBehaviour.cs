using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    public enum BulletOwner
    {
        PLAYER,
        ENEMY
    }

    public float speed = 50.0f;
    private BulletOwner m_eBulletOwner;
    private bool m_bIsStart = false;
    private Vector3 m_vTargetPosition;
    private Vector3 m_vMoveDir;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Firing();
	}

    public void SetBulletOwner(BulletOwner eOwner)
    {
        m_eBulletOwner = eOwner;
    }

    BulletOwner GetBulletOwner()
    {
        return m_eBulletOwner;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        m_vTargetPosition = targetPosition;
        m_bIsStart = true;
    }

    void Firing()
    {
        if(m_bIsStart)
        {
            m_vMoveDir = CalMoveDir(m_vTargetPosition);
            this.transform.LookAt(m_vTargetPosition);
            GetComponent<Rigidbody2D>().velocity = m_vMoveDir * speed * Time.deltaTime;
        }
    }

    Vector3 CalMoveDir(Vector3 targetPosition)
    {
        return (targetPosition - this.transform.position).normalized;
    }

    public void SetBulletPosition(Vector3 position)
    {
        this.transform.position = position;
    }
}
