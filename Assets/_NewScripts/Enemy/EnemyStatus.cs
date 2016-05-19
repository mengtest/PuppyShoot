using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {


    public float breakingDistance = 40.0f;
    public int m_nEnemyHealth = 1;
    public AudioClip m_ExploreSound;
    public int m_EnemyScore = 100;


    public GameObject player;
    private GameObject m_exploreEffect;

    private enum State
    {
        INITIALIZE,
        ATTACK,
        TAKEDAMAGE,
        DESTORY,
        DISAPPEAR
    }

    private State enemyState = State.INITIALIZE;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        enemyState = State.ATTACK;
	}


    void OnEnable()//从对象池取出SetActive(true)调用
    {

    }

    void OnDisable()
    {
        //入池后怪物状态还原避免影响下次Dequeue后状态异常
        enemyState = State.ATTACK;
        m_nEnemyHealth = 1;
    }
   
	
    void Update()
    {
        IsOverTheDistance();
        OnDamage();
        DestoryEnemy();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(Tags.Bullet))
        {
            if(other.gameObject.GetComponent<BulletBehaviour>().GetBulletOwner()== BulletBehaviour.BulletOwner.PLAYER)
            {
                enemyState = State.TAKEDAMAGE;
            }
        }
    }

    void OnDamage()
    {
        if (enemyState == State.TAKEDAMAGE)
        {
            enemyState = State.ATTACK;
            m_nEnemyHealth--;
            if (m_nEnemyHealth < 1)
            {
                enemyState = State.DESTORY;
            }
        }
    }

    void DestoryEnemy()
    {
        if(enemyState ==  State.DESTORY)
        {
            //播放爆炸声音
            AudioSource.PlayClipAtPoint(m_ExploreSound, this.gameObject.transform.position, 1.0f);


            //从对象池获取爆炸特效
            m_exploreEffect = ObjectPooler.Dequeue(PoolKeys.ExplosionEffect).gameObject;
            m_exploreEffect.gameObject.transform.position = (this.gameObject.transform.position);
            m_exploreEffect.gameObject.SetActive(true);
            m_exploreEffect.gameObject.GetComponent<ParticleSystem>().Play();
            //延迟特效入池
            m_exploreEffect.gameObject.GetComponentInParent<ExplosionEffect>().DelayEnqueue();

            //增加得分(给UI)
            this.PostNotification(Notifications.ADD_SCORE, new InfoEventArgs<int>(m_EnemyScore));

            //该敌人对象入池
            ObjectPooler.Enqueue(this.GetComponent<Poolable>());
            //Destroy(this.gameObject);

        }

        if(State.DISAPPEAR == enemyState)
        {
            //该敌人对象入池
            ObjectPooler.Enqueue(this.GetComponent<Poolable>());
            //Destroy(this.gameObject);
        }
        
    }


    private void IsOverTheDistance()
    {
        if(!player)
        {
            return;
        }

        if(enemyState == State.ATTACK)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if(distance > breakingDistance)
            {
                enemyState = State.DISAPPEAR;
            }
        }
    }

    public bool GetIsAttack()
    {
        return State.ATTACK == enemyState;
    }

   
}
