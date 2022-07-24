using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : Panel
{
    [SerializeField] private EquipCardSlot equipCardSlot;
    [SerializeField] private CardSlot[] inventorySlots;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        CardInformationPanel.onEquipCard -= EquipCard;
        CardInformationPanel.onEquipCard += EquipCard;

        CardInformationPanel.onReleaseCard -= ReleaseCard;
        CardInformationPanel.onReleaseCard += ReleaseCard;

        playerInventory = DataManager.Instance.PlayerData.PlayerInventory;        
    }

    private void OnEnable()
    {
        if (playerInventory.EquipCardName != null)
        {
            if(equipCardSlot.IsEmpty() == false)
            {
                equipCardSlot.ReleaseCard();
            }
            EquipCard(DataManager.Instance.CardDatabaseDictionary[playerInventory.EquipCardName]);
        }

        for (int i = 0; i < playerInventory.ItemNames.Count; ++i)
        {
            if (playerInventory.ItemNames[i] != null)
            {
                inventorySlots[i].RegisterCardToSlot(DataManager.Instance.CardDatabaseDictionary[playerInventory.ItemNames[i]]);
                inventorySlots[i].gameObject.SetActive(true);
            }

            else
            {
                inventorySlots[i].ClearSlot();
                inventorySlots[i].gameObject.SetActive(false);
            }
        }
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
    }

    public void EquipCard(Card requestCard)
    {
        equipCardSlot.EquipCard(requestCard);
    }

    public void ReleaseCard()
    {
        equipCardSlot.ReleaseCard();
    }
}
