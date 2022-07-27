using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryPanel : ScrollPanel
{
    [SerializeField] private EquipCardSlot equipCardSlot;
    [SerializeField] private CardSlot[] inventorySlots;

    private void Awake()
    {
        CardInformationPanel.onEquipCard -= EquipCard;
        CardInformationPanel.onEquipCard += EquipCard;

        CardInformationPanel.onReleaseCard -= ReleaseCard;
        CardInformationPanel.onReleaseCard += ReleaseCard;

        PlayerInventory.onLoadPlayerInventory -= RefreshInventory;
        PlayerInventory.onLoadPlayerInventory += RefreshInventory;

    }

    private void OnEnable()
    {
        RefreshInventory(DataManager.Instance.PlayerData.PlayerInventory);
    }

    private void OnDisable()
    {
        for(int i=0; i<inventorySlots.Length; ++i)
        {
            inventorySlots[i].ClearSlot();
            inventorySlots[i].gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        CardInformationPanel.onEquipCard -= EquipCard;
        CardInformationPanel.onReleaseCard -= ReleaseCard;
        PlayerInventory.onLoadPlayerInventory -= RefreshInventory;

    }

    public void RefreshInventory(PlayerInventory playerInventory)
    {
        Dictionary<string, Card> cardDatabase = DataManager.Instance.CardDatabaseDictionary; 
        if (playerInventory.EquipCardName != null)
        {
            EquipCard(cardDatabase[playerInventory.EquipCardName]);
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
    }

    public void EquipCard(Card requestCard)
    {
        if (equipCardSlot.IsEmpty() == false)
        {
            ReleaseCard();
        }
        equipCardSlot.EquipCard(requestCard);
    }

    public void ReleaseCard()
    {
        equipCardSlot.ReleaseCard();
    }
}
