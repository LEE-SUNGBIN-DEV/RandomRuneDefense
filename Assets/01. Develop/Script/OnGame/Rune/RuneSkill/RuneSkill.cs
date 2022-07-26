using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkill : MonoBehaviour
{
    [SerializeField] protected float damage;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(RuneSkillDamage(collision));
        }      
    }   

    IEnumerator RuneSkillDamage(Collider2D collision)
    {
        for (int i = 0; i < Constant.SKILL_TIME; i++)
        {
            GameObject damageTMP = DamageObjectPool.Instance.GetQueue();
            damageTMP.GetComponent<DamageUI>().Setup(collision.transform, (int)damage);

            collision.GetComponent<Enemy>().Health -= damage;

            yield return new WaitForSeconds(0.3f);
        }
    }
}
