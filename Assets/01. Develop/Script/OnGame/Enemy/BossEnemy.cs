using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{

    public override void Start()
    {
        health = 500;
        maxHealth = 500;
        MoveSpeed = Constant.BOSS_ENEMY_MOVE_SPEED;
    }

    public override void Die()
    {
        MoveSpeed = Constant.BOSS_ENEMY_MOVE_SPEED;
        MaxHealth += 100; // Á×À»¶§ ¸¶´Ù °­ÇØÁü.
        OnGameScene.Inst.TotalSP += 100;
        gameObject.SetActive(false);
    }
}
