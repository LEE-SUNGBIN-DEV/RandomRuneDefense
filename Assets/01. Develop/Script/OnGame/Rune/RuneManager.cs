using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuneManager : Singleton<RuneManager>
{
    public List<Rune> runeList;
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
                var windRune = GameObject.FindGameObjectsWithTag("Wind");

                for (int i = 0; i < windRune.Length; i++)
                {
                    windRune[i].GetComponent<WindRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
                }                              
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
                var windRune = GameObject.FindGameObjectsWithTag("Ice");

                for (int i = 0; i < windRune.Length; i++)
                {
                    windRune[i].GetComponent<IceRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
                }
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
                var windRune = GameObject.FindGameObjectsWithTag("Lightning");

                for (int i = 0; i < windRune.Length; i++)
                {
                    windRune[i].GetComponent<LightningRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
                }
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
                var windRune = GameObject.FindGameObjectsWithTag("Fire");

                for (int i = 0; i < windRune.Length; i++)
                {
                    windRune[i].GetComponent<FireRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
                }
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
            if (FindObjectOfType<PoisonRune>())
            {
                var windRune = GameObject.FindGameObjectsWithTag("Poison");

                for (int i = 0; i < windRune.Length; i++)
                {
                    windRune[i].GetComponent<PoisonRune>().RuneDamage += Constant.POWER_UP_DAMAGE;
                }
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
