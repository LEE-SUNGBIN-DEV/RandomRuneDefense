using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanel : ScrollPanel
{
    [SerializeField] private List<Card> cardDatabase;
    [SerializeField] private CardSlot[] collectionSlots;
    [SerializeField] private Dictionary<string, CardSlot> collectionDictionary;

    private void Awake()
    {
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
