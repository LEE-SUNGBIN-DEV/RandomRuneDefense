using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CardInformationPanel : Panel
{
    #region Events
    public static event UnityAction<Card> onEquipCard;
    public static event UnityAction onReleaseCard;
    #endregion

    [Header("Card Information")]
    [SerializeField] private Card card;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardDescriptionText;
    [SerializeField] private TextMeshProUGUI cardEffectText;
    [SerializeField] private Image cardImage;

    [Header("Button")]
    [SerializeField] private Button equipButton;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        CardSlot.onClickCardSlot -= SetCardInformation;
        CardSlot.onClickCardSlot += SetCardInformation;
    }

    private void OnDestroy()
    {
        CardSlot.onClickCardSlot -= SetCardInformation;
    }

    public void ClearSlot()
    {
        cardNameText.text = null;
        cardDescriptionText.text = null;
        cardEffectText.text = null;
        cardImage.sprite = null;
    }

    public void SetCardInformation(CardSlot cardSlot)
    {
        if (cardSlot.Card != null)
        {
            card = cardSlot.Card;
            cardNameText.text = card.ItemName;
            cardDescriptionText.text = card.ItemDescription;
            cardImage.sprite = card.ItemSprite;

            if (cardSlot is EquipCardSlot)
            {
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "«ÿ¡¶";
                equipButton.onClick.AddListener(OnClickReleaseButton);
            }

            else
            {
                equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "¿Â¬¯";
                equipButton.onClick.AddListener(OnClickEquipButton);
            }
        }
    }

    public void OnClickCloseButton()
    {
        ClearSlot();
        gameObject.SetActive(false);
    }

    public void OnClickEquipButton()
    {  
        if(card != null)
        {
            onEquipCard(card);
        }
    }

    public void OnClickReleaseButton()
    {
        if(card != null)
        {
            ClearSlot();
            onReleaseCard();
        }
    }
}
