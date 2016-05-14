using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasyEnemyMaker : MonoBehaviour {


    public float creationInterval = 2.0f;
    private PlayerStatus playerStatus;
    private int enemyCount = 0;
    private int destoryEnemyCount;
    private int maxEnemyCount = 10;
    private bool isMaking = false;

    private Poolable easyEnemyPoolobj;

    private List<Vector3> spawnPositions = new List<Vector3>()
    {
        new Vector3(12, -4, 0),
        new Vector3(12, 3, 0),
        new Vector3(9, -4, 0),
        new Vector3(9, 3, 0),
        new Vector3(6, -4, 0),
        new Vector3(6, 3, 0),
        new Vector3(3, -4, 0),
        new Vector3(3, 3, 0),
        new Vector3(0, -4, 0),
        new Vector3(0, 3, 0),
    };

	// Use this for initialization
	void Start () 
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerStatus.GetIsNOWPLAYING())
        {
            if(enemyCount < maxEnemyCount)
            {
                if (!isMaking)
                {
                    isMaking = true;
                    StartCoroutine(CreateEnemy());
                }
            }
        }
	}

    IEnumerator CreateEnemy()
    {
        yield return new WaitForSeconds(creationInterval);
        if(playerStatus.GetIsNOWPLAYING())
        {
            easyEnemyPoolobj = ObjectPooler.Dequeue(PoolKeys.EasyEnemys);
            easyEnemyPoolobj.GetComponent<EasyEnemyController>().SetEnemyMaker(this);
            easyEnemyPoolobj.gameObject.SetActive(true);
            enemyCount++;
        }
        isMaking = false;
    }

    public Vector3 GetSpawnPosition()
    {
        if (spawnPositions.Count > 0)
        {
            Vector3 retVec = spawnPositions[spawnPositions.Count - 1];
            spawnPositions.Remove(spawnPositions[spawnPositions.Count - 1]);
            return retVec;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
