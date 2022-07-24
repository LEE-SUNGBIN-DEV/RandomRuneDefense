using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerCurrency
{
    #region Event
    public static event UnityAction<PlayerCurrency> onPlayerCurrencyChanged;
    #endregion

    [SerializeField] private int gold;
    [SerializeField] private int crystal;

    public void OnLoadPlayerCurrency()
    {
        Gold = gold;
        Crystal = crystal;
    }

    public int Gold
    {
        get => gold;
        set
        {
            gold = value;
            if (onPlayerCurrencyChanged != null)
            {
                onPlayerCurrencyChanged(this);
            }
        }
    }
    public int Crystal
    {
        get => crystal;
        set
        {
            crystal = value;
            if (onPlayerCurrencyChanged != null)
            {
                onPlayerCurrencyChanged(this);
            }
        }
    }
}
