using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    #region Event
    public static event UnityAction<PlayerData> onPlayerDataChanged;
    #endregion

    [SerializeField] private uint level;
    [SerializeField] private float experience;
    [SerializeField] private uint gold;
    [SerializeField] private uint crystal;


    private void Awake()
    {
        DataManager.onLoadPlayerData -= LoadPlayerData;
        DataManager.onLoadPlayerData += LoadPlayerData;

        GameManager.onSceneLoaded -= UpdatePlayerData;
        GameManager.onSceneLoaded += UpdatePlayerData;
    }
    private void OnDestroy()
    {
        DataManager.onLoadPlayerData -= LoadPlayerData;
        GameManager.onSceneLoaded -= UpdatePlayerData;
    }

    public void LoadPlayerData()
    {
        Level = level;
        Experience = experience;
        Gold = gold;
        Crystal = crystal;
    }

    public void UpdatePlayerData(string sceneName)
    {
        if (sceneName == Constant.NAME_LOBBY_SCENE
            && onPlayerDataChanged != null)
        {
            onPlayerDataChanged(this);
        }
    }

    public uint Level
    {
        get => level;
        set
        {
            level = value;
            UpdatePlayerData(GameManager.Instance.CurrentSceneName);
        }
    }
    public float Experience
    {
        get => experience;
        set
        {
            experience = value;
            UpdatePlayerData(GameManager.Instance.CurrentSceneName);
        }
    }
    public uint Gold
    {
        get => gold;
        set
        {
            gold = value;
            UpdatePlayerData(GameManager.Instance.CurrentSceneName);
        }
    }
    public uint Crystal
    {
        get => crystal;
        set
        {
            crystal = value;
            UpdatePlayerData(GameManager.Instance.CurrentSceneName);
        }
    }
}
