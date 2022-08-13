using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPanel : ScrollPanel
{
    [SerializeField] private List<Card> cardDatabase;
    [SerializeField] private CardSlot[] collectionSlots;
    [SerializeField] private Dictionary<string, CardSlot> collectionDictionary;
    [SerializeField] private ContentSizeFitter[] sizeFitters;

    private void Awake()
    {
        sizeFitters = GetComponentsInChildren<ContentSizeFitter>();
        PlayerCollections.onPlayerCollectionsChanged -= RefreshCollections;
        PlayerCollections.onPlayerCollectionsChanged += RefreshCollections;

        cardDatabase = DataManager.Instance.CardDatabase;
        collectionDictionary = new Dictionary<string, CardSlot>();

        for (int i=0; i < cardDatabase.Count; ++i)
        {
            collectionSlots[i].RegisterCardToSlot(cardDatabase[i]);
            collectionSlots[i].gameObject.SetActive(true);
            InactiveCollectionSlot(collectionSlots[i]);

            collectionDictionary.Add(cardDatabase[i].ItemName, collectionSlots[i]);
        }
    }

    private void OnEnable()
    {
        RefreshCollections(DataManager.Instance.PlayerData.PlayerCollections);
    }

    private void OnDestroy()
    {
        PlayerCollections.onPlayerCollectionsChanged -= RefreshCollections;
    }

    public void RefreshCollections(PlayerCollections playerCollections)
    {
        for (int i = 0; i < playerCollections.CollectedCardNames.Count; ++i)
        {
            if (playerCollections.CollectedCardNames[i] != null)
            {
                ActiveCollectionSlot(collectionDictionary[playerCollections.CollectedCardNames[i]]);
            }
        }

        for (int i = 0; i < sizeFitters.Length; ++i)
        {
            sizeFitters[i].enabled = false;
            sizeFitters[i].enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sizeFitters[i].transform);
        }
    }

    private void ActiveCollectionSlot(CardSlot cardSlot)
    {
        cardSlot.ActiveCardSlot();
    }

    private void InactiveCollectionSlot(CardSlot cardSlot)
    {
        cardSlot.InactiveCardSlot();
    }
}
