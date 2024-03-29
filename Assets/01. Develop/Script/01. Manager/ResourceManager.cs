using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private string localDataPath;

    [Header("Prefab")]
    [SerializeField] private GameObject[] prefabs;

    [Header("Item Resource")]
    [SerializeField] private ItemResource[] cardResources;
    [SerializeField] private ItemResource[] shopResources;
    [SerializeField] private ItemResource[] runeResources;

    [Header("Resource Dictionary")]
    [SerializeField] private Dictionary<string, GameObject> prefabDictionary;
    [SerializeField] private Dictionary<string, ItemResource> cardResourceDictionary;
    [SerializeField] private Dictionary<string, ItemResource> shopResourceDictionary;
    [SerializeField] private Dictionary<string, ItemResource> runeResourceDictionary;

    public override void Initialize()
    {
        localDataPath = Application.dataPath + "/Resources/";

        cardResourceDictionary = new Dictionary<string, ItemResource>();
        shopResourceDictionary = new Dictionary<string, ItemResource>();
        runeResourceDictionary = new Dictionary<string, ItemResource>();

        for(int i=0; i<prefabs.Length; ++i)
        {
            prefabDictionary.Add(prefabs[i].name, prefabs[i]);
        }

        for (int i=0; i<cardResources.Length; ++i)
        {
            cardResourceDictionary.Add(cardResources[i].itemName, cardResources[i]);
        }

        for (int i = 0; i < shopResources.Length; ++i)
        {
            shopResourceDictionary.Add(shopResources[i].itemName, shopResources[i]);
        }

        for (int i = 0; i < runeResources.Length; ++i)
        {
            runeResourceDictionary.Add(runeResources[i].itemName, runeResources[i]);
        }
    }

    #region Property
    public Dictionary<string, GameObject> PrefabDictionary
    {
        get => prefabDictionary;
    }
    public Dictionary<string, ItemResource> CardResourceDictionary
    {
        get => cardResourceDictionary;
    }
    public Dictionary<string, ItemResource> ShopResourceDictionary
    {
        get => shopResourceDictionary;
    }
    public Dictionary<string, ItemResource> RuneResourceDictionary
    {
        get => runeResourceDictionary;
    }
    #endregion
}
