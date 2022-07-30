using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] GameObject bossBullet;

    public override void Awake()
    {
        MaxHealth = MaxHealth;
        Health = Health;     
        originSpeed = Constant.BOSS_ENEMY_MOVE_SPEED;
    }

    public override void Die()
    {
        OnGameScene.Inst.TotalSP += 100;       
        EnemyObjectPool.Instance.bossStage = false;
        gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        wayNum = 0;
        MaxHealth += 100;
        Health = MaxHealth;
        CurrentSpeed = OriginSpeed;
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

                 if (Board.Inst.RunePosition != Vector3.zero)
                 {
                     var bulletEffect = Instantiate(bossBullet, Board.Inst.RunePosition, Quaternion.identity);
                     Destroy(bulletEffect, 2f);
                     yield return new WaitForEndOfFrame();
                 }
                 else
                 {
                     yield break;
                 }

            }      
    }


    public override void OnDisable()
    {
        EnemyObjectPool.Instance.bossStage = false;
        EnemyObjectPool.Instance.endStage = true;

        Constant.BOSS_SKILL_COUNT += 1;
        Debug.Log(Constant.BOSS_SKILL_COUNT);
        distance = 0;        

        gameObject.SetActive(false);
        EnemyObjectPool.Instance.enemys.Remove(this);
        BossObjectPool.Instance.InsertQueue(gameObject);
    }
}
