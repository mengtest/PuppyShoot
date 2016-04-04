using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {

    public int m_nEnemyHealth = 1;
    public AudioClip m_ExploreSound;
    


    private GameObject player;
    private GameObject m_exploreEffect;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(Tags.Bullet))
        {
            OnDamage(1);
        }
    }

    void OnDamage(int damage)
    {
        m_nEnemyHealth -= damage;
        if(m_nEnemyHealth < 1)
        {
            OnEnemyDie();
        }
    }

    void OnEnemyDie()
    {
        //播放爆炸声音
        AudioSource.PlayClipAtPoint(m_ExploreSound, this.gameObject.transform.position, 1.0f);

        //该敌人对象入池
        ObjectPooler.Enqueue(this.GetComponent<Poolable>());

        //从对象池获取爆炸特效
        m_exploreEffect = ObjectPooler.Dequeue(PoolKeys.ExplosionEffect).gameObject;
        m_exploreEffect.gameObject.transform.position = (this.gameObject.transform.position);
        m_exploreEffect.gameObject.SetActive(true);
        m_exploreEffect.gameObject.GetComponent<ParticleSystem>().Play();
        //延迟特效入池
        m_exploreEffect.gameObject.GetComponentInParent<ExplosionEffect>().DelayEnqueue();
        
    }


}
