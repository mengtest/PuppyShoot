using UnityEngine;
using System.Collections;

public class EnemyReaction : MonoBehaviour {

    public Vector3 Position
    {
        get { return this.transform.position; }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Bullet))
        {
            this.PostNotification(Notifications.UPDATE_SCORE);
        }
    }
}
