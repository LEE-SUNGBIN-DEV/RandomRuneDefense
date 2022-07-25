using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRune : Rune
{
    private void Start()
    {
        runeDamage = 5;
    }
    public override void OnEnable()
    {
        StartCoroutine(AttackCo());
    }
    IEnumerator AttackCo()
    {
        while (true)
        {
            Enemy AttackEnemy;

            AttackEnemy = EnemyObjectPool.Instance.GetFirstEnemy();
            if (skillCount == 5)
            {
                StartCoroutine(Wind());
            }

            if (AttackEnemy != null)
            {
                skillCount += 1;
                GameObject bulletObj = BulletObjetPool.Instance.GetQueue();
                bulletObj.transform.position = this.transform.position;
                bulletObj.GetComponent<RuneBullet>().SetUpBullet(runeColor, AttackEnemy, runeDamage, runeType, skillCount , 0);
                if (skillCount == 5)
                {
                    skillEffect.Play();
                }
            }
            yield return new WaitForSeconds(runeAttackSpeed);
        }
    }
    IEnumerator Wind()
    {
        runeAttackSpeed = 0.3f;
        
        yield return new WaitForSeconds(Constant.SKILL_ON_TIME);

        runeAttackSpeed = 0.5f;

        skillCount = 0;       
    }
}
