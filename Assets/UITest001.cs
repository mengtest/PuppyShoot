using UnityEngine;
using System.Collections;

public class UITest001 : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.K))
        {
            player.GetComponent<PlayerStatus>().playerHealth--;
            this.PostNotification(Notifications.LOSE_HEALTH);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            player.GetComponent<PlayerStatus>().playerHealth++;
            this.PostNotification(Notifications.GET_HEAL);
        }
	}
}
