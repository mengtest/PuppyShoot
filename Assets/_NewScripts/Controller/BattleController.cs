using System;
using UnityEngine;
using System.Collections;

public class BattleController: MonoBehaviour
{
    private Coroutine MoveCoroutine = null;

    void Awake()
    {
        this.AddObserver(Notifications.PLAYER_MOVE, PlayerMove);
        this.AddObserver(Notifications.SHOOT_ENEMY,ShootEnemy);
        this.AddObserver(Notifications.UPDATE_SCORE,UpdateScore);
    }

    
    void OnDestroy()
    {
        this.RemoveObserver(Notifications.PLAYER_MOVE);
        this.RemoveObserver(Notifications.SHOOT_ENEMY);
        this.RemoveObserver(Notifications.UPDATE_SCORE);
    }


    void PlayerMove(object sender, EventArgs e)
    {
        if (MoveCoroutine != null)
        {
            StopCoroutine(MoveCoroutine);
        }

        GameObject player = sender as GameObject;

        InfoEventArgs<Vector3> pointEvent = (InfoEventArgs<Vector3>)e;

        Vector3 target = pointEvent.info;

        MoveCoroutine = StartCoroutine(MoveTo(player, target));
    }


    void ShootEnemy(object sender, EventArgs e)
    {
        InfoEventArgs<Vector3> enemyPos = (InfoEventArgs<Vector3>) e;
        GameObject shooter =sender as GameObject;
        
        Vector3 shooterPos = shooter.transform.position;
        Vector3 enemyPosition = enemyPos.info;
        
        Poolable obj=ObjectPooler.Dequeue(PoolKeys.Bullets);
        obj.transform.localPosition = shooterPos;
        
        PlayerBullet playerBullet = obj.gameObject.AddComponent<PlayerBullet>();
        playerBullet.TargetPosition = enemyPosition;
        
        obj.gameObject.SetActive(true);
        playerBullet.Shoot();
        
        Debug.Log(shooterPos+"ShootEnemy"+enemyPosition);
    }


    void UpdateScore(object sender, EventArgs e)
    {
        
    }

    IEnumerator MoveTo(GameObject player,Vector3 target)
    {
        while (Vector3.Distance(player.transform.position, target)>Mathf.Epsilon)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 3.0f*Time.deltaTime);
            yield return 0;
        }
    }
}
