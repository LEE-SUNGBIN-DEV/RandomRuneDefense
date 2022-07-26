using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemy : Enemy
{
    public override void Start()
    {
        Health = 100;
        MaxHealth = 100;
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
    }

    public override void OnDisable()
    {                    
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;

        distance = 0;

        gameObject.SetActive(false);
        EnemyObjectPool.Instance.enemys.Remove(this);
        EnemyObjectPool.Instance.InsertQueue(gameObject);
    }
}
