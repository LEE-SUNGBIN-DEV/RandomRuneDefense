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
    public static UnityAction onLoadDatabase;

    public static UnityAction<PlayerDatabase> onLoadPlayerData;
    public static UnityAction<PlayerDatabase> onSavePlayerData;
    #endregion
    [Header("PlayFab Entity")]
    private string playFabEntityId;
    private string playFabEntityType;

    private void UpdatePlayerDatabase(string key, object value)
    {
        PlayFabCloudScriptAPI.ExecuteFunction(new ExecuteFunctionRequest()
        {
            Entity = new PlayFab.CloudScriptModels.EntityKey()
            {
                Id = playFabEntityId, //Get this from when you logged in,
                Type = playFabEntityType //Get this from when you logged in
            },
            FunctionName = "UpdatePlayerDatabase", //This should be the name of your Azure Function that you created.
            FunctionParameter = new Dictionary<string, object>()
            {
                { "key", key },
                { "value", value }
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

            ConnectionInfomationText.text = "�÷��̾� ������ ���� ���Դϴ�.";
            GetPlayerDatabase();
        }, (PlayFabError error) =>
        {
            Debug.Log($"Opps Something went wrong: {error.GenerateErrorReport()}");
        });
    }

    public void GetPlayerDatabase()
    {
        ConnectionInfomationText.text = "�÷��̾� ������ �ҷ����� ���Դϴ�.";

        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                PlayerDatabase playerSaveData = JsonConvert.DeserializeObject<PlayerDatabase>(result.Objects["PlayerDatabase"].DataObject.ToString());
                onLoadPlayerData(playerSaveData);
            },
            OnPlayFabError);
    }
    //
    #region Request Database
    // ! �÷��̾� ������ ��û
    public void RequestPlayerData()
    {

    }
    // ! ���� �Ǹ� ����Ʈ ��û
    public void RequestShopSellList()
    {

    }
    // ! �÷��̾� �κ��丮 ��û
    public void RequestPlayerInventory()
    {

    }
    // ! ī�� ���� ��û
    public void RequestCardCollections()
    {

    }
    // ! �� ���� ��û
    public void RequestRuneInformation()
    {

    }
    // ! ���� ��û
    public void RequestPurchase()
    {

    }
    #endregion
}
