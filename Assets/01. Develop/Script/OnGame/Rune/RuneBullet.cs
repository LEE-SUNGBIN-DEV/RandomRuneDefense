using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneBullet : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int BulletDamage;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject[] typeSkill;

    RUNE_TYPE runeType;
    Enemy targetEnemy;
    int skillCount;
    int bulletEffectNum;

    [SerializeField] GameObject[] effect;
    [SerializeField] GameObject[] bulletEffcet;
    
    public void SetUpBullet(Color color ,Enemy _targetEnemy ,int TowerDamage , RUNE_TYPE _runeType , int _skillCount , int _bulletEffectNum)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.color = color;       
        targetEnemy = _targetEnemy;
        BulletDamage = TowerDamage;
        runeType = _runeType;
        skillCount = _skillCount;
        bulletEffectNum = _bulletEffectNum;

        StartCoroutine(AttackCo());
    }

    IEnumerator AttackCo()
    {
        bulletEffcet[bulletEffectNum].SetActive(true);

        while (true)
        {           
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position,
                                                     bulletSpeed * Time.deltaTime);          
            yield return null;

            if((transform.position - targetEnemy.transform.position).sqrMagnitude < bulletSpeed * Time.deltaTime)
            {
                transform.position = targetEnemy.transform.position;
                break;
            }
        }        
        //데미지를 입힌다..        
        if (targetEnemy != null)       
        {           
            spriteRenderer.enabled = false;
            targetEnemy.Damage(BulletDamage);
            GameObject damageTMP = DamageObjectPool.Instance.GetQueue();
            damageTMP.GetComponent<DamageUI>().Setup(targetEnemy.transform , BulletDamage);                      
        }

        Die();
    }

    void Die()
    {
        bulletEffcet[bulletEffectNum].SetActive(false);
        
        switch (runeType)
        {
            case RUNE_TYPE.WIND:
                StartCoroutine(NomalAttack(0));
                break;

            case RUNE_TYPE.POISON:
                if (skillCount == 3)
                {
                    StartCoroutine(Poison());                   
                }
                else
                {
                    StartCoroutine(NomalAttack(1));
                }
                break;

            case RUNE_TYPE.ICE:             
                    StartCoroutine(Slow());                                         
                break;

            case RUNE_TYPE.FIRE:
                if (skillCount == 3)
                {
                    StartCoroutine(Fire());
                }
                else 
                {
                    StartCoroutine(NomalAttack(3));
                }
                break;

            case RUNE_TYPE.LIGHTNING:
                if(skillCount == 3)
                {
                    StartCoroutine(Lightning());
                }
                else 
                {
                    StartCoroutine(NomalAttack(4));
                }
                break;
        }   
    }
    #region Skill Set

    IEnumerator NomalAttack(int effectNumber)
    {       
        switch(effectNumber)
        {
            case 0:
                effect[0].SetActive(true);
                break;
            case 1:
                effect[1].SetActive(true);               
                break;
            case 2:
                //; ; // 얼음 이펙트 넣을 예정
                break;
            case 3:
                effect[3].SetActive(true);                
                break;
            case 4:
                effect[4].SetActive(true);                
                break;
        }       

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < effect.Length; ++i)
        {
            effect[i].SetActive(false);            
        }
        BulletObjetPool.Instance.InsertQueue(gameObject);
    }
    
    IEnumerator Poison()
    {
        typeSkill[2].SetActive(true); 
        yield return new WaitForSeconds(Constant.SKILL_COOL_TIME);
        typeSkill[2].SetActive(false);

        BulletObjetPool.Instance.InsertQueue(gameObject);
    }
    IEnumerator Slow()
    {
        effect[2].SetActive(true);

        targetEnemy.moveSpeed -= 0.3f;
        targetEnemy.HealthBar.GetComponent<Image>().color = Color.blue;

        yield return new WaitForSeconds(Constant.SLOW_TIME);

        effect[2].SetActive(false);

        targetEnemy.moveSpeed += 0.3f;
        targetEnemy.HealthBar.GetComponent<Image>().color = Color.red;

        BulletObjetPool.Instance.InsertQueue(gameObject);
    }
    IEnumerator Fire()
    {
        typeSkill[0].SetActive(true);     
        yield return new WaitForSeconds(Constant.SKILL_COOL_TIME);
        typeSkill[0].SetActive(false);
        BulletObjetPool.Instance.InsertQueue(gameObject);
    }
    IEnumerator Lightning()
    {
        typeSkill[1].SetActive(true);        
        yield return new WaitForSeconds(Constant.SKILL_COOL_TIME);
        typeSkill[1].SetActive(false);
        BulletObjetPool.Instance.InsertQueue(gameObject);
    }
    #endregion
}
