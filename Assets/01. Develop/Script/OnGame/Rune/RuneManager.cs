using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuneManager : Singleton<RuneManager>
{
    public List<GameObject> runeList;
    public Dictionary<int, Rune> runeDictionary;

    [SerializeField] TMP_Text[] value_Sp_Text;


    // 룬 업그레이드에 들어가는 비용
    [SerializeField] private int windRuneCost;
    [SerializeField] private int iceRuneCost;
    [SerializeField] private int lightningRuneCost;
    [SerializeField] private int fireRuneCost;
    [SerializeField] private int poisonRuneCost; 

    private void Start()
    {       
        RuneDictionary = new Dictionary<int, Rune>();
        for (int i = 0; i < RuneList.Count; ++i)
        {
            RuneDictionary.Add(RuneList[i].GetComponent<Rune>().RuneNumber, RuneList[i].GetComponent<Rune>());
            RuneList[i].GetComponent<Rune>().Awake();
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

    #region RUNE_POWER_UP
    public void WindPowerUP()
    {
        // sp 관리를 위한 onGameScene.TotalSp
        if (OnGameScene.Inst.TotalSP >= WindRuneCost)
        {
            Line[] lines = Board.Inst.lines;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].tiles.Length; j++)
                {
                    if (lines[i].tiles[j].rune != null && lines[i].tiles[j].rune.RuneType == RUNE_TYPE.WIND)
                    {
                        lines[i].tiles[j].rune.RuneDamage += Constant.POWER_UP_DAMAGE;
                    }
                }
            }

            runeDictionary[Constant.WIND_RUNE].GetComponent<WindRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            WindRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= WindRuneCost;
        }
    }
    public void IcePowerUP()
    {        
        // sp 관리를 위한 onGameScene.TotalSp
        if (OnGameScene.Inst.TotalSP >= IceRuneCost)
        {
            Line[] lines = Board.Inst.lines;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].tiles.Length; j++)
                {                   
                    if (lines[i].tiles[j].rune != null && lines[i].tiles[j].rune.RuneType == RUNE_TYPE.ICE)
                    {
                        lines[i].tiles[j].rune.RuneDamage += Constant.POWER_UP_DAMAGE;
                    }
                }
            }
            runeDictionary[Constant.ICE_RUNE].GetComponent<IceRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            IceRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= IceRuneCost;
        }
    }
    public void LightningPowerUP()
    {        
        // sp 관리를 위한 onGameScene.TotalSp
        if (OnGameScene.Inst.TotalSP >= LightningRuneCost)
        {
            Line[] lines = Board.Inst.lines;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].tiles.Length; j++)
                {                   
                    if (lines[i].tiles[j].rune != null && lines[i].tiles[j].rune.RuneType == RUNE_TYPE.LIGHTNING)
                    {
                        lines[i].tiles[j].rune.RuneDamage += Constant.POWER_UP_DAMAGE;
                    }
                }
            }
            runeDictionary[Constant.LIGHTNING_RUNE].GetComponent<LightningRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            LightningRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= LightningRuneCost;
        }
    }
    public void FirePowerUP()
    {        
        // sp 관리를 위한 onGameScene.TotalSp
        if (OnGameScene.Inst.TotalSP >= FireRuneCost)
        {
            Line[] lines = Board.Inst.lines;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].tiles.Length; j++)
                {
                    if (lines[i].tiles[j].rune != null && lines[i].tiles[j].rune.RuneType == RUNE_TYPE.FIRE)
                    {
                        lines[i].tiles[j].rune.RuneDamage += Constant.POWER_UP_DAMAGE;
                    }
                }
            }
            runeDictionary[Constant.FIRE_RUNE].GetComponent<FireRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            FireRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= FireRuneCost;
        }
    }
    public void PoisonPowerUP()
    {      
        // sp 관리를 위한 onGameScene.TotalSp
        if (OnGameScene.Inst.TotalSP >= PoisonRuneCost)
        {
            Line[] lines = Board.Inst.lines;

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].tiles.Length; j++)
                {
                    if (lines[i].tiles[j].rune != null && lines[i].tiles[j].rune.RuneType == RUNE_TYPE.POISON)
                    {
                        lines[i].tiles[j].rune.RuneDamage += Constant.POWER_UP_DAMAGE;
                    }
                }
            }
            runeDictionary[Constant.POISON_RUNE].GetComponent<PoisonRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            PoisonRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= PoisonRuneCost;
        }
    }
    #endregion

    #region Property
    public List<GameObject> RuneList
    {
        get { return runeList; }
        private set { runeList = value; }
    }
    public Dictionary<int, Rune> RuneDictionary
    {
        get { return runeDictionary; }
        private set { runeDictionary = value; }
    }

    public int WindRuneCost
    {
        get => windRuneCost;
        set
        {
            windRuneCost = value;
            value_Sp_Text[Constant.WIND_RUNE].text = windRuneCost.ToString();
        }
    }
    public int IceRuneCost
    {
        get => iceRuneCost;
        set
        {
            iceRuneCost = value;
            value_Sp_Text[Constant.ICE_RUNE].text = iceRuneCost.ToString();
        }
    }
    public int LightningRuneCost
    {
        get => lightningRuneCost;
        set
        {
            lightningRuneCost = value;
            value_Sp_Text[Constant.LIGHTNING_RUNE].text = lightningRuneCost.ToString();
        }
    }
    public int FireRuneCost
    {
        get => fireRuneCost;
        set
        {
            fireRuneCost = value;
            value_Sp_Text[Constant.FIRE_RUNE].text = fireRuneCost.ToString();
        }
    }
    public int PoisonRuneCost
    {
        get => poisonRuneCost;
        set
        {
            poisonRuneCost = value;
            value_Sp_Text[Constant.POISON_RUNE].text = poisonRuneCost.ToString();
        }
    }


    #endregion
}
