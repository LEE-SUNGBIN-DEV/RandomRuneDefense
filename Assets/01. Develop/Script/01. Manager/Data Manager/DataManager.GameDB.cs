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
    // ! ���� ����� ���� DB�� �޾ƿ��� �۾�
    public IEnumerator RequestGameDatabase()
    {
#if DEBUG_MODE
        Debug.Log("���� �����͸� ��û�մϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("���� �����͸� ��û�մϴ�.");
        yield return StartCoroutine(Function.WaitPlayFabAPI(RequestCardDatabase));
        yield return StartCoroutine(Function.WaitPlayFabAPI(RequestRuneDatabase));
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
            Debug.Log("ī�� �����͸� �ҷ��Խ��ϴ�.");
#endif
            UIManager.Instance.ShowNetworkState("ī�� �����͸� �ҷ��Խ��ϴ�.");
            Function.isAsyncOperationComplete = true;
        }, OnDataRequestError);
    }
    #endregion

    // ! �� ���� ��û
    public void RequestRuneDatabase()
    {
#if DEBUG_MODE
        Debug.Log("�� �����͸� ��û�մϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("�� �����͸� ��û�մϴ�.");
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = Constant.SERVER_CATALOG_VERSION_RUNE
        };

        PlayFabClientAPI.GetCatalogItems(request, (GetCatalogItemsResult result) =>
        {
            //

#if DEBUG_MODE
            Debug.Log("�� �����͸� �ҷ��Խ��ϴ�.");
#endif
            UIManager.Instance.ShowNetworkState("�� �����͸� �ҷ��Խ��ϴ�.");
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
