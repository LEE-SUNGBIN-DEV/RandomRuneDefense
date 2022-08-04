using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StatPanel : MonoBehaviour
{
    [SerializeField] private StatSlot attackPowerSlot;
    [SerializeField] private StatSlot attackSpeedSlot;
    [SerializeField] private StatSlot criticalChanceSlot;
    [SerializeField] private StatSlot criticalDamageSlot;
    [SerializeField] private StatSlot diceControlSlot;
    [SerializeField] private StatSlot spAcquisitionSlot;
    [SerializeField] private StatSlot spUsageSlot;

    public void SetStatPanel(IStats stats)
    {
        attackPowerSlot.SetStatSlot(stats.AttackPower);
        attackSpeedSlot.SetStatSlot(stats.AttackSpeed);
        criticalChanceSlot.SetStatSlot(stats.CriticalChance);
        criticalDamageSlot.SetStatSlot(stats.CriticalDamage);
        diceControlSlot.SetStatSlot(stats.DiceControl);
        spAcquisitionSlot.SetStatSlot(stats.SpAcquisition);
        spUsageSlot.SetStatSlot(stats.SpUsage);
    }
}
