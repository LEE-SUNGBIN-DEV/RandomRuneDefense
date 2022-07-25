#define DEBUG_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

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
#if DEBUG_MODE
            Debug.Log("������ ������ �õ��մϴ�.");
#endif
        }
        else
        {
            UIManager.Instance.ShowNetworkState("�̹� ������ ����Ǿ��ֽ��ϴ�.");
#if DEBUG_MODE
            Debug.Log("�̹� ������ ����Ǿ��ֽ��ϴ�.");
#endif
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
#if DEBUG_MODE
        Debug.Log("������ ������ ������ϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("������ ������ ������ϴ�.");
        if (onConnect != null)
        {
            onConnect(false);
        }
    }
    public override void OnConnectedToMaster()
    {
#if DEBUG_MODE
        Debug.Log("������ ����Ǿ����ϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("������ ����Ǿ����ϴ�.");
        if (onConnect != null)
        {
            onConnect(true);
        }

        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
#if DEBUG_MODE
        Debug.Log("�κ� �����Ͽ����ϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("�κ� �����Ͽ����ϴ�.");
        onJoinLobby(true);
    }

    public override void OnJoinedRoom()
    {
#if DEBUG_MODE
        Debug.Log("�÷��̾ ��ٸ��� ���Դϴ�.");
#endif
        UIManager.Instance.ShowNetworkState("�÷��̾ ��ٸ��� ���Դϴ�.");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
#if DEBUG_MODE
        Debug.Log("������� �÷��̾ �����ϴ�.");
#endif
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
#if DEBUG_MODE
        Debug.Log("ȣ��Ʈ�Դϴ�.");
#endif
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
#if DEBUG_MODE
        Debug.Log("��Ī�� �����Ͽ����ϴ�.");
#endif
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
                PhotonNetwork.LoadLevel(Constant.NAME_SCENE_GAME);
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
