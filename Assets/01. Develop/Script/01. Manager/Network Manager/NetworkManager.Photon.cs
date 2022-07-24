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
            UIManager.Instance.ShowNetworkState("������ ������ �õ��մϴ�.");
        }
        else
        {
            UIManager.Instance.ShowNetworkState("�̹� ������ ����Ǿ��ֽ��ϴ�.");
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        UIManager.Instance.ShowNetworkState("������ ������ ������ϴ�.");
        if (onConnect != null)
        {
            onConnect(false);
        }
    }
    public override void OnConnectedToMaster()
    {
        UIManager.Instance.ShowNetworkState("������ ����Ǿ����ϴ�.");
        if (onConnect != null)
        {
            onConnect(true);
        }

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        UIManager.Instance.ShowNetworkState("�κ� �����Ͽ����ϴ�.");
        onJoinLobby(true);
    }

    public override void OnJoinedRoom()
    {
        UIManager.Instance.ShowNetworkState("�÷��̾ ��ٸ��� ���Դϴ�.");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        UIManager.Instance.ShowNetworkState("������� �÷��̾ �����ϴ�.");
        onJoinLobby(false);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
    public override void OnCreatedRoom()
    {
        onPlayerEnteredRoom();
        UIManager.Instance.ShowNetworkState("ȣ��Ʈ�Դϴ�.");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
        UIManager.Instance.ShowNetworkState("��Ī�� �����Ͽ����ϴ�.");
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
