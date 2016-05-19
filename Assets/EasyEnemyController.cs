using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasyEnemyController : MonoBehaviour 
{
    private EnemyStatus m_enemyStatus;
    public GameObject player;
    //private EasyEnemyMaker easyEnemyMaker;

    public float startDistanceToShoot = 15.0f;
    //public float endDistanceToShoot = 8.0f;
    //public float distanceFromPlayerAtStart = 9.0f;


    public bool m_bCanShoot = false;

        
	// Use this for initialization
	void Awake () 
    {
        m_enemyStatus = this.GetComponent<EnemyStatus>();
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        SetCanShoot(true);
	}

    void OnEnable()
    {
        ////处理出生位置相关点
        //if(!player||!easyEnemyMaker)
        //{
        //    return;
        //}
        //// 初始化位置从Maker里面拿.
        //Vector3 spawnPos = easyEnemyMaker.GetSpawnPosition();
        //if (spawnPos != Vector3.zero)
        //{
        //    transform.position = spawnPos;
        //}
        //else
        //{
        //    this.gameObject.SetActive(false);
        //}
    }

    void OnDisable()
    {
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
	
	// Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (m_enemyStatus.GetIsAttack())
            {
                //移动相关逻辑ToDo

                //射击相关逻辑
                if (m_bCanShoot)
                {
                    IsFireDistace();
                }
            }
        }
        else
        {
            m_bCanShoot = false;

        }
    }

    void IsFireDistace()
    {
        bool isFiring = false;
        if (this.GetComponent<ShotMaker>())
        {
            isFiring = this.GetComponent<ShotMaker>().GetIsFiring();
            if (!isFiring)
            {
                if (IsInRange(startDistanceToShoot))
                {
                    this.GetComponent<ShotMaker>().SetIsFiring();
                }
            }
        }
    }

    private bool IsInRange(float startDistance)
    {
        float distance = Mathf.Abs(this.transform.position.x - player.transform.position.x);
        
        return (distance <= startDistance);
    }

    public void SetCanShoot(bool canShoot)
    {
        this.m_bCanShoot = canShoot;
    }

    //public void SetEnemyMaker(EasyEnemyMaker maker)
    //{
    //    this.easyEnemyMaker = maker;
    //}
}
