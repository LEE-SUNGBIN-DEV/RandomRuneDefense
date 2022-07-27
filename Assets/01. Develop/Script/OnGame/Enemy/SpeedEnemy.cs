using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemy : Enemy
{
    public override void Start()
    {
        Health = Health;
        MaxHealth = MaxHealth;
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
    }
    public override void OnEnable()
    {
        wayNum = 0;
        Health = MaxHealth;
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
        HealthBar.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;

        //�̹� ������ƮǮ�� �� ���ӿ�����Ʈ���� �ڷ�ƾ�� �����Ű�� �� ����
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(MovePath());
        }        
    }

    public override void OnDisable()
    {
        MaxHealth += 10;
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
        HealthBar.GetComponent<UnityEngine.UI.Image>().color = Color.red;

        distance = 0;

        gameObject.SetActive(false);
        EnemyObjectPool.Instance.enemys.Remove(this);
        EnemyObjectPool.Instance.InsertQueue(gameObject);
    }
}
