using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour
{
    private Rigidbody2D rg2D;
    private GameObject player;
    public float sensitiveDis = 1.0f;
	// Use this for initialization
	void Start () 
    {
        rg2D = this.GetComponent<Rigidbody2D>();
        rg2D.gravityScale = 0;
        player = GameObject.FindGameObjectWithTag(Tags.Player);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(player)
        {
            Fall();
        }
	}

    void Fall()
    {
        if (this.transform.position.x - player.transform.position.x < sensitiveDis) 
        {
            rg2D.gravityScale = 1;
            Destroy(this.gameObject, 3.0f);
        }
    }

   
}
