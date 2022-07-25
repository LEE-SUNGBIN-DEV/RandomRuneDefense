using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{

    public override void Start()
    {
        Health = 500;
        MaxHealth = 500;
        MoveSpeed = Constant.BOSS_ENEMY_MOVE_SPEED;
    }

    public override void Die()
    {
        OnGameScene.Inst.TotalSP += 100;
        EnemyObjectPool.Instance.bossStage = false;
        gameObject.SetActive(false);
    }

    public override void OnDisable()
    {
        MaxHealth += 100;       
        MoveSpeed = Constant.BOSS_ENEMY_MOVE_SPEED;

        EnemyObjectPool.Instance.bossStage = false;

        distance = 0;
        Health = MaxHealth;

        gameObject.SetActive(false);
        EnemyObjectPool.Instance.enemys.Remove(this);
        BossObjectPool.Instance.InsertQueue(gameObject);
    }
}
