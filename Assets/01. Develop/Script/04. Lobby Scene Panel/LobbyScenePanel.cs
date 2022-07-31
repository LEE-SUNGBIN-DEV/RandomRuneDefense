using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [Header("Top Panel")]
    [SerializeField] private TopUserPanel topUserPanel;

    private IEnumerator scrollToTargetPanel;

    private void OnEnable()
    {
        Function.SetAllPanelActivation(lobbyScenePanels, true);
        MovePanel(lobbyPanel);
    }

    private void OnDisable()
    {
        Function.SetAllPanelActivation(lobbyScenePanels, false);
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
        float scrollTime = 0f;
        while (scrollTime < Constant.TIME_SCROLL)
        {
            scrollTime += Time.deltaTime / Constant.TIME_SCROLL;
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPanel.ScrollPosition, scrollTime);
            yield return null;
        }
        scrollbar.value = targetPanel.ScrollPosition;

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
