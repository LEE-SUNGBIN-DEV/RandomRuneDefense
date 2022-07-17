using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private void Awake()
    {
        PlayerData.onPlayerDataChanged -= UpdateUGUI;
        PlayerData.onPlayerDataChanged += UpdateUGUI;
    }

    private void OnEnable()
    {
        Function.SetPanelsActivation(lobbyScenePanels, false);
        Function.OpenPanel(lobbyPanel);
        currentPanel = lobbyPanel;
    }

    private void OnDisable()
    {
        Function.SetPanelsActivation(lobbyScenePanels, false);
    }

    private void OnDestroy()
    {
        PlayerData.onPlayerDataChanged -= UpdateUGUI;
    }

    private void UpdateUGUI(PlayerData playerData)
    {
        topUserNicknameText.text = "�г���: " + playerData.Nickname;
        topUserGoldText.text = "���: " + playerData.Gold;
        topUserCrystalText.text = "ũ����Ż: " + playerData.Crystal;
    }

    public void OnClickBottomPanelButton(Panel selectPanel)
    {
        Function.ClosePanel(currentPanel);
        Function.OpenPanel(selectPanel, out currentPanel);
    }
}
