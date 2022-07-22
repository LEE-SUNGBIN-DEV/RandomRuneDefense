using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRune : Rune
{
    public override void OnEnable()
    {
        StartCoroutine(AttackCo());
    }
    IEnumerator AttackCo()
    {
        while (true)
        {
            Enemy AttackEnemy = null;

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
                bulletObj.GetComponent<RuneBullet>().SetUpBullet(runeColor, AttackEnemy, runeDamage, runeType, skillCount);
            }
            yield return new WaitForSeconds(runeAttackSpeed);
        }
    }
    IEnumerator Wind()
    {
        runeAttackSpeed = 0.3f;

        yield return new WaitForSeconds(Constant.SKILL_COOL_TIME);

        runeAttackSpeed = 0.5f;
        skillCount = 0;

    }
}
