using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class Card : Item<Card>
{
    #region Events
    public static event UnityAction<Card> onEquipCard;
    public static event UnityAction<Card> onReleaseCard;
    #endregion

    [SerializeField] private float normalAttackPower;
    [SerializeField] private float bossAttackPower;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalDamage;
    [SerializeField] private float diceControl;
    [SerializeField] private float spAcquisition;
    [SerializeField] private float spUsage;

    public override void LoadItem(string jsonString)
    {
        base.LoadItem(jsonString);
        JObject jObject = JObject.Parse(jsonString);

        normalAttackPower = float.Parse(jObject["normalAttackPower"].ToString());
        bossAttackPower = float.Parse(jObject["bossAttackPower"].ToString());
        attackSpeed = float.Parse(jObject["attackSpeed"].ToString());
        criticalChance = float.Parse(jObject["criticalChance"].ToString());
        criticalDamage = float.Parse(jObject["criticalDamage"].ToString());
        diceControl = float.Parse(jObject["diceControl"].ToString());
        spAcquisition = float.Parse(jObject["spAcquisition"].ToString());
        spUsage = float.Parse(jObject["spUsage"].ToString());
    }

    public void EquipCard()
    {
        onEquipCard(this);
    }

    public void ReleaseCard()
    {
        onReleaseCard(this);
    }

    #region Property
    public float NormalAttackPower
    {
        get => normalAttackPower;
    }
    public float BossAttackPower
    {
        get => bossAttackPower;
    }
    public float AttackSpeed
    {
        get => attackSpeed;
    }
    public float CriticalChance
    {
        get => criticalChance;
    }
    public float CriticalDamage
    {
        get => criticalDamage;
    }
    public float DiceControl
    {
        get => diceControl;
    }
    public float SpAcquisition
    {
        get => spAcquisition;
    }
    public float SpUsage
    {
        get => spUsage;
    }
    #endregion
}
