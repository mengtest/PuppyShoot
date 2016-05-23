using UnityEngine;
using System.Collections;

public class ASEnemyController : MonoBehaviour 
{
    private ASEnemyStatus m_ASEnemyStatus;
    private GameObject player;

    public float startDistanceToShoot = 10.0f;
    //public float endDistanceToShoot = 8.0f;
    //public float distanceFromPlayerAtStart = 9.0f;


    public bool m_bCanShoot = false;


    // Use this for initialization
    void Awake()
    {
        m_ASEnemyStatus = this.GetComponent<ASEnemyStatus>();
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        SetCanShoot(true);
    }

    void OnEnable()
    {
       
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
            if (m_ASEnemyStatus.GetIsAttack())
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
        if (this.GetComponent<ASShotMaker>())
        {
            isFiring = this.GetComponent<ASShotMaker>().GetIsFiring();
            if (!isFiring)
            {
                if (IsInRange(startDistanceToShoot))
                {
                    this.GetComponent<ASShotMaker>().SetIsFiring();
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

}
