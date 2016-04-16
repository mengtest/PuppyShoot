using UnityEngine;
using System.Collections;

public class EasyEnemyController : MonoBehaviour 
{
    private EnemyStatus m_enemyStatus;
    private GameObject player;

    public float startDistanceToShoot = 5.0f;
    public float endDistanceToShoot = 8.0f;


    public bool m_bCanShoot = false;
    
	// Use this for initialization
	void Start () 
    {
        m_enemyStatus = this.GetComponent<EnemyStatus>();
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        SetCanShoot(true);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(m_enemyStatus.GetIsAttack())
        {
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

        if (distance >= fromDistance && distance <= toDisRance)
        {
            return true;
        }
        return false;
    }

    public void SetCanShoot(bool canShoot)
    {
        this.m_bCanShoot = canShoot;
    }
}
