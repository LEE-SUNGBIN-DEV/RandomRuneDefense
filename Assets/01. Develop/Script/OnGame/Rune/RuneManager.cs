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
