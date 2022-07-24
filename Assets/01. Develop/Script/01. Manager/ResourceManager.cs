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
    [SerializeField] private ItemResource[] boxResources;

    [Header("Resource Dictionary")]
    [SerializeField] private Dictionary<string, GameObject> prefabDictionary;
    [SerializeField] private Dictionary<string, ItemResource> cardResourceDictionary;
    [SerializeField] private Dictionary<string, ItemResource> boxResourceDictionary;

    public override void Initialize()
    {
        localDataPath = Application.dataPath + "/Resources/";

        cardResourceDictionary = new Dictionary<string, ItemResource>();
        boxResourceDictionary = new Dictionary<string, ItemResource>();

        for(int i=0; i<prefabs.Length; ++i)
        {
            prefabDictionary.Add(prefabs[i].name, prefabs[i]);
        }

        for (int i=0; i<cardResources.Length; ++i)
        {
            cardResourceDictionary.Add(cardResources[i].itemName, cardResources[i]);
        }

        for (int i = 0; i < boxResources.Length; ++i)
        {
            boxResourceDictionary.Add(boxResources[i].itemName, boxResources[i]);
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
    public Dictionary<string, ItemResource> BoxResourceDictionary
    {
        get => boxResourceDictionary;
    }
    #endregion
}
