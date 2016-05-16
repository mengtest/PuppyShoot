using UnityEngine;
using System.Collections;

public class AirPlane : MonoBehaviour 
{
    private const float startMoveDis = 30.0f;
    private const float disappearDis = 40.0f;
    public float startThrowBombDis = 10.0f;
    public float airplaneSpeed = 5.0f;
    public float throwInterval = 0.5f;

    private GameObject player;
    //private bool isPlayerInRange =  false;
    private Bomb bombObj;
    private bool isThrowing = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(player)
        {
            IsOverDistance();
            StartMove();
            ThrowBomb();
        }
	}

    private void StartMove()
    {
        if ((this.transform.position.x - player.transform.position.x) <= startMoveDis)
        {
            this.transform.Translate(new Vector3(-Time.deltaTime * airplaneSpeed, 0, 0));
        }
    }

    private void ThrowBomb()
    {
        if ((this.transform.position.x - player.transform.position.x) <= startThrowBombDis)
        {
            if (!isThrowing)
            {
                StartCoroutine(StartThrowBomb());
            }
        }
    }

    private void IsOverDistance()
    {
        if (!player)
        {
            return;
        }

        float distance = player.transform.position.x - transform.position.x;
        if (distance > disappearDis)
        {
            HidePlane();
        }
    }

    private void HidePlane()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator StartThrowBomb()
    {
        isThrowing = true;
        bombObj = ObjectPooler.Dequeue(PoolKeys.Bomb).GetComponent<Bomb>();
        bombObj.Initial(this.transform.position);
        bombObj.gameObject.SetActive(true);
        yield return new WaitForSeconds(throwInterval);
        bombObj = ObjectPooler.Dequeue(PoolKeys.Bomb).GetComponent<Bomb>();
        bombObj.Initial(this.transform.position);
        bombObj.gameObject.SetActive(true);
        yield return new WaitForSeconds(throwInterval);
        bombObj = ObjectPooler.Dequeue(PoolKeys.Bomb).GetComponent<Bomb>();
        bombObj.Initial(this.transform.position);
        bombObj.gameObject.SetActive(true);
        yield return null;
    }
}
