using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRune : Rune
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
            if (skillCount == 3)
            {
                skillCount = 0;
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
}
