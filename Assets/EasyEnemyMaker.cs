using UnityEngine;
using System.Collections;

public class EasyEnemyMaker : MonoBehaviour {


    public float creationInterval = 2.0f;

    private PlayerStatus playerStatus;
    private int enemyCount = 0;
    private int destoryEnemyCount;
    private int maxEnemyCount;
    private bool isMaking = false;

    private Poolable easyEnemyPoolobj;
    

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
            if (!isMaking)
            {
                isMaking = true;
                StartCoroutine(CreateEnemy());
            }
        }
	}

    IEnumerator CreateEnemy()
    {
        enemyCount++;
        yield return new WaitForSeconds(creationInterval);
        easyEnemyPoolobj = ObjectPooler.Dequeue(PoolKeys.EasyEnemys);
        easyEnemyPoolobj.gameObject.SetActive(true);
        isMaking = false;
    }
}
