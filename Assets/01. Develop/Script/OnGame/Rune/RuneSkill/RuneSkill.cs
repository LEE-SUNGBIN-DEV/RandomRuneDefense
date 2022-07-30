using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkill : MonoBehaviour
{
    [SerializeField] protected float damage;

    #region Property
    public float Damage
    {
        get => damage;
        set => damage = value;
    }
    #endregion

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(RuneSkillDamage(collision));
        }      
    }  
    public  IEnumerator RuneSkillDamage(Collider2D collision)
    {
        for (int i = 0; i < Constant.SKILL_TIME; i++)
        {          
            collision.GetComponent<Enemy>().Health -= damage;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
