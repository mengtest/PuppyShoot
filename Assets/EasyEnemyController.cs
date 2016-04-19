using UnityEngine;
using System.Collections;

public class EasyEnemyController : MonoBehaviour 
{
    private EnemyStatus m_enemyStatus;
    private GameObject player;

    public float startDistanceToShoot = 5.0f;
    public float endDistanceToShoot = 8.0f;
    public float distanceFromPlayerAtStart = 9.0f;


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
        //处理出生位置相关点
        float playerAngleY = player.transform.rotation.eulerAngles.z;
        float additionalAngle = (float)Random.Range(-45, 45);

        // 方向を設定.
        transform.rotation = Quaternion.Euler(0f, 0f, playerAngleY + additionalAngle);

        // 位置を設定.
        transform.position = new Vector3(0, 0, 0);
        transform.position = player.transform.position + transform.right * distanceFromPlayerAtStart;
    }

    void OnDisable()
    {
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
	
	// Update is called once per frame
	void Update () 
    {
	    if(m_enemyStatus.GetIsAttack())
        {
            //移动相关逻辑ToDo

            //射击相关逻辑
            if(m_bCanShoot)
            {
                IsFireDistace();
            }
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
                if (IsInRange(startDistanceToShoot, endDistanceToShoot))
                {
                    this.GetComponent<ShotMaker>().SetIsFiring();
                }
            }
        }
    }

    private bool IsInRange(float fromDistance, float toDisRance)
    {
        float distance = Vector3.Distance(
            player.transform.position,
            transform.position);

        return (distance >= fromDistance && distance <= toDisRance);
    }

    public void SetCanShoot(bool canShoot)
    {
        this.m_bCanShoot = canShoot;
    }
}
