using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class EndingScrollController : MonoBehaviour 
{

	public float scrollSpeed = 0.05f;				
	public float startPosition = -1.0f;			
	

	private float stopPositionY= 1.0f;
    private bool broll = true;
	
	void Start()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x ,startPosition,0);
    }

    void Update()
    {
        if(!broll)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Opening");
            }
        }
    }
	
    void FixedUpdate()
    {
        if(broll)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + scrollSpeed * Time.deltaTime, 0);
            if (this.transform.position.y >= stopPositionY)
            {
                broll = false;
            }
        }
        
    }
}
