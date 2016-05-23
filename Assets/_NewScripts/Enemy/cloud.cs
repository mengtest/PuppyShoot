using UnityEngine;
using System.Collections;

public class cloud : MonoBehaviour 
{
    public Vector3 startPosition;
    public Vector3 lightPosition1;
    public Vector3 lightPosition2;
    public Vector3 endPosition;
    public float triggerDistance;

    public float lightInterval = 2.0f;
    public float floatingInterval = 1.0f;
    public float m_moveSpeed = 0.1f;

    private enum CloudState
    {
        START_TO_POS1,
        POS1_TO_POS2,
        POS2_TO_END,
        LIGHTNING,
        DESTORY
    }
    private bool isLighting = false;
    private bool bStart = false;
    private CloudState currentState;

    private ParticleSystem particleLight;
    private BoxCollider2D lightCollider;
    private GameObject player;


	// Use this for initialization
	void Start () 
    {
        this.particleLight = transform.GetComponentInChildren<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        this.transform.position = startPosition;
        lightCollider = this.GetComponent<BoxCollider2D>();
        lightCollider.enabled = false;
        currentState = CloudState.START_TO_POS1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (player)
        {
            IsInRange();
            StartAction();
        }
	}

    private void IsInRange()
    {
        float distance = this.transform.position.x - player.transform.position.x;
        if (distance < triggerDistance)
        {
            bStart = true;
        }
    }

    private void StartAction()
    {
        if (bStart)
        {
            switch (currentState)
            {
                case CloudState.START_TO_POS1:
                    StartCoroutine(MoveToAndLightning(lightPosition1, CloudState.POS1_TO_POS2));
                    break;
                case CloudState.POS1_TO_POS2:
                    StartCoroutine(MoveToAndLightning(lightPosition2, CloudState.POS2_TO_END));
                    break;
                case CloudState.POS2_TO_END:
                    StartCoroutine(MoveToAndLightning(endPosition, CloudState.DESTORY));
                    break;
                case CloudState.DESTORY:
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }


    IEnumerator MoveToAndLightning(Vector3 moveToPosition, CloudState nextState)
    {
        while (Vector3.Distance(this.gameObject.transform.position, moveToPosition) > Mathf.Epsilon)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, moveToPosition, m_moveSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(floatingInterval);
        if (nextState != CloudState.DESTORY)
        {
            if (!isLighting)
            {
                StartCoroutine(CoroutineLightening(nextState));
            }
        }
        else
        {
            currentState = CloudState.DESTORY;
        }
    }


    IEnumerator CoroutineLightening(CloudState nextState)
    {
        isLighting = true;
        particleLight.Play();
        lightCollider.enabled = true;
        yield return new WaitForSeconds(particleLight.startLifetime);
        lightCollider.enabled = false;
        yield return new WaitForSeconds(lightInterval);
        isLighting = false;
        currentState = nextState;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(Tags.Player))
        {
            Debug.Log("FuckHappy: " + Time.time);
        }
    }
}
