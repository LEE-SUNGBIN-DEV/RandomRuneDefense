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
using Newtonsoft.Json.Linq;

public class DataManager : Singleton<DataManager>
{
    #region Event
    public static UnityAction onLoadCardDatabase;
    public static UnityAction onLoadPlayerData;
    #endregion

    private string localDataPath;

    [Header("PlayFab Entity")]
    private string playFabEntityId;
    private string playFabEntityType;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

    [Header("Database")]
    [SerializeField] private Card card;
    [SerializeField] private List<Card> cardDatabase;
    [SerializeField] private List<Enemy> enemyDatabase;
    [SerializeField] private List<CatalogItem> shopGoldItemDatabase;

    public override void Initialize()
    {
        localDataPath = Application.dataPath + "/Resources/";
    }

    private void Start()
    {
    }

    public void SetPlayFabEntity(LoginResult loginResult)
    {
        PlayFabEntityId = loginResult.EntityToken.Entity.Id;
        PlayFabEntityType = loginResult.EntityToken.Entity.Type;
    }

    #region Request Database
    // ! Request Player DB
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

    public void RequestPlayerData()
    {
        UIManager.Instance.ShowNetworkState("플레이어 정보를 불러오는 중입니다.");

        var getRequest = new GetObjectsRequest { Entity = new PlayFab.DataModels.EntityKey { Id = playFabEntityId, Type = playFabEntityType } };
        PlayFabDataAPI.GetObjects(getRequest,
            result =>
            {
                JsonConvert.PopulateObject(result.Objects[Constant.SERVER_NAME_FILE_PLAYER_DATA].DataObject.ToString(), PlayerData);
                onLoadPlayerData();
                UIManager.Instance.ShowNetworkState("플레이어 정보를 불러왔습니다.");
            },
            OnDataRequestError);
    }
    // ! Request Card DB
    public void RequestCardDatabase()
    {
        UIManager.Instance.ShowNetworkState("카드 정보를 불러오고 있습니다.");
        PlayFabCloudScriptAPI.ExecuteFunction(new ExecuteFunctionRequest()
        {
            Entity = new PlayFab.CloudScriptModels.EntityKey()
            {
                Id = "FA99D",
                Type = "title"
            },
            FunctionName = Constant.SERVER_NAME_FUNCTION_GET_CARD_DATABASE, //This should be the name of your Azure Function that you created.
            FunctionParameter = null,
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

            GetObjectsResponse getObjectsResponse = (result.FunctionResult) as GetObjectsResponse;
            Debug.Log(getObjectsResponse.ToString());
            card = JsonConvert.DeserializeObject<Card>(getObjectsResponse.Objects[Constant.SERVER_NAME_FILE_CARD_DATABASE].DataObject.ToString());
            onLoadPlayerData();

            UIManager.Instance.ShowNetworkState("카드 정보를 불러왔습니다.");
        }, (PlayFabError error) =>
        {
            Debug.Log($"ExecuteFunction Error: {error.GenerateErrorReport()}");
        });
    }
    
    // ! 상점 판매 리스트 요청
    public void RequestShopSellList()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = Constant.SERVER_CATALOG_VERSION
        };

        PlayFabClientAPI.GetCatalogItems(request, UpdateShopSellList, OnDataRequestError);
    }
    public void UpdateShopSellList(GetCatalogItemsResult result)
    {
        for (int i = 0; i < result.Catalog.Count; ++i)
        {
            if (result.Catalog[i].ItemClass == Constant.SERVER_NAME_ITEM_CLASS_CARD)
            {
                ShopGoldItemDatabase.Add(result.Catalog[i]);
            }
        }

        if (onLoadCardDatabase != null)
        {
            onLoadCardDatabase();
        }
    }
    // ! 플레이어 인벤토리 요청
    public void RequestPlayerInventory()
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
    #endregion

    private void OnDataRequestError(PlayFabError obj)
    {
        Debug.Log(obj.GenerateErrorReport());
    }

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
    public PlayerData PlayerData
    {
        get => playerData;
        set => playerData = value;
    }
    public List<Card> CardDatabase
    {
        get => cardDatabase;
        private set
        {
            cardDatabase = value;
        }
    }
    public List<Enemy> EnemyDatabase
    {
        get => enemyDatabase;
        private set => enemyDatabase = value;
    }
    public List<CatalogItem> ShopGoldItemDatabase
    {
        get => shopGoldItemDatabase;
        private set => shopGoldItemDatabase = value;
    }
    #endregion
}
