using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerStats
{
    #region Event
    public static event UnityAction<PlayerStats> onPlayerStatsChanged;
    #endregion

    [SerializeField] private int level;
    [SerializeField] private float experience;
    [SerializeField] private float normalAttackPower;
    [SerializeField] private float bossAttackPower;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalDamage;
    [SerializeField] private float diceControl;
    [SerializeField] private float spAcquisition;
    [SerializeField] private float spUsage;

    PlayerStats()
    {
        Card.onEquipCard -= EquipCard;
        Card.onEquipCard += EquipCard;
        Card.onReleaseCard -= ReleaseCard;
        Card.onReleaseCard += ReleaseCard;
    }

    ~PlayerStats()
    {
        Card.onEquipCard -= EquipCard;
        Card.onReleaseCard -= ReleaseCard;
    }

    public void OnLoadPlayerStats()
    {
        Level = level;
        Experience = experience;
    }

    public void EquipCard(Card requestCard)
    {
        NormalAttackPower += requestCard.NormalAttackPower;
        BossAttackPower += requestCard.BossAttackPower;
        AttackSpeed += requestCard.AttackSpeed;
        CriticalChance += requestCard.CriticalChance;
        CriticalDamage += requestCard.CriticalDamage;
        DiceControl += requestCard.DiceControl;
        SpAcquisition += requestCard.SpAcquisition;
        SpUsage += requestCard.SpUsage;
    }

    public void ReleaseCard(Card requestCard)
    {
        NormalAttackPower -= requestCard.NormalAttackPower;
        BossAttackPower -= requestCard.BossAttackPower;
        AttackSpeed -= requestCard.AttackSpeed;
        CriticalChance -= requestCard.CriticalChance;
        CriticalDamage -= requestCard.CriticalDamage;
        DiceControl -= requestCard.DiceControl;
        SpAcquisition -= requestCard.SpAcquisition;
        SpUsage -= requestCard.SpUsage;
    }

    public int Level
    {
        get => level;
        set
        {
            level = value;

            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    public float Experience
    {
        get => experience;
        set
        {
            experience = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    #region Property
    public float NormalAttackPower
    {
        get => normalAttackPower;
        set
        {
            normalAttackPower = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    public float BossAttackPower
    {
        get => bossAttackPower;
        set
        {
            bossAttackPower = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    public float AttackSpeed
    {
        get => attackSpeed;
        set
        {
            attackSpeed = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    public float CriticalChance
    {
        get => criticalChance;
        set
        {
            criticalChance = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    public float CriticalDamage
    {
        get => criticalDamage;
        set
        {
            criticalDamage = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    public float DiceControl
    {
        get => diceControl;
        set
        {
            diceControl = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    public float SpAcquisition
    {
        get => spAcquisition;
        set
        {
            spAcquisition = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    public float SpUsage
    {
        get => spUsage;
        set
        {
            spUsage = value;
            if (onPlayerStatsChanged != null)
            {
                onPlayerStatsChanged(this);
            }
        }
    }
    #endregion
}
