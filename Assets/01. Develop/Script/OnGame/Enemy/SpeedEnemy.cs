using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemy : Enemy
{
    public override void Awake()
    {
        MaxHealth = MaxHealth;
        Health = Health;
        originSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
    }
    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        MaxHealth += 1;
        HealthBar.GetComponent<UnityEngine.UI.Image>().color = Color.red;        
        distance = 0;

        EnemyObjectPool.Instance.enemys.Remove(this);
        EnemyObjectPool.Instance.InsertQueue(gameObject);
    }
}
