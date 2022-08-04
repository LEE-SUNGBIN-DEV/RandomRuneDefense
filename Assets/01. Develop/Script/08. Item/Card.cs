using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class Card : Item<Card>, IStats
{
    #region Events
    public static event UnityAction<Card> onEquipCard;
    public static event UnityAction<Card> onReleaseCard;
    #endregion

    [SerializeField] private float attackPower;
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

        attackPower = float.Parse(jObject["attackPower"].ToString());
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
    public float AttackPower
    {
        get => attackPower;
        set => attackPower = value;
    }
    public float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }
    public float CriticalChance
    {
        get => criticalChance;
        set => criticalChance = value;
    }
    public float CriticalDamage
    {
        get => criticalDamage;
        set => criticalDamage = value;
    }
    public float DiceControl
    {
        get => diceControl;
        set => diceControl = value;
    }
    public float SpAcquisition
    {
        get => spAcquisition;
        set => spAcquisition = value;
    }
    public float SpUsage
    {
        get => spUsage;
        set => spUsage = value;
    }
    #endregion
}
