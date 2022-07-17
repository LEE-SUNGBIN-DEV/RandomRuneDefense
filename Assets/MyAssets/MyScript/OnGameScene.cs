using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnGameScene : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.onSceneLoaded -= CreatePlayer;
        GameManager.onSceneLoaded += CreatePlayer;
    }

    private void OnDisable()
    {
        GameManager.onSceneLoaded -= CreatePlayer;
    }

    private void CreatePlayer(string sceneName)
    {
        if (sceneName == Constant.NAME_GAME_SCENE)
        {
            PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
        }
    }
}
