using UnityEngine;
using System.Collections;

public class PlayerRocketMaker : MonoBehaviour {

    private bool m_bStartFire = false;

    private GameObject enemy;
    private Poolable bulletPoolableObj;

    public enum RocketType
    {
        NORMAL,
        MISSILE
    }

    public RocketType playerRocketType = RocketType.NORMAL;
	// Use this for initialization
	void Start () 
    {
              
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        ShootRocket();
	}

    private void ShootRocket()
    {
        if (m_bStartFire)
        {
            m_bStartFire = false;
            switch (playerRocketType)
            {
                case RocketType.NORMAL:
                    FireNormalRocket();
                    break;
                case RocketType.MISSILE:
                    FireMissile();
                    break;
                default:
                    break;
            }
        }
    }

   

    private void FireNormalRocket()
    {
        bulletPoolableObj = ObjectPooler.Dequeue(PoolKeys.Bullets);
        BulletBehaviour rocketBehaviour = bulletPoolableObj.GetComponent<BulletBehaviour>();
        //rocketBehaviour.SetBulletOwner(BulletBehaviour.BulletOwner.PLAYER);
        rocketBehaviour.tag = Tags.Bullet;
        rocketBehaviour.SetBulletPosition(this.gameObject.transform.position);
        rocketBehaviour.gameObject.SetActive(true);
        rocketBehaviour.SetTargetPosition(enemy.transform.position);
    }

    private void FireMissile()
    {
        bulletPoolableObj = ObjectPooler.Dequeue(PoolKeys.Missile);
        MissileBehaviour missileBehaviour = bulletPoolableObj.GetComponent<MissileBehaviour>();
        //rocketBehaviour.SetBulletOwner(BulletBehaviour.BulletOwner.PLAYER);
        missileBehaviour.tag = Tags.Bullet;
        missileBehaviour.SetBulletPosition(this.gameObject.transform.position);
        missileBehaviour.gameObject.SetActive(true);
        missileBehaviour.SetTarget(enemy);
    }

    public void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
        m_bStartFire = true;
    }

    public void SetPlayerRocketType(RocketType rocketType)
    {
        this.playerRocketType = rocketType;
    }
}
