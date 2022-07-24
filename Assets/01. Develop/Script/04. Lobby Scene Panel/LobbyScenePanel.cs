using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class LobbyScenePanel : Panel
{
    [SerializeField] private Panel currentPanel = null;

    [Header("Panels in Lobby Scene")]
    [SerializeField] private Panel[] lobbyScenePanels;
    [SerializeField] private Shop shopPanel;
    [SerializeField] private Inventory inventoryPanel;
    [SerializeField] private Lobby lobbyPanel;
    [SerializeField] private CardCollection cardCollectionPanel;
    [SerializeField] private RuneInformation runeInformationPanel;

    [Header("Top User Panel")]
    [SerializeField] private TextMeshProUGUI topUserNicknameText;
    [SerializeField] private TextMeshProUGUI topUserGoldText;
    [SerializeField] private TextMeshProUGUI topUserCrystalText;
    [SerializeField] private TextMeshProUGUI topUserLevelText;

    private void Awake()
    {
        PlayerStats.onPlayerStatsChanged -= UpdateStatsUI;
        PlayerStats.onPlayerStatsChanged += UpdateStatsUI;
        PlayerCurrency.onPlayerCurrencyChanged -= UpdateCurrencyUI;
        PlayerCurrency.onPlayerCurrencyChanged += UpdateCurrencyUI;
    }

    private void OnEnable()
    {
        Function.SetAllPanelActivation(lobbyScenePanels, false);
        Function.OpenPanel(lobbyPanel);
        currentPanel = lobbyPanel;
    }

    private void OnDisable()
    {
        Function.SetAllPanelActivation(lobbyScenePanels, false);
    }

    private void OnDestroy()
    {
        PlayerStats.onPlayerStatsChanged -= UpdateStatsUI;
        PlayerCurrency.onPlayerCurrencyChanged -= UpdateCurrencyUI;
    }

    private void UpdateCurrencyUI(PlayerCurrency playerCurrency)
    {
        topUserGoldText.text = playerCurrency.Gold.ToString();
        topUserCrystalText.text = playerCurrency.Crystal.ToString();
    }
    private void UpdateStatsUI(PlayerStats playerStats)
    {
        topUserNicknameText.text = PhotonNetwork.LocalPlayer.NickName;
        topUserLevelText.text = playerStats.Level.ToString();
    }

    public void OnClickBottomPanelButton(Panel selectPanel)
    {
        Function.ClosePanel(currentPanel);
        Function.OpenPanel(selectPanel, out currentPanel);
    }
}
