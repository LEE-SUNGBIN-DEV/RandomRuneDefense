using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CardSlot : Slot
{
    #region Event
    public static event UnityAction<CardSlot> onClickCardSlot;
    #endregion
    [SerializeField] private Card card;
    [SerializeField] private Image cardImage;
    [SerializeField] private Button cardButton;
    [SerializeField] private TextMeshProUGUI cardName;

    public bool IsEmpty()
    {
        return card == null;
    }
    public void RegisterCardToSlot(Card requestCard)
    {
        Card = requestCard;
        CardImage.sprite = Card.ItemSprite;
        CardName.text = requestCard.ItemName;

        CardImage.color = Function.SetAlpha(CardImage.color, 1f);
    }

    public override void ClearSlot()
    {
        Card = null;
        CardImage.sprite = null;
        CardName.text = null;

        CardImage.color = Function.SetAlpha(CardImage.color, 0f);
    }

    public void OnClickCardSlot()
    {
        if (card != null)
        {
            Function.OpenPanel(UIManager.Instance.CardInformationPanel);
            onClickCardSlot(this);
        }
    }

    public void ActiveCardSlot()
    {
        cardImage.color = Color.white;
        cardButton.interactable = true;
    }

    public void InactiveCardSlot()
    {
        cardImage.color = Color.gray;
        cardButton.interactable = false;
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
    public TextMeshProUGUI CardName
    {
        get => cardName;
    }
    #endregion
}
