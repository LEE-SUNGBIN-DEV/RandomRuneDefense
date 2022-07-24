using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuneManager : Singleton<RuneManager>
{
    public List<Rune> runeList;
    public Dictionary<int, Rune> runeDictionary;

    [SerializeField] TMP_Text[] value_Sp_Text;

    private int windRuneCost;
    private int iceRuneCost;
    private int lightningRuneCost;
    private int fireRuneCost;
    private int poisonRuneCost; 

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

    #region RUNE_POWER_UP
    public void WindPowerUP()
    {
        // sp 관리를 위한 onGameScene.TotalSp
        if (OnGameScene.Inst.TotalSP >= WindRuneCost)
        {
            if(FindObjectOfType<WindRune>())
            {
                FindObjectOfType<WindRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            }

            runeDictionary[Constant.WIND_RUNE].GetComponent<WindRune>().RuneDamage += Constant.POWER_UP_DAMAGE;     
            WindRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= WindRuneCost;
        }
    }
    public void IcePowerUP()
    {
        if (OnGameScene.Inst.TotalSP >= IceRuneCost)
        {
            if (FindObjectOfType<IceRune>())
            {
                FindObjectOfType<IceRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            }

            runeDictionary[Constant.ICE_RUNE].GetComponent<IceRune>().RuneDamage += Constant.POWER_UP_DAMAGE;

            IceRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= WindRuneCost;
        }
    }
    public void LightningPowerUP()
    {
        if (OnGameScene.Inst.TotalSP >= LightningRuneCost)
        {
            if (FindObjectOfType<LightningRune>())
            {
                FindObjectOfType<LightningRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            }

            runeDictionary[Constant.LIGHTNING_RUNE].GetComponent<LightningRune>().RuneDamage += Constant.POWER_UP_DAMAGE;

            LightningRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= WindRuneCost;
        }
    }
    public void FirePowerUP()
    {
        if (OnGameScene.Inst.TotalSP >= FireRuneCost)
        {
            if (FindObjectOfType<FireRune>())
            {
                FindObjectOfType<FireRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            }

            runeDictionary[Constant.FIRE_RUNE].GetComponent<FireRune>().RuneDamage += Constant.POWER_UP_DAMAGE;   
            
            FireRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= WindRuneCost;
        }
    }
    public void PoisonPowerUP()
    {
        if (OnGameScene.Inst.TotalSP >= PoisonRuneCost)
        {
            if (FindObjectOfType<WindRune>())
            {
                FindObjectOfType<PoisonRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
            }

            runeDictionary[Constant.POISON_RUNE].GetComponent<PoisonRune>().RuneDamage += Constant.POWER_UP_DAMAGE;

            PoisonRuneCost += Constant.POWER_UP_COST;
            OnGameScene.Inst.TotalSP -= WindRuneCost;
        }
    }
    #endregion

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
            value_Sp_Text[Constant.ICE_RUNE].text = value.ToString();
        }
    }
    public int LightningRuneCost
    {
        get => lightningRuneCost;
        set
        {
            lightningRuneCost = value;
            value_Sp_Text[Constant.LIGHTNING_RUNE].text = value.ToString();
        }
    }
    public int FireRuneCost
    {
        get => fireRuneCost;
        set
        {
            fireRuneCost = value;
            value_Sp_Text[Constant.FIRE_RUNE].text = value.ToString();
        }
    }
    public int PoisonRuneCost
    {
        get => poisonRuneCost;
        set
        {
            poisonRuneCost = value;
            value_Sp_Text[Constant.POISON_RUNE].text = value.ToString();
        }
    }


    #endregion
}
