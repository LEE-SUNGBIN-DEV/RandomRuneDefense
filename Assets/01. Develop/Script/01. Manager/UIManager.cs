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

    public override void Initialize()
    {
        GameManager.onSceneLoaded -= SceneLoaded;
        GameManager.onSceneLoaded += SceneLoaded;

        scenePanelDictionary = new Dictionary<string, Panel>
        {
            {Constant.NAME_TITLE_SCENE, titleScenePanel },
            {Constant.NAME_LOBBY_SCENE, lobbyScenePanel },
            {Constant.NAME_GAME_SCENE, gameScenePanel }
        };
    }

    private void Start()
    {
        Function.OpenPanel(TitleScenePanel);
    }

    private void OnDestroy()
    {
        GameManager.onSceneLoaded -= SceneLoaded;
    }

    public void SceneLoaded(string sceneName)
    {
        Function.SetPanelsActivation(scenePanels, false);
        Function.OpenPanel(scenePanelDictionary[sceneName]);
    }

    public void DoSystemNotice(string content)
    {
        SystemNoticePanel.SystemNoticeContent.text = content;
        Function.OpenPanel(SystemNoticePanel);
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
    #endregion
}
