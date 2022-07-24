using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Events
    public static UnityAction<string> onSceneLoaded;
    #endregion

    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private string currentSceneName = null;

    public override void Initialize()
    {
        Screen.SetResolution(Constant.SCREEN_RESOLUTION_WIDTH, Constant.SCREEN_RESOLUTION_HEIGHT, false);

        SceneManager.sceneLoaded -= SceneLoaded;
        SceneManager.sceneLoaded += SceneLoaded;

        NetworkManager.onJoinLobby -= LoadLobbyScene;
        NetworkManager.onJoinLobby += LoadLobbyScene;
    }

    private void Start()
    {
        onSceneLoaded(CurrentSceneName);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
        NetworkManager.onJoinLobby -= LoadLobbyScene;
    }

    private void LoadLobbyScene(bool isConnect)
    {
        if(isConnect)
        {
            SceneManager.LoadScene(Constant.NAME_SCENE_LOBBY);
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        if(scene.name == Constant.NAME_SCENE_GAME_)
        {
            Screen.SetResolution(Constant.SCREEN_RESOLUTION_HEIGHT, Constant.SCREEN_RESOLUTION_WIDTH, false);
        }
        else
        {
            Screen.SetResolution(Constant.SCREEN_RESOLUTION_WIDTH, Constant.SCREEN_RESOLUTION_HEIGHT, false);
        }
        onSceneLoaded(currentSceneName);
    }

    #region Property
    public NetworkManager NetworkManager
    {
        get => networkManager;
    }
    public string CurrentSceneName
    {
        get => currentSceneName;
    }
    #endregion
}
