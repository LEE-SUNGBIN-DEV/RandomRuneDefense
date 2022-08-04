#define DEBUG_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Photon.Pun;

public class TopUserPanel : Panel
{
    #region Events
    public static event UnityAction<TopUserPanel> onClickPortrait;
    #endregion

    [Header("Portrait")]
    [SerializeField] private Card equipCard;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Currency")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI crystalText;

    [Header("Experience")]
    [SerializeField] private Image experienceFillImage;
    [SerializeField] TextMeshProUGUI experienceText;

    private void Awake()
    {
        PlayerStats.onPlayerStatsChanged -= UpdateStatsUI;
        PlayerStats.onPlayerStatsChanged += UpdateStatsUI;
        PlayerCurrency.onPlayerCurrencyChanged -= UpdateCurrencyUI;
        PlayerCurrency.onPlayerCurrencyChanged += UpdateCurrencyUI;
        InventoryPanel.onInventoryChanged -= UpdatePortrait;
        InventoryPanel.onInventoryChanged += UpdatePortrait;
    }

    private void OnEnable()
    {
        UpdatePortrait(UIManager.Instance.LobbyScenePanel.InventoryPanel);
        UpdateCurrencyUI(DataManager.Instance.PlayerData.PlayerCurrency);
        UpdateStatsUI(DataManager.Instance.PlayerData.PlayerStats);
    }

    private void OnDestroy()
    {
        PlayerStats.onPlayerStatsChanged -= UpdateStatsUI;
        PlayerCurrency.onPlayerCurrencyChanged -= UpdateCurrencyUI;
        InventoryPanel.onInventoryChanged -= UpdatePortrait;
    }

    public void UpdatePortrait(InventoryPanel inventoryPanel)
    {
        if(inventoryPanel.EquipCardSlot.Card != null)
        {
            equipCard = inventoryPanel.EquipCardSlot.Card;
            cardImage.sprite = equipCard.ItemSprite;
            cardImage.color = Function.SetAlpha(cardImage.color, 1f);
        }
        else
        {
#if DEBUG_MODE
            Debug.Log("TopUserPanel: inventoryPanel.EquipCardSlot.Card == NULL");
#endif
            equipCard = null;
            cardImage.sprite = null;
            cardImage.color = Function.SetAlpha(cardImage.color, 0f);
        }
    }

    public void UpdateCurrencyUI(PlayerCurrency playerCurrency)
    {
        goldText.text = playerCurrency.Gold.ToString();
        crystalText.text = playerCurrency.Crystal.ToString();
    }
    public void UpdateStatsUI(PlayerStats playerStats)
    {
        nicknameText.text = PhotonNetwork.LocalPlayer.NickName;
        levelText.text = playerStats.Level.ToString();
    }

    public void OnClickPortrait()
    {
        Function.OpenPanel(UIManager.Instance.UserInformationPanel);
        onClickPortrait(this);
    }

    #region Property
    public Card EquipCard
    {
        get => equipCard;
    }
    #endregion
}
