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
    [SerializeField] protected int runeNumber;   
    [SerializeField] protected RUNE_TYPE runeType;
    [SerializeField] protected SpriteRenderer runeRenderer;
    [SerializeField] protected int runeDamage;
    [SerializeField] protected float runeAttackSpeed;
    [SerializeField] protected Color runeColor;
    [SerializeField] protected GameObject[] level;
    [SerializeField] protected GameObject skillEffect;
    [SerializeField] protected int skillCount;   

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

    public virtual void OnEnable() {}  
}
