using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Photon.Pun;

public class LobbyScenePanel : Panel, IBeginDragHandler, IEndDragHandler
{
    [Header("Current Panel")]
    [SerializeField] private ScrollPanel currentPanel;

    [Header("Lobby Scene Panel Array")]
    [SerializeField] private ScrollPanel[] lobbyScenePanels;

    [Header("Lobby Scene Scrollbar")]
    [SerializeField] private Scrollbar scrollbar;

    [Header("Lobby Scene Panels")]
    [SerializeField] private ShopPanel shopPanel;
    [SerializeField] private InventoryPanel inventoryPanel;
    [SerializeField] private LobbyPanel lobbyPanel;
    [SerializeField] private CollectionPanel collectionPanel;
    [SerializeField] private RunePanel runePanel;

    [Header("Top User Panel")]
    [SerializeField] private TextMeshProUGUI topUserNicknameText;
    [SerializeField] private TextMeshProUGUI topUserGoldText;
    [SerializeField] private TextMeshProUGUI topUserCrystalText;
    [SerializeField] private TextMeshProUGUI topUserLevelText;

    private IEnumerator scrollToTargetPanel;
    private bool isScroll = false;

    private void Awake()
    {
        PlayerStats.onPlayerStatsChanged -= UpdateStatsUI;
        PlayerStats.onPlayerStatsChanged += UpdateStatsUI;
        PlayerCurrency.onPlayerCurrencyChanged -= UpdateCurrencyUI;
        PlayerCurrency.onPlayerCurrencyChanged += UpdateCurrencyUI;
    }

    private void OnEnable()
    {
        Function.SetAllPanelActivation(lobbyScenePanels, true);
        MovePanel(lobbyPanel);
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

    public void MovePanel(ScrollPanel targetPanel)
    {
        if(scrollToTargetPanel != null)
        {
            StopCoroutine(scrollToTargetPanel);
        }
        scrollToTargetPanel = ScrollToTargetPanel(targetPanel);
        StartCoroutine(scrollToTargetPanel);
    }

    public IEnumerator ScrollToTargetPanel(ScrollPanel targetPanel)
    {
        isScroll = true;
        float scrollTime = 0f;
        while (scrollTime < Constant.TIME_SCROLL)
        {
            scrollTime += Time.deltaTime / Constant.TIME_SCROLL;
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPanel.ScrollPosition, scrollTime);
            yield return null;
        }
        scrollbar.value = targetPanel.ScrollPosition;
        isScroll = false;

        currentPanel = targetPanel;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (scrollToTargetPanel != null)
        {
            StopCoroutine(scrollToTargetPanel);
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        for(int i=0; i<lobbyScenePanels.Length; ++i)
        {
            if (lobbyScenePanels[i].IsTargetScrollPanel(scrollbar.value))
            {
                MovePanel(lobbyScenePanels[i]);
            }
        }
    }
}
