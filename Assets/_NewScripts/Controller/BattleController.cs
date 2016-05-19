using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleController: MonoBehaviour
{
    public static BattleController instance;
    public GameObject loseMask;
    public GameObject winMask;

    private PlayerStatus playerStatus;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerStatus>();
        loseMask.SetActive(false);
        winMask.SetActive(false);
    }


    void Update()
    {
        if(playerStatus.IsGameOver())
        {
            //若玩家被撸死了,留跳出Lose并且5秒后回到游戏主界面,哈哈哈
            loseMask.SetActive(true);
            StartCoroutine(Replay());
        }

        if(playerStatus.IsPlayerSucceed())
        {
            winMask.SetActive(true);
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator Replay()
    {
        yield return new WaitForSeconds(5.0f);
        //对象池物体先隐藏起来
        ObjectPooler.EnqueueAll();
        SceneManager.LoadScene("Opening");
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5.0f);
        ObjectPooler.EnqueueAll();
        SceneManager.LoadScene("Ending");
    }
    

}
