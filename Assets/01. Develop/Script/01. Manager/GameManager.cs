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

    [SerializeField] private string currentSceneName = null;
    [SerializeField] private PlayerData playerData = null;

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
            SceneManager.LoadScene(Constant.NAME_LOBBY_SCENE);
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        onSceneLoaded(currentSceneName);
    }

    #region Property
    public PlayerData PlayerData
    {
        get => playerData;
        set => playerData = value;
    }
    public string CurrentSceneName
    {
        get => currentSceneName;
    }
    #endregion
}
