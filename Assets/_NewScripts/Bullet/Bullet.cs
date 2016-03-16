using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    protected Vector3 targetPosition;
    protected Vector3 movedir;

    protected Vector3 Position
    {
        get { return this.gameObject.transform.position; }
    }
    

    public Vector3 TargetPosition
    {
        set { targetPosition = value; }
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public virtual void Shoot()
    {
        movedir = (targetPosition - Position).normalized;
        this.transform.LookAt(targetPosition);
        GetComponent<Rigidbody2D>().velocity = movedir * 5.0f;
    }


}
