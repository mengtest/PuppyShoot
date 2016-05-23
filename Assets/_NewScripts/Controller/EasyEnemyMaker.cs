using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasyEnemyMaker : MonoBehaviour {


    public float creationInterval;
    public float rangeDistance = 18.0f;
    public enum SpawnType
    {
        ONCE,
        ONEBYONE,
    };
    public SpawnType spawnType;
    private PlayerStatus playerStatus;
    private int enemyCount = 0;
    private int maxEnemyCount = 10;
    private bool isMaking = false;

    private Poolable easyEnemyPoolobj;

    public List<Vector3> spawnPositions;

	// Use this for initialization
	void Start () 
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerStatus.GetIsNOWPLAYING() && IsPlayerInRange())
        {
            if(enemyCount < maxEnemyCount)
            {
                if (!isMaking)
                {
                    isMaking = true;
                    if(spawnType == SpawnType.ONEBYONE)
                    {
                        StartCoroutine(CreateEnemyIEnum());
                    }
                    else if(spawnType == SpawnType.ONCE)
                    {
                        CreateEnemy();
                    }
                }
            }
        }
	}

    IEnumerator CreateEnemyIEnum()
    {
        yield return new WaitForSeconds(creationInterval);     
   
        if(playerStatus.GetIsNOWPLAYING())
        {
            easyEnemyPoolobj = ObjectPooler.Dequeue(PoolKeys.EasyEnemys);
            //easyEnemyPoolobj.GetComponent<EasyEnemyController>().SetEnemyMaker(this);
            Vector3 spawnPos = GetSpawnPosition();
            if (spawnPos != Vector3.zero)
            {
                easyEnemyPoolobj.gameObject.transform.position = spawnPos;
            }
            else
            {
                easyEnemyPoolobj.gameObject.SetActive(false);
            }
            easyEnemyPoolobj.gameObject.GetComponent<EasyEnemyController>().player = playerStatus.gameObject;
            easyEnemyPoolobj.gameObject.GetComponent<EnemyStatus>().player = playerStatus.gameObject;
            easyEnemyPoolobj.gameObject.GetComponent<ShotMaker>().player = playerStatus.gameObject;
            easyEnemyPoolobj.gameObject.GetComponent<ShotMaker>().playerStatus = playerStatus;
            easyEnemyPoolobj.gameObject.GetComponent<ShotMaker>().isFiring = false;
            easyEnemyPoolobj.gameObject.GetComponent<ShotMaker>().isMakingBullet = false;
            easyEnemyPoolobj.gameObject.SetActive(true);
            enemyCount++;
        }
        isMaking = false;
    }

    void CreateEnemy()
    {
        if(playerStatus.GetIsNOWPLAYING())
        {
            for(int i = 0 ;i <spawnPositions.Count;++i)
            {
                easyEnemyPoolobj = ObjectPooler.Dequeue(PoolKeys.EasyEnemys);
                //easyEnemyPoolobj.GetComponent<EasyEnemyController>().SetEnemyMaker(this);
                Vector3 spawnPos = GetSpawnPosition();
                if (spawnPos != Vector3.zero)
                {
                    easyEnemyPoolobj.gameObject.transform.position = spawnPos;
                }
                else
                {
                    easyEnemyPoolobj.gameObject.SetActive(false);
                }
                easyEnemyPoolobj.gameObject.GetComponent<EasyEnemyController>().player = playerStatus.gameObject;
                easyEnemyPoolobj.gameObject.GetComponent<EnemyStatus>().player = playerStatus.gameObject;
                easyEnemyPoolobj.gameObject.GetComponent<ShotMaker>().player = playerStatus.gameObject;
                easyEnemyPoolobj.gameObject.GetComponent<ShotMaker>().playerStatus = playerStatus;
                easyEnemyPoolobj.gameObject.GetComponent<ShotMaker>().isFiring = false;
                easyEnemyPoolobj.gameObject.GetComponent<ShotMaker>().isMakingBullet = false;
                easyEnemyPoolobj.gameObject.SetActive(true);
                enemyCount++;
            }
        }
        isMaking = false;
    }

    public Vector3 GetSpawnPosition()
    {
        if (spawnPositions.Count > 0)
        {
            Vector3 retVec = spawnPositions[spawnPositions.Count - 1];
            spawnPositions.Remove(spawnPositions[spawnPositions.Count - 1]);
            retVec.x += this.transform.position.x;
            return retVec;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private bool IsPlayerInRange()
    {
        float distanceX = Mathf.Abs(this.transform.position.x - playerStatus.gameObject.transform.position.x);
        return distanceX < rangeDistance;
    }
}
