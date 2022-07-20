using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RUNE_TYPE
{
    WIND = 0,   
    ICE = 1,
    LIGHTNING = 2,
    FIRE = 3,
    POISON = 4
}

public class Rune : MonoBehaviour
{
    [SerializeField] private int runeNumber;   
    [SerializeField] private RUNE_TYPE runeType;
    [SerializeField] private SpriteRenderer runeRenderer;
    [SerializeField] private int runeDamage;
    [SerializeField] private float runeAttackSpeed;
    [SerializeField] private Color runeColor;
    [SerializeField] private GameObject[] level;
    [SerializeField] private int skillCount;   

    #region Property
    public int RuneNumber
    {
        get { return runeNumber; }
        set { runeNumber = value; }
    }
    public RUNE_TYPE RuneType
    {
        get { return runeType; }
        set { runeType = value; }
    }
    public SpriteRenderer RuneRenderer
    {
        get { return runeRenderer; }
        set { runeRenderer = value; }
    }
    public int RuneDamage
    {
        get { return runeDamage; }
        set { runeDamage = value; }
    }
    public float RuneAttackSpeed
    {
        get { return runeAttackSpeed; }
        set { runeAttackSpeed = value; }
    }
    public Color RuneColor
    {
        get { return runeColor; }
        set { runeColor = value; }
    }
    public GameObject[] Level
    {
        get { return level; }
        set { level = value; }
    }
    #endregion

    public void RuneLevelUP(int _level)
    {
        for (int i = 0; i < level.Length; ++i)
        {
            level[i].SetActive(false);
        }
        level[_level].SetActive(true);
    }

    public void OnEnable()
    {
        StartCoroutine(AttackCo());
    }
    IEnumerator AttackCo()
    {
        while (true)
        {
            Enemy AttackEnemy = null;

            switch (runeType)
            {
                case RUNE_TYPE.WIND:                               
                case RUNE_TYPE.ICE:
                case RUNE_TYPE.FIRE:                  
                case RUNE_TYPE.LIGHTNING:
                    AttackEnemy = EnemyObjectPool.Instance.GetFirstEnemy();
                    
                    if(skillCount == 3)
                    {
                        skillCount = 0;
                    }
                    skillCount += 1;

                    break;
                case RUNE_TYPE.POISON:
                    AttackEnemy = EnemyObjectPool.Instance.GetRandomEnemy();

                    if (skillCount == 3)
                    {
                        skillCount = 0;
                    }
                    skillCount += 1;
                    break;
            }
            if (AttackEnemy != null)
            {
                GameObject bulletObj = BulletObjetPool.Instance.GetQueue();
                bulletObj.transform.position = this.transform.position;
                bulletObj.GetComponent<RuneBullet>().SetUpBullet(runeColor, AttackEnemy, runeDamage, runeType , skillCount);              
            }
            yield return new WaitForSeconds(runeAttackSpeed);
        }
    }
}
