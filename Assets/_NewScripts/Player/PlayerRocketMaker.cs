using UnityEngine;
using System.Collections;

public class PlayerRocketMaker : MonoBehaviour {

    private bool m_bStartFire = false;

    private GameObject enemy;
    private Poolable bulletPoolableObj;

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
            bulletPoolableObj = ObjectPooler.Dequeue(PoolKeys.Bullets);
            BulletBehaviour rocketBehaviour = bulletPoolableObj.GetComponent<BulletBehaviour>();
            //rocketBehaviour.SetBulletOwner(BulletBehaviour.BulletOwner.PLAYER);
            rocketBehaviour.tag = Tags.Bullet;
            rocketBehaviour.SetBulletPosition(this.gameObject.transform.position);
            rocketBehaviour.gameObject.SetActive(true);
            rocketBehaviour.SetTargetPosition(enemy.transform.position);
        }
    }

    public void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
        m_bStartFire = true;
    }
}
