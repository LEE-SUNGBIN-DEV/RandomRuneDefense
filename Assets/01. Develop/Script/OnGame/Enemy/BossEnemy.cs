using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] GameObject bossBullet;

    public override void Start()
    {
        Health = 500;
        //MaxHealth = 500;
        MoveSpeed = Constant.BOSS_ENEMY_MOVE_SPEED;
    }

    public override void Die()
    {
        OnGameScene.Inst.TotalSP += 100;
        Health = MaxHealth;
        EnemyObjectPool.Instance.bossStage = false;
        gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        wayNum = 0;
        HealthBar.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
        StartCoroutine(MovePath());
        StartCoroutine(DestroyRuneSkill());
    }
    

    IEnumerator DestroyRuneSkill()
    {
        yield return new WaitForSeconds(Constant.BOSS_SKILL_COOL_TIME);

            for (int i = 0; i < Constant.BOSS_SKILL_COUNT; ++i)
            {
                Board.Inst.DestroyRune();
                               
                 var bulletEffect = Instantiate(bossBullet, Board.Inst.RunePosition, Quaternion.identity);
                 Destroy(bulletEffect, 2f);

                 yield return new WaitForEndOfFrame();                         
            }      
    }


    public override void OnDisable()
    {
        MaxHealth += EnemyObjectPool.Instance.stage * 100;       
        MoveSpeed = Constant.BOSS_ENEMY_MOVE_SPEED;

        EnemyObjectPool.Instance.bossStage = false;
        EnemyObjectPool.Instance.endStage = true;

        distance = 0;
        Health = MaxHealth;

        gameObject.SetActive(false);
        EnemyObjectPool.Instance.enemys.Remove(this);
        BossObjectPool.Instance.InsertQueue(gameObject);
    }
}
