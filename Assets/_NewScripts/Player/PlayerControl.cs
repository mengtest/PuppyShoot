using System;
using UnityEngine;
using System.Collections;
using UnityEditor.AnimatedValues;

public class PlayerControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.CompareTag(Tags.Background))
            {
                this.gameObject.PostNotification(Notifications.PLAYER_MOVE, new InfoEventArgs<Vector3>((Vector3)hit.point));
            }
            
            if (hit.collider.CompareTag(Tags.Enemy))
            {
                //this.gameObject.PostNotification(Notifications.SHOOT_ENEMY,
                //    new InfoEventArgs<Vector3>(hit.collider.transform.position));
                this.gameObject.GetComponent<PlayerRocketMaker>().SetEnemy(hit.collider.gameObject);
            }
        }
    }
}


