using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
    public float startDistanceToShoot = 5.0f;
    public float endDistanceToShoot = 8.0f;
    public float shotInterval = 1.5f;

    private BlackBullet[] blackBullets = new BlackBullet[3];
    private float deltAngle = 15.0f;
    private GameObject player;
    private bool bFire = true;
    private bool bIsFiring = false;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(player)
        {
            IsInRange(startDistanceToShoot, endDistanceToShoot);
            FireBullet();
        }
        
	}

    void FireBullet()
    {
        if (bFire)
        {
            if(!bIsFiring)
            {
                StartCoroutine(ShotBlackBullets());
            }
        }
    }

    private void IsInRange(float fromDistance, float toDistance)
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        bFire = (distance >= fromDistance && distance <= toDistance) ? true : false;
    }

    IEnumerator ShotBlackBullets()
    {
        bIsFiring = true;
        for (int i = 0; i < 3; ++i)
        {
            blackBullets[i] = ObjectPooler.Dequeue(PoolKeys.BlackBullet).GetComponent<BlackBullet>();
            float angleZ = (i - 1) * deltAngle;
            blackBullets[i].SetMoveDir(angleZ, this.transform.position);
            blackBullets[i].gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(shotInterval);
        bIsFiring = false;
    }

}
