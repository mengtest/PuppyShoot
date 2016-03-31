using UnityEngine;
using System.Collections;

public class PlayerRocketMaker : MonoBehaviour {

    private bool m_bStartFire = false;

    private GameObject rocket;
    private GameObject enemy;

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
            rocket = ObjectPooler.Dequeue(PoolKeys.Bullets).gameObject;
            BulletBehaviour rocketBehaviour = rocket.GetComponent<BulletBehaviour>();
            rocketBehaviour.SetBulletOwner(BulletBehaviour.BulletOwner.PLAYER);
            rocketBehaviour.SetBulletPosition(this.gameObject.transform.position);
            rocketBehaviour.SetTargetPosition(enemy.transform.position);
        }
    }

    public void SetEnemy(GameObject enemy)
    {
        this.enemy = enemy;
        m_bStartFire = true;
    }
}
