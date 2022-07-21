using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardSlot : Slot
{
    #region Event
    public static event UnityAction<CardSlot> onClickCardSlot;
    #endregion
    [SerializeField] private Card card;
    [SerializeField] private Button cardButton;

    private void Start()
    {
        cardButton = GetComponent<Button>();
/*
        if (card == null)
        {
            cardButton.interactable = false;
        }

        else
        {
            cardButton.interactable = true;
        }
*/
        cardButton.onClick.AddListener(OnClickCardSlot);
    }

    private void OnClickCardSlot()
    {
        Function.OpenPanel(UIManager.Instance.CardInformationPanel);
        if (card != null)
        {
            onClickCardSlot(this);
        }
    }

    #region Property
    public Card Card
    {
        get => card;
        set => card = value;
    }
    #endregion
}
