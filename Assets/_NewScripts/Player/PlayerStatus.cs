using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    public int playerHealth = 5;
    public AudioClip m_ExploreSound;

    private enum State
    {
        INITIALIZE,
        NOWPLAYING,
        PLAYERGETHURT,
        STARTDESTRUCTION,
        WAITING,
        RESTART,
        SUCCEED
    }

    private State programState = State.INITIALIZE;
    //private int waitTimeAfterExplosion = 2;
    private bool m_bIsGameOver = false;

    private GameObject m_exploreEffect;


	// Use this for initialization
	void Start () 
    {
        programState = State.NOWPLAYING;
	}
	
	// Update is called once per frame
	void Update () {
	    if(!m_bIsGameOver)
        {
            if(State.PLAYERGETHURT == programState)
            {
                programState = State.NOWPLAYING;
                PlayerGetHurt();
                if(IsPlayerDie())
                {
                    programState = State.STARTDESTRUCTION;
                    OnPlayerDie();
                }                
            }
        }
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if(State.NOWPLAYING == programState)
        {
            string otherTag = other.tag;

            if(Tags.EnemyBullet == otherTag || Tags.FireStick == otherTag || Tags.Bomb == otherTag ||
                Tags.CloudLightening == otherTag || Tags.Stone == otherTag)
            {
                programState = State.PLAYERGETHURT;
            }

            if(Tags.Exit == otherTag)
            {
                programState = State.SUCCEED;
            }
        }
    }


    public bool GetIsNOWPLAYING()
    {
        return State.NOWPLAYING == programState;
    }

    public bool GetCanPlay()
    {
        return State.NOWPLAYING == programState;
    }

    private void PlayerGetHurt()
    {
        this.playerHealth--;
        if(playerHealth >= 0)
        {
            this.PostNotification(Notifications.LOSE_HEALTH);
        }
    }

    private bool IsPlayerDie()
    {
        return playerHealth < 1;
    }

    private void OnPlayerDie()
    {

        //播放爆炸声音
        AudioSource.PlayClipAtPoint(m_ExploreSound, this.gameObject.transform.position, 1.0f);
        //需要爆炸特效
        //从对象池获取爆炸特效
        m_exploreEffect = ObjectPooler.Dequeue(PoolKeys.ExplosionEffect).gameObject;
        m_exploreEffect.gameObject.transform.position = (this.gameObject.transform.position);
        m_exploreEffect.gameObject.SetActive(true);
        m_exploreEffect.gameObject.GetComponent<ParticleSystem>().Play();
        //延迟特效入池
        m_exploreEffect.gameObject.GetComponentInParent<ExplosionEffect>().DelayEnqueue();
        
        //销毁对象
        this.gameObject.SetActive(false);

        m_bIsGameOver = true;
    }

    public int GetPlayerHealth()
    {
        return this.playerHealth;
    }

    public bool IsGameOver()
    {
        return m_bIsGameOver;
    }
}
