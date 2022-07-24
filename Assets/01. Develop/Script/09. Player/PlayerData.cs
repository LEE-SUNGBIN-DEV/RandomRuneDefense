using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerCurrency playerCurrency;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerCollections playerCollections;

    private void Awake()
    {
        DataManager.onLoadPlayerData -= OnLoadPlayerData;
        DataManager.onLoadPlayerData += OnLoadPlayerData;
    }

    private void OnDestroy()
    {
        DataManager.onLoadPlayerData -= OnLoadPlayerData;
    }

    public void OnLoadPlayerData()
    {
        playerStats.OnLoadPlayerStats();
        playerCurrency.OnLoadPlayerCurrency();
        playerInventory.OnLoadPlayerInventory();
        playerCollections.OnLoadPlayerCollections();
    }

    #region Property
    public PlayerStats PlayerStats
    {
        get => playerStats;
    }
    public PlayerCurrency PlayerCurrency
    {
        get => playerCurrency;
    }
    public PlayerInventory PlayerInventory
    {
        get => playerInventory;
    }
    public PlayerCollections PlayerCollections
    {
        get => playerCollections;
    }
    #endregion
}
