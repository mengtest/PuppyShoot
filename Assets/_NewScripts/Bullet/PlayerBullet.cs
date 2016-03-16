using UnityEngine;
using System.Collections;

public class PlayerBullet : Bullet 
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.Enemy))
        {
            //爆炸特效



            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
