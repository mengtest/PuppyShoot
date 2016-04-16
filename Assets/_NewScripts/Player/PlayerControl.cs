using System;
using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float m_moveSpeed = 3.0f;

    private bool m_bIsAlive = false;
    private PlayerStatus m_playerStatus;
    private PlayerRocketMaker m_playerRocketMaker;
    private Coroutine MoveCoroutine = null;

    void Start()
    {
        m_playerStatus = this.gameObject.GetComponent<PlayerStatus>();
        m_playerRocketMaker = this.gameObject.GetComponent<PlayerRocketMaker>();
        SetIsAlive(true);
    }

    private void Update()
    {
        if(m_bIsAlive)
        {
            if(m_playerStatus)
            {
                if(m_playerStatus.GetCanPlay())
                {
                    HandleTouchInput();
                }
            }
        }
    }

    private void HandleTouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.CompareTag(Tags.Background))
            {
                //this.gameObject.PostNotification(Notifications.PLAYER_MOVE, new InfoEventArgs<Vector3>((Vector3)hit.point));
                PlayerMoveTo((Vector3)hit.point);
            }

            if (hit.collider.CompareTag(Tags.Enemy))
            {
                ShootEnemy(hit.collider.gameObject);
            }
        }
    }

    private void PlayerMoveTo(Vector3 toPosition)
    {
        if (MoveCoroutine != null)
        {
            StopCoroutine(MoveCoroutine);
        }
        MoveCoroutine = StartCoroutine(MoveTo(toPosition));
    }
    IEnumerator MoveTo(Vector3 toPosition)
    {
        while (Vector3.Distance(this.gameObject.transform.position, toPosition) > Mathf.Epsilon)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, toPosition, m_moveSpeed * Time.deltaTime);
            yield return 0;
        }
    }

    private void ShootEnemy(GameObject obj_Target)
    {
        m_playerRocketMaker.SetEnemy(obj_Target);
    }
    
    public void SetIsAlive(bool bIsAlive)
    {
        this.m_bIsAlive = bIsAlive;
    }
}


