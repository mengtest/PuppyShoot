using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    public int playerHealth = 5;
    
    private enum State
    {
        INITIALIZE,
        NOWPLAYING,
        PLAYERGETHURT,
        STARTDESTRUCTION,
        WAITING,
        RESTART
    }

    private State programState = State.INITIALIZE;
    //private int waitTimeAfterExplosion = 2;
    private bool m_bIsGameOver = false;

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
            if(other.CompareTag(Tags.EnemyBullet))
            {
                programState = State.PLAYERGETHURT;
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
        //this.playerHealth--;
        if(playerHealth >= 0)
        {
            //this.PostNotification(Notifications.LOSE_HEALTH);
        }
    }

    private bool IsPlayerDie()
    {
        return playerHealth < 1;
    }

    private void OnPlayerDie()
    {
        this.gameObject.SetActive(false);
        m_bIsGameOver = true;
    }

    public int GetPlayerHealth()
    {
        return this.playerHealth;
    }
}
