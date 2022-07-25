using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;

public partial class DataManager : Singleton<DataManager>
{
    [Header("PlayFab Entity")]
    private string playFabEntityId;
    private string playFabEntityType;


    public override void Initialize()
    {
        cardDatabase = new List<Card>();
        enemyDatabase = new List<Enemy>();
    }

    public void SetPlayFabEntity(LoginResult loginResult)
    {
        PlayFabEntityId = loginResult.EntityToken.Entity.Id;
        PlayFabEntityType = loginResult.EntityToken.Entity.Type;
    }

    public void OnDataRequestError(PlayFabError obj)
    {
        Debug.Log(obj.GenerateErrorReport());
    }

    #region Azure Function
    public void GrantItemsToUsers(string requestItemName)
    {
        PlayFabCloudScriptAPI.ExecuteFunction(new ExecuteFunctionRequest()
        {
            Entity = new PlayFab.CloudScriptModels.EntityKey()
            {
                Id = playFabEntityId, //Get this from when you logged in,
                Type = playFabEntityType //Get this from when you logged in
            },
            FunctionName = Constant.SERVER_NAME_FUNCTION_GRANT_ITEMS_TO_USERS, //This should be the name of your Azure Function that you created.
            FunctionParameter = new Dictionary<string, object>
            {
                {"key", requestItemName }
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
            Debug.Log($"ExecuteFunction Error: {error.GenerateErrorReport()}");
        });
    }
    /*
    private void UpdatePlayerData(string requestKey, object requestValue)
    {
        PlayFabCloudScriptAPI.ExecuteFunction(new ExecuteFunctionRequest()
        {
            Entity = new PlayFab.CloudScriptModels.EntityKey()
            {
                Id = playFabEntityId, //Get this from when you logged in,
                Type = playFabEntityType //Get this from when you logged in
            },
            FunctionName = Constant.SERVER_NAME_FUNCTION_UPDATE_PLAYER_DATA, //This should be the name of your Azure Function that you created.
            FunctionParameter = new Dictionary<string, object>
            {
                {"key", requestKey },
                {"value", requestValue}
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

            UIManager.Instance.ShowNetworkState("플레이어 정보를 갱신 중입니다.");
            RequestPlayerData();
        }, (PlayFabError error) =>
        {
            Debug.Log($"ExecuteFunction Error: {error.GenerateErrorReport()}");
        });
    }
     */
    #endregion

    #region Property
    public string PlayFabEntityId
    {
        get => playFabEntityId;
        private set => playFabEntityId = value;
    }
    public string PlayFabEntityType
    {
        get => playFabEntityType;
        private set => playFabEntityType = value;
    }
    #endregion
}
