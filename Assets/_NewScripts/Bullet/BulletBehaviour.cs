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

    public BulletOwner GetBulletOwner()
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
            m_vMoveDir = CalcMoveDir(m_vTargetPosition);
            Debug.Log(m_vMoveDir);
            this.transform.LookAt(m_vTargetPosition);//这个函数有毒,每次LookAt后下次的velocity就会改变方向,我总算是知道了
            GetComponent<Rigidbody2D>().velocity = m_vMoveDir * speed * Time.deltaTime;
        }
    }

    Vector3 CalcMoveDir(Vector3 targetPosition)
    {
        return (targetPosition - this.transform.position).normalized;
    }

    public void SetBulletPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(Tags.Enemy) && !this.CompareTag(Tags.EnemyBullet))
        {
            OnDestroyBullet();
        }

        if (other.gameObject.CompareTag(Tags.Player) && !this.CompareTag(Tags.Bullet))
        {
            OnDestroyBullet();
        }

    }

    void OnDestroyBullet()
    {
        //入池
        ObjectPooler.Enqueue(this.GetComponent<Poolable>());
    }

}
