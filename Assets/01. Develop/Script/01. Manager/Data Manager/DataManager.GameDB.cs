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
    public void RequestAllDatabase()
    {
        UIManager.Instance.ShowNetworkState("게임 데이터를 요청합니다.");
        RequestCardDatabase();
    }
    public void RequestCardDatabase()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = Constant.SERVER_CATALOG_VERSION_CARD
        };

        PlayFabClientAPI.GetCatalogItems(request, UpdateCardDatabase, OnDataRequestError);
    }
    public void UpdateCardDatabase(GetCatalogItemsResult result)
    {
        UIManager.Instance.ShowNetworkState("카드 데이터를 불러오고 있습니다.");

        for (int i = 0; i < result.Catalog.Count; ++i)
        {
            Card newCard = new Card();
            newCard.LoadItem(result.Catalog[i].CustomData);
            cardDatabase.Add(newCard);
            cardDatabaseDictionary.Add(newCard.ItemName, newCard);
        }

        if (onLoadDatabase != null)
        {
            onLoadDatabase();
        }
    }
    #endregion


    // ! 룬 정보 요청
    public void RequestRuneInformation()
    {

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
