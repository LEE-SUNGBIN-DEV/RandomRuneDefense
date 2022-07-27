#define DEBUG_MODE

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
    #region Request Player Data
    public IEnumerator RequestPlayerDatabase()
    {
#if DEBUG_MODE
        Debug.Log("플레이어 데이터를 요청합니다.");
#endif
        UIManager.Instance.ShowNetworkState("플레이어 데이터를 요청합니다.");
        yield return StartCoroutine(Function.WaitPlayFabAPI(GetPlayerStats));
        yield return StartCoroutine(Function.WaitPlayFabAPI(GetPlayerCurrency));
        yield return StartCoroutine(Function.WaitPlayFabAPI(GetPlayerInventory));
        yield return StartCoroutine(Function.WaitPlayFabAPI(GetPlayerCollections));
    }
    public void GetPlayerStats()
    {
#if DEBUG_MODE
        Debug.Log("플레이어 스탯을 요청합니다.");
#endif
        UIManager.Instance.ShowNetworkState("플레이어 스탯을 요청합니다.");
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_STATS].DataObject.ToString(), PlayerData.PlayerStats);
                onLoadPlayerData();
#if DEBUG_MODE
                Debug.Log("플레이어 스탯을 불러왔습니다.");
#endif
                UIManager.Instance.ShowNetworkState("플레이어 스탯을 불러왔습니다.");
                Function.isAsyncOperationComplete = true;
            },
            OnDataRequestError);
    }
    public void GetPlayerCurrency()
    {
#if DEBUG_MODE
        Debug.Log("플레이어 화폐를 요청합니다.");
#endif
        UIManager.Instance.ShowNetworkState("플레이어 화폐를 요청합니다.");
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_CURRENCY].DataObject.ToString(), PlayerData.PlayerCurrency);
                onLoadPlayerData();
#if DEBUG_MODE
                Debug.Log("플레이어 화폐를 불러왔습니다.");
#endif
                UIManager.Instance.ShowNetworkState("플레이어 화폐를 불러왔습니다.");
                Function.isAsyncOperationComplete = true;
            },
            OnDataRequestError);
    }
    public void GetPlayerInventory()
    {
#if DEBUG_MODE
        Debug.Log("플레이어 인벤토리를 요청합니다.");
#endif
        UIManager.Instance.ShowNetworkState("플레이어 인벤토리를 요청합니다.");
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                PlayerData.PlayerInventory.ClearPlayerInventory();
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_INVENTORY].DataObject.ToString(), PlayerData.PlayerInventory);
                onLoadPlayerData();
#if DEBUG_MODE
                Debug.Log("플레이어 인벤토리를 불러왔습니다.");
#endif
                UIManager.Instance.ShowNetworkState("플레이어 인벤토리를 불러왔습니다.");
                Function.isAsyncOperationComplete = true;
            },
            OnDataRequestError);
    }
    public void GetPlayerCollections()
    {
#if DEBUG_MODE
        Debug.Log("플레이어 도감을 요청합니다.");
#endif
        UIManager.Instance.ShowNetworkState("플레이어 도감을 요청합니다.");
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_COLLECTIONS].DataObject.ToString(), PlayerData.PlayerCollections);
                onLoadPlayerData();
#if DEBUG_MODE
                Debug.Log("플레이어 도감을 불러왔습니다.");
#endif
                UIManager.Instance.ShowNetworkState("플레이어 도감을 불러왔습니다.");
                Function.isAsyncOperationComplete = true;
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

    #region PlayFab Inventory and Currency (Legacy)
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
