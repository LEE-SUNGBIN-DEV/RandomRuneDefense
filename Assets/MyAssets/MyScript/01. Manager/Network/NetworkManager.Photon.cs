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
            ConnectionInfomationText.text = "������ ������ �õ��մϴ�.";
        }
        else
        {
            ConnectionInfomationText.text = "�̹� ������ ����Ǿ��ֽ��ϴ�.";
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        ConnectionInfomationText.text = "������ ������ ������ϴ�.";
        if (onConnect != null)
        {
            onConnect(false);
        }
    }
    public override void OnConnectedToMaster()
    {
        ConnectionInfomationText.text = "������ ����Ǿ����ϴ�.";
        if (onConnect != null)
        {
            onConnect(true);
        }
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        ConnectionInfomationText.text = "�κ� �����Ͽ����ϴ�.";
        onJoinLobby(true);
    }

    public override void OnJoinedRoom()
    {
        ConnectionInfomationText.text = "�÷��̾ ��ٸ��� ���Դϴ�.";
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ConnectionInfomationText.text = "������� �÷��̾ �����ϴ�.";
        onJoinLobby(false);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
    public override void OnCreatedRoom()
    {
        onPlayerEnteredRoom();
        ConnectionInfomationText.text = "ȣ��Ʈ�Դϴ�.";
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
        ConnectionInfomationText.text = ("��Ī�� �����Ͽ����ϴ�.");
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
