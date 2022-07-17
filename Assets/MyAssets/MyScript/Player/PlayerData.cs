using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    #region Event
    public static event UnityAction<PlayerData> onPlayerDataChanged;
    #endregion

    [SerializeField] private string nickname;
    [SerializeField] private uint level;
    [SerializeField] private float experience;
    [SerializeField] private uint gold;
    [SerializeField] private uint crystal;

    // 

    private void Awake()
    {
        NetworkManager.onLoadPlayerData -= LoadPlayerData;
        NetworkManager.onLoadPlayerData += LoadPlayerData;

        NetworkManager.onSavePlayerData -= SavePlayerData;
        NetworkManager.onSavePlayerData += SavePlayerData;

        GameManager.onSceneLoaded -= UpdatePlayerData;
        GameManager.onSceneLoaded += UpdatePlayerData;
    }
    private void OnDestroy()
    {
        NetworkManager.onLoadPlayerData -= LoadPlayerData;
        NetworkManager.onSavePlayerData -= SavePlayerData;
        GameManager.onSceneLoaded -= UpdatePlayerData;
    }

    public void LoadPlayerData(PlayerSaveData playerSaveData)
    {
        Nickname = playerSaveData.nickname;
        Level = playerSaveData.level;
        Experience = playerSaveData.experience;
        Gold = playerSaveData.gold;
        Crystal = playerSaveData.crystal;
    }

    public void SavePlayerData(PlayerSaveData playerSaveData)
    {
        playerSaveData.nickname = nickname;
        playerSaveData.level = level;
        playerSaveData.experience = experience;
        playerSaveData.gold = gold;
        playerSaveData.crystal = crystal;
    }

    public void UpdatePlayerData(string sceneName)
    {
        if (sceneName == Constant.NAME_LOBBY_SCENE
            && onPlayerDataChanged != null)
        {
            onPlayerDataChanged(this);
        }
    }

    public string Nickname
    {
        get => nickname;
        set
        {
            nickname = value;
            UpdatePlayerData(GameManager.Instance.CurrentSceneName);
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
