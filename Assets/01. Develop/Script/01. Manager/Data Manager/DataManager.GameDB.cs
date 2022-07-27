#define DEBUG_MODE

using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;


public partial class DataManager
{
    #region Event
    public static UnityAction onLoadDatabase;
    #endregion

    [Header("Game Database")]
    [SerializeField] private List<Card> cardDatabase;
    [SerializeField] private Dictionary<string, Card> cardDatabaseDictionary = new Dictionary<string, Card>();

    [SerializeField] private List<Rune> runeDatabase;
    [SerializeField] private Dictionary<string, Rune> runeDatabaseDictionary = new Dictionary<string, Rune>();

    [SerializeField] private List<Enemy> enemyDatabase;
    [SerializeField] private Dictionary <string, Enemy> enemyDatabaseDictionary = new Dictionary<string, Enemy>();

    #region Game Database
    // ! 게임 실행시 게임 DB를 받아오는 작업
    public IEnumerator RequestGameDatabase()
    {
#if DEBUG_MODE
        Debug.Log("게임 데이터를 요청합니다.");
#endif
        UIManager.Instance.ShowNetworkState("게임 데이터를 요청합니다.");
        yield return StartCoroutine(Function.WaitPlayFabAPI(RequestCardDatabase));
        yield return StartCoroutine(Function.WaitPlayFabAPI(RequestRuneDatabase));
    }

    public void RequestCardDatabase()
    {
#if DEBUG_MODE
        Debug.Log("카드 데이터를 요청합니다.");
#endif
        UIManager.Instance.ShowNetworkState("카드 데이터를 요청합니다.");
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = Constant.SERVER_CATALOG_VERSION_CARD
        };

        PlayFabClientAPI.GetCatalogItems(request, (GetCatalogItemsResult result) =>
        {
            cardDatabase.Clear();
            for (int i = 0; i < result.Catalog.Count; ++i)
            {
                string jsonString = result.Catalog[i].CustomData;

                Card newCard = new Card();
                newCard.LoadItem(jsonString);
                cardDatabase.Add(newCard);
                cardDatabaseDictionary.Add(newCard.ItemName, newCard);
            }

#if DEBUG_MODE
            Debug.Log("카드 데이터를 불러왔습니다.");
#endif
            UIManager.Instance.ShowNetworkState("카드 데이터를 불러왔습니다.");
            Function.isAsyncOperationComplete = true;
        }, OnDataRequestError);
    }
    #endregion

    // ! 룬 정보 요청
    public void RequestRuneDatabase()
    {
#if DEBUG_MODE
        Debug.Log("룬 데이터를 요청합니다.");
#endif
        UIManager.Instance.ShowNetworkState("룬 데이터를 요청합니다.");
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = Constant.SERVER_CATALOG_VERSION_RUNE
        };

        PlayFabClientAPI.GetCatalogItems(request, (GetCatalogItemsResult result) =>
        {
            //

#if DEBUG_MODE
            Debug.Log("룬 데이터를 불러왔습니다.");
#endif
            UIManager.Instance.ShowNetworkState("룬 데이터를 불러왔습니다.");
            Function.isAsyncOperationComplete = true;
        }, OnDataRequestError);
    }

    #region Property
    public List<Card> CardDatabase
    {
        get => cardDatabase;
    }
    public Dictionary<string, Card> CardDatabaseDictionary
    {
        get => cardDatabaseDictionary;
    }
    public List<Rune> RuneDatabase
    {
        get => runeDatabase;
    }
    public Dictionary<string, Rune> RuneDatabaseDictionary
    {
        get => runeDatabaseDictionary;
    }
    public List<Enemy> EnemyDatabase
    {
        get => enemyDatabase;
    }
    public Dictionary<string, Enemy> EnemyDatabaseDictionary
    {
        get => enemyDatabaseDictionary;
    }
    #endregion
}
