using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayFab;
using PlayFab.DataModels;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using Newtonsoft.Json;

public partial class DataManager
{
    #region Event
    public static event UnityAction onLoadPlayerData;
    #endregion

    [Header("Player Database")]
    [SerializeField] private PlayerData playerData;

    #region Player DB
    #region Get Player Data
    public void GetPlayerData()
    {
        UIManager.Instance.ShowNetworkState("플레이어 정보를 불러오는 중입니다.");

        GetPlayerStats();
        GetPlayerCurrency();
        GetPlayerInventory();
        GetPlayerCollections();
    }
    public void GetPlayerStats()
    {
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_STATS].DataObject.ToString(), PlayerData.PlayerStats);
                onLoadPlayerData();
            },
            OnDataRequestError);
    }
    public void GetPlayerCurrency()
    {
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_CURRENCY].DataObject.ToString(), PlayerData.PlayerCurrency);
                onLoadPlayerData();
            },
            OnDataRequestError);
    }
    public void GetPlayerInventory()
    {
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                PlayerData.PlayerInventory.ClearPlayerInventory();
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_INVENTORY].DataObject.ToString(), PlayerData.PlayerInventory);
                onLoadPlayerData();
            },
            OnDataRequestError);
    }
    public void GetPlayerCollections()
    {
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_COLLECTIONS].DataObject.ToString(), PlayerData.PlayerCollections);
                onLoadPlayerData();
            },
            OnDataRequestError);
    }
    #endregion

    #region Set Player Data
    public void SetPlayerData()
    {
        var playerStats = JsonConvert.SerializeObject(PlayerData.PlayerStats);
        var playerCurrency = JsonConvert.SerializeObject(PlayerData.PlayerCurrency);
        var playerInventory = JsonConvert.SerializeObject(PlayerData.PlayerInventory);
        var playerCollections = JsonConvert.SerializeObject(PlayerData.PlayerCollections);

        var dataList = new List<SetObject>()
        {
            new SetObject()
            {
                ObjectName = Constant.SERVER_NAME_FILE_PLAYER_STATS,
                DataObject = playerStats
            },
            new SetObject()
            {
                ObjectName = Constant.SERVER_NAME_FILE_PLAYER_CURRENCY,
                DataObject = playerCurrency
            },
            new SetObject()
            {
                ObjectName = Constant.SERVER_NAME_FILE_PLAYER_INVENTORY,
                DataObject = playerInventory
            },
            new SetObject()
            {
                ObjectName = Constant.SERVER_NAME_FILE_PLAYER_COLLECTIONS,
                DataObject = playerCollections
            }
        };

        var setRequest = new SetObjectsRequest
        {
            Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType },
            Objects = dataList
        };
        PlayFabDataAPI.SetObjects(setRequest,
            result =>
            {
                Debug.Log("저장 성공");
            },
            OnDataRequestError);
    }
    #endregion
    #endregion

    #region PlayFab Inventory and Currency
    /*
    public void GetPlayerCurrency()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
            result =>
            {
                PlayerData.PlayerCurrency.Gold = result.VirtualCurrency["GD"];
                PlayerData.PlayerCurrency.Crystal = result.VirtualCurrency["CR"];
                onLoadPlayerData();
            },
            OnDataRequestError);
    }
    public void GetPlayerInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
            result =>
            {
                for(int i=0; i<result.Inventory.Count; ++i)
                {
                    if (result.Inventory[i].ItemClass == Constant.SERVER_NAME_CATALOG_CLASS_CARD)
                    {
                        Debug.Log(result.Inventory[i].CustomData);
                        Debug.Log(JsonConvert.SerializeObject(result.Inventory[i].CustomData));
                        Card newCard = new Card();
                        newCard.LoadItem(JsonConvert.SerializeObject(result.Inventory[i].CustomData));
                        PlayerData.Playerinventory.CardList.Add(newCard);
                    }
                }
                onLoadPlayerData();
            },
            OnDataRequestError);
    }
    public void AddPlayerCurrency(string currency, int amount)
    {
        var request = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = currency,
            Amount = amount
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request,
            result =>
            {
                PlayerData.PlayerCurrency.Gold = result.Balance;
                onLoadPlayerData();
            },
            OnDataRequestError);
    }

    public void SubstractPlayerCurrency(string currency, int amount)
    {
        var request = new SubtractUserVirtualCurrencyRequest()
        {
            VirtualCurrency = currency,
            Amount = amount
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request,
            result =>
            {
                PlayerData.PlayerCurrency.Gold = result.Balance;
                onLoadPlayerData();
            },
            OnDataRequestError);
    }
    */
    #endregion

    #region Property
    public PlayerData PlayerData
    {
        get => playerData;
        set => playerData = value;
    }
    #endregion
}
