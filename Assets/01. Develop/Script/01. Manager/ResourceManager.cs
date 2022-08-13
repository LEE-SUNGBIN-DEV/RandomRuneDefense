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

    [Header("Resource Dictionary")]
    [SerializeField] private Dictionary<string, GameObject> prefabDictionary;
    [SerializeField] private Dictionary<string, ItemResource> cardResourceDictionary;
    [SerializeField] private Dictionary<string, ItemResource> shopResourceDictionary;

    public override void Initialize()
    {
        localDataPath = Application.dataPath + "/Resources/";

        cardResourceDictionary = new Dictionary<string, ItemResource>();
        shopResourceDictionary = new Dictionary<string, ItemResource>();

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
    #endregion
}
