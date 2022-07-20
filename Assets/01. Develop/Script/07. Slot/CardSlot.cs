using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : Slot
{
    [SerializeField] private Card card;
    [SerializeField] private Button cardButton;

    private void Start()
    {
        cardButton = GetComponent<Button>();

        if (card == null)
        {
            cardButton.interactable = false;
            cardButton.onClick.AddListener(OnClickCardSlot);
        }

        else
        {
            cardButton.interactable = true;
        }
    }

    private void OnClickCardSlot()
    {
        if(card != null)
        {
        }
    }
}
