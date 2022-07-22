using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemy : Enemy
{
    void Start()
    {
        health = 100;
        maxHealth = 100;
        moveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
    }
}
