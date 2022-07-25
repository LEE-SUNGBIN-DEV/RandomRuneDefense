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
        MaxHealth += 10; // Á×À»¶§ ¸¶´Ù °­ÇØÁü.      
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;

        distance = 0;
        Health = MaxHealth;

        gameObject.SetActive(false);
        EnemyObjectPool.Instance.enemys.Remove(this);
        EnemyObjectPool.Instance.InsertQueue(gameObject);
    }
}
