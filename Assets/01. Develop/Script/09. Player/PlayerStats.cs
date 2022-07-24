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

    public void OnLoadPlayerStats()
    {
        Level = level;
        Experience = experience;
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
}
