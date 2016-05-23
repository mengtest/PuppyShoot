using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
    public Vector3 startPosition;
    public Vector3 shootPosition1;
    public Vector3 shootPosition2;
    public Vector3 endPosition;
    
    public float triggerDistance;
    
    public float shotInterval = 1.5f;
    public float floatingInterval = 1.0f;
    public float m_moveSpeed = 0.1f;

    private enum BirdState
    {
        START_TO_POS1,
        POS1_TO_POS2,
        POS2_TO_END,
        SHOOTTING,
        DESTORY
    }

    private BirdState currentState = BirdState.START_TO_POS1;


    private BlackBullet[] blackBullets = new BlackBullet[3];
    private float deltAngle = 15.0f;
    private GameObject player;
    private bool bIsFiring = false;
    private bool bStart = false;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    void Start () 
    {
        this.transform.position = startPosition;
        currentState = BirdState.START_TO_POS1;
        bIsFiring = false;
	}
	
	void Update () 
    {
        if(player)
        {
            IsInRange();
            StartAction();
        }
	}

    private void IsInRange()
    {
        float distance = this.transform.position.x - player.transform.position.x;
        if(distance < triggerDistance)
        {
            bStart = true;
        }
    }

    private void StartAction()
    {
        if(bStart)
        {
            switch(currentState)
            {
                case BirdState.START_TO_POS1:
                    StartCoroutine(MoveToAndShoot(shootPosition1,BirdState.POS1_TO_POS2));
                    break;
                case BirdState.POS1_TO_POS2:
                    StartCoroutine(MoveToAndShoot(shootPosition2, BirdState.POS2_TO_END));
                    break;
                case BirdState.POS2_TO_END:
                    StartCoroutine(MoveToAndShoot(endPosition,BirdState.DESTORY));
                    break;
                case BirdState.DESTORY:
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    

    IEnumerator MoveToAndShoot(Vector3 moveToPosition, BirdState nextState)
    {
        while (Vector3.Distance(this.gameObject.transform.position, moveToPosition) > Mathf.Epsilon)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, moveToPosition, m_moveSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(floatingInterval);
        if(nextState!=BirdState.DESTORY)
        {
            if(!bIsFiring)
            {
                StartCoroutine(ShotBlackBullets(nextState));
            }
        }
        else
        {
            currentState = BirdState.DESTORY;
        }
    }

    IEnumerator ShotBlackBullets(BirdState stateWhenShootEnd)
    {
        bIsFiring = true;
        currentState = BirdState.SHOOTTING;
        for (int t = 0; t < 3; ++t)
        {
            for (int i = 0; i < 3; ++i)
            {
                blackBullets[i] = ObjectPooler.Dequeue(PoolKeys.BlackBullet).GetComponent<BlackBullet>();
                float angleZ = (i - 1) * deltAngle;
                blackBullets[i].SetMoveDir(angleZ, this.transform.position);
                blackBullets[i].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(shotInterval);
        }
        bIsFiring = false;
        currentState = stateWhenShootEnd;
    }
}
