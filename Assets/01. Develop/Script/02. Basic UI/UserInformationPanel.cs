using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UserInformationPanel : Panel
{
    [Header("User Information")]
    [SerializeField] private Card equipCard;
    [SerializeField] private TextMeshProUGUI equipCardNameText;
    [SerializeField] private TextMeshProUGUI equipCardDescriptionText;
    [SerializeField] private Image equipCardImage;

    [Header("User Stat Panel")]
    [SerializeField] private StatPanel userStatPanel;

    [Header("Button")]
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        TopUserPanel.onClickPortrait -= SetUserInformation;
        TopUserPanel.onClickPortrait += SetUserInformation;
    }

    private void OnDestroy()
    {
        TopUserPanel.onClickPortrait -= SetUserInformation;
    }

    public void ClearSlot()
    {
        equipCardNameText.text = null;
        equipCardDescriptionText.text = null;
        equipCardImage.sprite = null;
        equipCardImage.color = Function.SetAlpha(equipCardImage.color, 0f);
        equipCard = null;
    }

    public void SetUserInformation(TopUserPanel topUserPanel)
    {
        if (topUserPanel.EquipCard != null)
        {
            equipCard = topUserPanel.EquipCard;
            equipCardNameText.text = equipCard.ItemName;
            equipCardDescriptionText.text = equipCard.ItemDescription;
            equipCardImage.sprite = equipCard.ItemSprite;
            equipCardImage.color = Function.SetAlpha(equipCardImage.color, 1f);
        }

        userStatPanel.SetStatPanel(DataManager.Instance.PlayerData.PlayerStats);
    }

    public void OnClickCloseButton()
    {
        ClearSlot();
        gameObject.SetActive(false);
    }

}
