using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : ScrollPanel
{
    [SerializeField] private ShopSlot[] goldBoxSlots;
    [SerializeField] private ShopSlot[] cardBoxSlots;
    [SerializeField] private ContentSizeFitter[] sizeFitters;

    private void Awake()
    {
        DataManager.onLoadDatabase -= RefreshShop;
        DataManager.onLoadDatabase += RefreshShop;
    }

    private void OnEnable()
    {
        RefreshShop();
    }

    private void OnDisable()
    {
        for (int i = 0; i < goldBoxSlots.Length; ++i)
        {
            goldBoxSlots[i].ClearSlot();
            goldBoxSlots[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < cardBoxSlots.Length; ++i)
        {
            cardBoxSlots[i].ClearSlot();
            cardBoxSlots[i].gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        DataManager.onLoadDatabase -= RefreshShop;
    }

    
    public void RefreshShop()
    {
        /*
        var cardDatabase = DataManager.Instance.CardDatabaseDictionary;
        if (playerInventory.EquipCardName != null)
        {
            RegisterSellList(cardDatabase[playerInventory.EquipCardName]);
        }

        for (int i = 0; i < playerInventory.ItemNames.Count; ++i)
        {
            if (playerInventory.ItemNames[i] != null)
            {
                inventorySlots[i].RegisterCardToSlot(cardDatabase[playerInventory.ItemNames[i]]);
                inventorySlots[i].gameObject.SetActive(true);
            }

            else
            {
                inventorySlots[i].ClearSlot();
                inventorySlots[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < sizeFitters.Length; ++i)
        {
            sizeFitters[i].enabled = false;
            sizeFitters[i].enabled = true;
        }
    */
    }

    /*
    public void RegisterSellList(Card requestCard)
    {
        if (equipCardSlot.IsEmpty() == false)
        {
            ReleaseCard();
        }
        equipCardSlot.EquipCard(requestCard);
    }
    */
}
