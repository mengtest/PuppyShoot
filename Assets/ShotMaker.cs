using UnityEngine;
using System;
using System.Collections;

public class ShotMaker : MonoBehaviour 
{
    public float fireInterval = 3.0f;
    public int numberOfBullets = 10;
    
    private GameObject player;
    private Poolable shotPoolableObj;

    private int fireCount;
    private bool isFiring = false;
    private bool isMakingBullet = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);

    }

    void Update()
    {
        if(isFiring)
        {
           if(!isMakingBullet)
           {
               isMakingBullet = true;
               MakeBullet();
           }
        }
    }

    private void MakeBullet()
    {
        shotPoolableObj = ObjectPooler.Dequeue(PoolKeys.Bullets);
        BulletBehaviour rocketBehaviour = shotPoolableObj.GetComponent<BulletBehaviour>();
        rocketBehaviour.tag = Tags.EnemyBullet;
        rocketBehaviour.SetBulletPosition(this.gameObject.transform.position);
        rocketBehaviour.gameObject.SetActive(true);
        rocketBehaviour.SetTargetPosition(player.transform.position);

        fireCount++;
        if(fireCount >= numberOfBullets)
        {
            isFiring = false;
        }

        StartCoroutine(WaitAndUpdateFlag(fireInterval));
    }

    IEnumerator WaitAndUpdateFlag(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
        isMakingBullet = false;
    }

    public void SetIsFiring()
    {
        fireCount = 0;
        this.isFiring = true;
    }

    public bool GetIsFiring()
    {
        return isFiring;
    }

}
