using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

using TMPro;

public partial class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Events
    public static event UnityAction<bool> onLogin;
    public static event UnityAction<bool> onRegister;
    public static event UnityAction<bool> onConnect;
    public static event UnityAction<bool> onJoinLobby;

    public static event UnityAction<bool> onMatch;
    public static event UnityAction onPlayerEnteredRoom;
    #endregion

    [Header("Photon Settings")]
    [SerializeField] private string gameVersion;
    [SerializeField] private TextMeshProUGUI connectionInfomationText;

    #region Photon API
    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            // Set Game Version
            PhotonNetwork.GameVersion = GameVersion;

            // Try Connect to Master Server
            PhotonNetwork.ConnectUsingSettings();
            ConnectionInfomationText.text = "서버에 연결을 시도합니다.";
        }
        else
        {
            ConnectionInfomationText.text = "이미 서버와 연결되어있습니다.";
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        ConnectionInfomationText.text = "서버와 연결이 끊겼습니다.";
        if (onConnect != null)
        {
            onConnect(false);
        }
    }
    public override void OnConnectedToMaster()
    {
        ConnectionInfomationText.text = "서버와 연결되었습니다.";
        if (onConnect != null)
        {
            onConnect(true);
        }
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        ConnectionInfomationText.text = "로비에 입장하였습니다.";
        onJoinLobby(true);
    }

    public override void OnJoinedRoom()
    {
        ConnectionInfomationText.text = "플레이어를 기다리는 중입니다.";
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ConnectionInfomationText.text = "대기중인 플레이어가 없습니다.";
        onJoinLobby(false);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
    public override void OnCreatedRoom()
    {
        onPlayerEnteredRoom();
        ConnectionInfomationText.text = "호스트입니다.";
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
        ConnectionInfomationText.text = ("매칭에 실패하였습니다.");
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
                PhotonNetwork.LoadLevel(Constant.NAME_GAME_SCENE);
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
    public TextMeshProUGUI ConnectionInfomationText
    {
        get => connectionInfomationText;
    }
    #endregion
}
