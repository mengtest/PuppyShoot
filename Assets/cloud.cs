using UnityEngine;
using System.Collections;

public class cloud : MonoBehaviour 
{
    private ParticleSystem particleLight;
    public float lightInterval = 2.0f;
    private bool isLighting = false;
    private BoxCollider2D lightCollider;

	// Use this for initialization
	void Start () 
    {
        this.particleLight = transform.GetComponentInChildren<ParticleSystem>();
        lightCollider = this.GetComponent<BoxCollider2D>();
        lightCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(!isLighting)
        {
            StartCoroutine(CoroutineLightening());
        }
	}

    IEnumerator CoroutineLightening()
    {
        isLighting = true;
        particleLight.Play();
        lightCollider.enabled = true;
        yield return new WaitForSeconds(particleLight.startLifetime);
        lightCollider.enabled = false;
        yield return new WaitForSeconds(lightInterval);
        isLighting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(Tags.Player))
        {
            Debug.Log("FuckHappy: " + Time.time);
        }
    }
}
