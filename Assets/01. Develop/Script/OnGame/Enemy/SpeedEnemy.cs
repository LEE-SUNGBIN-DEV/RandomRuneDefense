using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemy : Enemy
{
    void Start()
    {
        health = 50;
        maxHealth = 50;
        moveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
    }
}
