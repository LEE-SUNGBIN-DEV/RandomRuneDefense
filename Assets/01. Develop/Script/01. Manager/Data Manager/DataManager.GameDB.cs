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
using Newtonsoft.Json.Linq;


public partial class DataManager
{
    #region Event
    public static event UnityAction onLoadCardDatabase;
    public static event UnityAction onLoadShopDatabase;
    public static event UnityAction onLoadRuneDatabase;
    #endregion

    [Header("Card Database")]
    [SerializeField] private List<Card> cardDatabase;
    [SerializeField] private Dictionary<string, Card> cardDatabaseDictionary = new Dictionary<string, Card>();

    [Header("Shop Database")]
    [SerializeField] private List<ShopItem> goldShopDatabase;
    [SerializeField] private Dictionary<string, ShopItem> goldShopDatabaseDictionary = new Dictionary<string, ShopItem>();

    [SerializeField] private List<ShopItem> crystalShopDatabase;
    [SerializeField] private Dictionary<string, ShopItem> crystalShopDatabaseDictionary = new Dictionary<string, ShopItem>();

    [Header("Rune Database")]
    [SerializeField] private List<RuneData> runeDatabase;
    [SerializeField] private Dictionary<string, RuneData> runeDatabaseDictionary = new Dictionary<string, RuneData>();

    [Header("Enemy Database")]
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
        yield return Function.WaitPlayFabAPI(RequestCardDatabase);
        yield return Function.WaitPlayFabAPI(RequestShopDatabase);
        yield return Function.WaitPlayFabAPI(RequestRuneDatabase);
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
            onLoadCardDatabase?.Invoke();
            UIManager.Instance.ShowNetworkState("ī�� �����͸� �ҷ��Խ��ϴ�.");
            Function.isAsyncOperationComplete = true;
        }, OnDataRequestError);
    }
    #endregion

    // ! ���� ���� ��û
    public void RequestShopDatabase()
    {
#if DEBUG_MODE
        Debug.Log("���� �����͸� ��û�մϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("���� �����͸� ��û�մϴ�.");
        GetCatalogItemsRequest request = new GetCatalogItemsRequest()
        {
            CatalogVersion = Constant.SERVER_CATALOG_VERSION_SHOP
        };

        PlayFabClientAPI.GetCatalogItems(request, (GetCatalogItemsResult result) =>
        {
            goldShopDatabase.Clear();
            crystalShopDatabase.Clear();

            for (int i = 0; i < result.Catalog.Count; ++i)
            {
                string jsonString = result.Catalog[i].CustomData;
                ShopItem newShopItem;

                // ������ ����
                if(result.Catalog[i].ItemClass == "GoldBox")
                {
                    GoldBox goldBox = new GoldBox();
                    goldBox.LoadItem(jsonString);
                    newShopItem = goldBox;
                }
                else if(result.Catalog[i].ItemClass == "TicketBox")
                {
                    TicketBox ticketBox = new TicketBox();
                    ticketBox.LoadItem(jsonString);
                    newShopItem = ticketBox;
                }
                else if (result.Catalog[i].ItemClass == "CardBox")
                {
                    CardBox cardBox = new CardBox();
                    cardBox.LoadItem(jsonString);
                    newShopItem = cardBox;
                }
                else
                {
                    ShopItem shopItem = new ShopItem();
                    shopItem.LoadItem(jsonString);
                    newShopItem = shopItem;
                }

                // ȭ�� Ÿ��
                if (newShopItem.CurrencyType == "gold")
                {
                    goldShopDatabase.Add(newShopItem);
                    goldShopDatabaseDictionary.Add(newShopItem.ItemName, newShopItem);
                }
                else if (newShopItem.CurrencyType == "crystal")
                {
                    crystalShopDatabase.Add(newShopItem);
                    crystalShopDatabaseDictionary.Add(newShopItem.ItemName, newShopItem);
                }
                else
                {
                    Debug.Log("�ùٸ��� ���� ���� Ÿ��");
                }
            }

#if DEBUG_MODE
            Debug.Log("���� �����͸� �ҷ��Խ��ϴ�.");
#endif
            onLoadShopDatabase?.Invoke();
            UIManager.Instance.ShowNetworkState("���� �����͸� �ҷ��Խ��ϴ�.");
            Function.isAsyncOperationComplete = true;
        }, OnDataRequestError);
    }

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
            runeDatabase.Clear();

            for (int i = 0; i < result.Catalog.Count; ++i)
            {
                string jsonString = result.Catalog[i].CustomData;
                RuneData newRune = new RuneData();
                newRune.LoadItem(result.Catalog[i].DisplayName, jsonString);

                runeDatabase.Add(newRune);
                runeDatabaseDictionary.Add(result.Catalog[i].ItemId, newRune);
            }

#if DEBUG_MODE
            Debug.Log("�� �����͸� �ҷ��Խ��ϴ�.");
#endif
            onLoadRuneDatabase?.Invoke();
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
    public List<ShopItem> GoldShopDatabase
    {
        get => goldShopDatabase;
    }
    public Dictionary<string, ShopItem> GoldShopDatabaseDictionary
    {
        get => goldShopDatabaseDictionary;
    }
    public List<ShopItem> CrystalShopDatabase
    {
        get => crystalShopDatabase;
    }
    public Dictionary<string, ShopItem> CrystalShopDatabaseDictionary
    {
        get => crystalShopDatabaseDictionary;
    }
    public List<RuneData> RuneDatabase
    {
        get => runeDatabase;
    }
    public Dictionary<string, RuneData> RuneDatabaseDictionary
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
