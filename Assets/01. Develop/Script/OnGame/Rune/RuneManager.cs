using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : Singleton<RuneManager>
{
    public List<Rune> runeList;
    public Dictionary<int, Rune> runeDictionary;

    private void Start()
    {
        RuneDictionary = new Dictionary<int, Rune>();
        for (int i = 0; i < RuneList.Count; ++i)
        {
            RuneDictionary.Add(RuneList[i].RuneNumber, RuneList[i]);
        }
    }

    public Rune FindRuneFromList(int runeNumber)
    {
        if(runeDictionary.ContainsKey(runeNumber))
        {
            return runeDictionary[runeNumber];
        }
        else
        {
            return null;
        }
    }
    public Rune RuneTypeChange(Rune rune , int level)
    {
        int randomType = Random.Range(0, Constant.RUNE_RANDOM_MAX_VALUE);              
        if (RuneDictionary.ContainsKey(randomType))
        {
            Debug.Log("¹Ù²ñ");
            
            var _rune = RuneDictionary[randomType].GetComponent<Rune>();            

            rune.RuneNumber = _rune.RuneNumber;
            rune.RuneType = _rune.RuneType;
            rune.RuneRenderer.sprite = _rune.RuneRenderer.sprite;
            rune.RuneDamage += _rune.RuneDamage;
            rune.RuneAttackSpeed = _rune.RuneAttackSpeed;
            rune.RuneColor = _rune.RuneColor;          
            rune.RuneLevelUP(level); 
          
            return rune;       
        }
        else
        {
            return null;
        }
    }


    #region Property
    public List<Rune> RuneList
    {
        get { return runeList; }
        private set { runeList = value; }
    }
    public Dictionary<int, Rune> RuneDictionary
    {
        get { return runeDictionary; }
        private set { runeDictionary = value; }
    }
    #endregion
}
