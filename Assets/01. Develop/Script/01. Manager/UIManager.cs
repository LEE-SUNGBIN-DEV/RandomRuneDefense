using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Dictionary<string, Panel> scenePanelDictionary;
    [SerializeField] private Panel[] scenePanels;
    [SerializeField] private TitleScenePanel titleScenePanel;
    [SerializeField] private LobbyScenePanel lobbyScenePanel;
    [SerializeField] private GameScenePanel gameScenePanel;
    [SerializeField] private SystemNoticePanel systemNoticePanel;
    [SerializeField] private CardInformationPanel cardInformationPanel;
    [SerializeField] private NetworkStatePanel networkStatePanel;

    public override void Initialize()
    {
        GameManager.onSceneLoaded -= SceneLoaded;
        GameManager.onSceneLoaded += SceneLoaded;

        scenePanelDictionary = new Dictionary<string, Panel>
        {
            {Constant.NAME_SCENE_TITLE, titleScenePanel },
            {Constant.NAME_SCENE_LOBBY, lobbyScenePanel },
            {Constant.NAME_SCENE_GAME, gameScenePanel }
        };
    }

    private void OnDestroy()
    {
        GameManager.onSceneLoaded -= SceneLoaded;
    }

    public void SceneLoaded(string sceneName)
    {
        Function.SetAllPanelActivation(ScenePanels, false);
        Function.OpenPanel(scenePanelDictionary[sceneName]);
    }

    public void DoSystemNotice(string content)
    {
        SystemNoticePanel.SystemNoticeContent.text = content;
        Function.OpenPanel(SystemNoticePanel);
    }

    public void ShowNetworkState(string content)
    {
        if (NetworkStatePanel != null)
        {
            Function.OpenPanel(NetworkStatePanel);
            networkStatePanel.ShowNetworkStateMessage(content);
        }
    }

    #region Property
    public Panel[] ScenePanels
    {
        get => scenePanels;
    }
    public TitleScenePanel TitleScenePanel
    {
        get => titleScenePanel;
    }
    public LobbyScenePanel LobbyScenePanel
    {
        get => lobbyScenePanel;
    }
    public Panel GameScenePanel
    {
        get => gameScenePanel;
    }
    public SystemNoticePanel SystemNoticePanel
    {
        get => systemNoticePanel;
    }
    public CardInformationPanel CardInformationPanel
    {
        get => cardInformationPanel;
    }
    public NetworkStatePanel NetworkStatePanel
    {
        get => networkStatePanel;
    }
    #endregion
}
