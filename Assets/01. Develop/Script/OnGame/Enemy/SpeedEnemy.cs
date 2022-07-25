using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemy : Enemy
{
    public override void Start()
    {
        health = 100;
        maxHealth = 100;
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
    }

    public override void Die()
    {
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
        MaxHealth += 10; // Á×À»¶§ ¸¶´Ù °­ÇØÁü.
        OnGameScene.Inst.TotalSP += 10;
        gameObject.SetActive(false);
    }
}
