using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayFab;
using PlayFab.DataModels;
using PlayFab.CloudScriptModels;
using Newtonsoft.Json;

public partial class NetworkManager
{
    #region Event
    public static UnityAction<PlayerSaveData> onLoadPlayerData;
    public static UnityAction<PlayerSaveData> onSavePlayerData;
    #endregion
    [Header("PlayFab Entity")]
    private string playFabEntityId;
    private string playFabEntityType;

    private void GetUserData()
    {
        PlayFabCloudScriptAPI.ExecuteFunction(new ExecuteFunctionRequest()
        {
            Entity = new PlayFab.CloudScriptModels.EntityKey()
            {
                Id = playFabEntityId, //Get this from when you logged in,
                Type = playFabEntityType //Get this from when you logged in
            },
            FunctionName = "GetUserData", //This should be the name of your Azure Function that you created.
            FunctionParameter = new Dictionary<string, string>()
            {
                {"key", "gold" }
            },
            GeneratePlayStreamEvent = true
        }, (ExecuteFunctionResult result) =>
        {
            if (result.FunctionResultTooLarge != null && (bool)result.FunctionResultTooLarge)
            {
                Debug.Log("This can happen if you exceed the limit that can be returned from an Azure Function," +
                    " See PlayFab Limits Page for details.");
                return;
            }
            Debug.Log($"The {result.FunctionName} function took {result.ExecutionTimeMilliseconds} to complete");
            Debug.Log($"Result: {result.FunctionResult}");
        }, (PlayFabError error) =>
        {
            Debug.Log($"Opps Something went wrong: {error.GenerateErrorReport()}");
        });
    }

    private void UpdateUserData()
    {
        PlayFabCloudScriptAPI.ExecuteFunction(new ExecuteFunctionRequest()
        {
            Entity = new PlayFab.CloudScriptModels.EntityKey()
            {
                Id = playFabEntityId, //Get this from when you logged in,
                Type = playFabEntityType //Get this from when you logged in
            },
            FunctionName = "UpdateUserData", //This should be the name of your Azure Function that you created.
            FunctionParameter = new Dictionary<string, string>()
            {
                {"key", "gold" },
                {"value", "300" }
            },
            GeneratePlayStreamEvent = false
        }, (ExecuteFunctionResult result) =>
        {
            if (result.FunctionResultTooLarge != null && (bool)result.FunctionResultTooLarge)
            {
                Debug.Log("This can happen if you exceed the limit that can be returned from an Azure Function," +
                    " See PlayFab Limits Page for details.");
                return;
            }
            Debug.Log($"The {result.FunctionName} function took {result.ExecutionTimeMilliseconds} to complete");
            Debug.Log($"Result: {result.FunctionResult}");
        }, (PlayFabError error) =>
        {
            Debug.Log($"Opps Something went wrong: {error.GenerateErrorReport()}");
        });
    }
    #region Request Database
    // ! 플레이어 데이터 요청
    public void RequestPlayerData()
    {

    }
    // ! 상점 판매 리스트 요청
    public void RequestShopSellList()
    {

    }
    // ! 플레이어 인벤토리 요청
    public void RequestPlayerInventory()
    {

    }
    // ! 카드 도감 요청
    public void RequestCardCollections()
    {

    }
    // ! 룬 정보 요청
    public void RequestRuneInformation()
    {

    }
    // ! 구매 요청
    public void RequestPurchase()
    {

    }

    public void SavePlayerData()
    {
        PlayerSaveData playerSaveData = new PlayerSaveData();
        onSavePlayerData(playerSaveData);

        var data = new Dictionary<string, object>()
        {
            {"name", playerSaveData.nickname},
            {"level", playerSaveData.level},
            {"experience", playerSaveData.experience},
            {"gold", playerSaveData.gold},
            {"crystal", playerSaveData.crystal}
        };

        var dataList = new List<SetObject>()
        {
            new SetObject()
            {
              ObjectName = "PlayerSaveData",
              DataObject = data
            },
        // A free-tier customer may store up to 3 objects on each entity
        };

        PlayFabDataAPI.SetObjects(new SetObjectsRequest()
        {
            Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType }, // Saved from GetEntityToken, or a specified key created from a titlePlayerId, CharacterId, etc
            Objects = dataList,
        }, (setResult) =>
        {
            Debug.Log(setResult.ProfileVersion);
        }, OnPlayFabError);
    }
    public void LoadPlayerData()
    {
        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                PlayerSaveData playerSaveData = JsonConvert.DeserializeObject<PlayerSaveData>(result.Objects["PlayerSaveData"].DataObject.ToString());
                onLoadPlayerData(playerSaveData);

            },
            OnPlayFabError);
    }
    #endregion
}
