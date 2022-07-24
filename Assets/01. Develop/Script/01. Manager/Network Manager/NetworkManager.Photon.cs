using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

using TMPro;

public partial class NetworkManager
{
    #region Events
    public static event UnityAction<bool> onConnect;
    public static event UnityAction<bool> onJoinLobby;

    public static event UnityAction<bool> onMatch;
    public static event UnityAction onPlayerEnteredRoom;
    #endregion

    [Header("Photon Settings")]
    [SerializeField] private string gameVersion;

    #region Photon API
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            // Set Game Version
            PhotonNetwork.GameVersion = GameVersion;

            // Try Connect to Master Server
            PhotonNetwork.ConnectUsingSettings();
            UIManager.Instance.ShowNetworkState("서버에 연결을 시도합니다.");
        }
        else
        {
            UIManager.Instance.ShowNetworkState("이미 서버와 연결되어있습니다.");
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        UIManager.Instance.ShowNetworkState("서버와 연결이 끊겼습니다.");
        if (onConnect != null)
        {
            onConnect(false);
        }
    }
    public override void OnConnectedToMaster()
    {
        UIManager.Instance.ShowNetworkState("서버와 연결되었습니다.");
        if (onConnect != null)
        {
            onConnect(true);
        }

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        UIManager.Instance.ShowNetworkState("로비에 입장하였습니다.");
        onJoinLobby(true);
    }

    public override void OnJoinedRoom()
    {
        UIManager.Instance.ShowNetworkState("플레이어를 기다리는 중입니다.");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        UIManager.Instance.ShowNetworkState("대기중인 플레이어가 없습니다.");
        onJoinLobby(false);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
    public override void OnCreatedRoom()
    {
        onPlayerEnteredRoom();
        UIManager.Instance.ShowNetworkState("호스트입니다.");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
        UIManager.Instance.ShowNetworkState("매칭에 실패하였습니다.");
        onMatch(false);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        onPlayerEnteredRoom();
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                onMatch(true);
                PhotonNetwork.LoadLevel(Constant.NAME_SCENE_GAME_);
            }
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }
    #endregion

    #region Property
    public string GameVersion
    {
        get => gameVersion;
        set => gameVersion = value;
    }
    #endregion
}
