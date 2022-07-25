#define DEBUG_MODE

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayFab;
using PlayFab.ClientModels;

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
#if DEBUG_MODE
        Debug.Log("���� �����͸� ��û�մϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("���� �����͸� ��û�մϴ�.");
        RequestCardDatabase();
        RequestRuneDatabase();
        GetPlayerData();
    }
    public void RequestCardDatabase()
    {
#if DEBUG_MODE
        Debug.Log("ī�� �����͸� ��û�մϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("ī�� �����͸� ��û�մϴ�.");
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = Constant.SERVER_CATALOG_VERSION_CARD
        };

        PlayFabClientAPI.GetCatalogItems(request, UpdateCardDatabase, OnDataRequestError);
    }
    public void UpdateCardDatabase(GetCatalogItemsResult result)
    {
        for (int i = 0; i < result.Catalog.Count; ++i)
        {
            string jsonString = result.Catalog[i].CustomData;

            Card newCard = new Card();
            newCard.LoadItem(jsonString);
            cardDatabase.Add(newCard);
            cardDatabaseDictionary.Add(newCard.ItemName, newCard);
        }

#if DEBUG_MODE
        Debug.Log("ī�� �����͸� �ҷ��Խ��ϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("ī�� �����͸� �ҷ��Խ��ϴ�.");
    }
    #endregion


    // ! �� ���� ��û
    public void RequestRuneDatabase()
    {
#if DEBUG_MODE
        Debug.Log("�� �����͸� ��û�մϴ�.");
#endif
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
