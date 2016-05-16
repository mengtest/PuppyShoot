//using UnityEngine;
//using System.Collections;

//public class MissileBehaviour : MonoBehaviour
//{
//    public float speed = 2.0f;
//    public float m_DisappearDistance = 20.0f;
//    public float m_turnRate = 30.0f;
//    public float turnRateAcceleration = 18.0f;
//    private bool m_bIsStart = false;
//    private Vector3 m_vTargetPosition;
//    private Vector3 m_vMoveDir;

//    private GameObject m_goTarget;

//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        OnOverDistance();
//        Firing();
//    }


//    public void SetTargetPosition(Vector3 targetPosition)
//    {
//        m_vTargetPosition = targetPosition;
//        m_vMoveDir = CalcMoveDir(m_vTargetPosition);
//        this.transform.LookAt(m_vTargetPosition);//放到这里就好了
//        m_bIsStart = true;
//    }

//    public void SetTarget(GameObject target)
//    {
//        m_goTarget = target;
//        m_bIsStart = true;
//    }

//    void Firing()
//    {
//        if (m_bIsStart)
//        {
//            Vector3 enemyPosition = m_goTarget.gameObject.transform.position;
//            Vector3 relativePosition = enemyPosition - transform.position;
//            Quaternion targetRotation = Quaternion.LookRotation(relativePosition,Vector3.forward);

//            float targetRotationAngle = -targetRotation.eulerAngles.x;
//            float currentRotationAngle = -transform.eulerAngles.x;

//            currentRotationAngle = Mathf.Lerp(currentRotationAngle, targetRotationAngle, m_turnRate*Time.deltaTime);
//            Quaternion tiltedRotation = Quaternion.Euler(0, 0, currentRotationAngle);

//            m_turnRate += turnRateAcceleration * Time.deltaTime;
//            transform.rotation = tiltedRotation;
//            transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f));
//        }
//    }

//    Vector3 CalcMoveDir(Vector3 targetPosition)
//    {
//        return (targetPosition - this.transform.position).normalized;
//    }

//    public void SetBulletPosition(Vector3 position)
//    {
//        this.transform.position = position;
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag(Tags.Enemy) && !this.CompareTag(Tags.EnemyBullet))
//        {
//            OnDestroyBullet();
//        }

//        if (other.gameObject.CompareTag(Tags.Player) && !this.CompareTag(Tags.Bullet))
//        {
//            OnDestroyBullet();
//        }

//    }

//    void OnDestroyBullet()
//    {
//        //入池
//        ObjectPooler.Enqueue(this.GetComponent<Poolable>());
//    }

//    void OnOverDistance()
//    {
//        float distance = Vector3.Distance(this.transform.position, m_vTargetPosition);
//        if (distance > m_DisappearDistance)
//        {
//            OnDestroyBullet();
//        }
//    }
//}
