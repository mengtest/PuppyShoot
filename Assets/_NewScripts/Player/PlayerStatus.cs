using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    public int playerHealth = 5;
    
    private enum State
    {
        INITIALIZE,
        INVISIBLE,
        NOWPLAYING,
        STARTdESTRUCTION,
        WAITING,
        RESTART
    }

    private State programState = State.INITIALIZE;
    private int waitTimeAfterExplosion = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


}
