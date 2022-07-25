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
        PlayerInventory playerInventory = DataManager.Instance.PlayerData.PlayerInventory;

        for (int i = 0; i < playerInventory.ItemNames.Count; ++i)
        {
            if (playerInventory.ItemNames[i] != null)
            {
                ActiveCollectionSlot(collectionDictionary[playerInventory.ItemNames[i]]);
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
