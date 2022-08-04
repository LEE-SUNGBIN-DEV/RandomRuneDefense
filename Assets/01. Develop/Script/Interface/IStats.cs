using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    public float AttackPower { get; set; }
    public float AttackSpeed { get; set; }
    public float CriticalChance { get; set; }
    public float CriticalDamage { get; set; }
    public float DiceControl { get; set; }
    public float SpAcquisition { get; set; }
    public float SpUsage { get; set; }
}
