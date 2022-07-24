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
    [SerializeField] private Image cardImage;
    [SerializeField] private Button cardButton;

    public bool IsEmpty()
    {
        return card == null;
    }
    public void RegisterCardToSlot(Card requestCard)
    {
        Card = requestCard;
        CardImage.sprite = Card.ItemSprite;
    }

    public override void ClearSlot()
    {
        Card = null;
        CardImage.sprite = null;
    }

    public void OnClickCardSlot()
    {
        if (card != null)
        {
            Function.OpenPanel(UIManager.Instance.CardInformationPanel);
            onClickCardSlot(this);
        }
    }

    #region Property
    public Card Card
    {
        get => card;
        set => card = value;
    }
    public Image CardImage
    {
        get => cardImage;
    }
    #endregion
}
