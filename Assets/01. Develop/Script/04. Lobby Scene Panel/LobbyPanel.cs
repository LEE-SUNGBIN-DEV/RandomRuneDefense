using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class LobbyPanel : ScrollPanel
{
    [SerializeField] private GameObject quickMatchingPanel;
    [SerializeField] private TextMeshProUGUI quickMatchingText;
    [SerializeField] private Button quickMatchingButton;

    private void Awake()
    {
        NetworkManager.onMatch -= MatchResult;
        NetworkManager.onMatch += MatchResult;

        NetworkManager.onPlayerEnteredRoom -= UpdateQuickMatchPanel;
        NetworkManager.onPlayerEnteredRoom += UpdateQuickMatchPanel;
    }

    private void OnDestroy()
    {
        NetworkManager.onMatch -= MatchResult;
        NetworkManager.onPlayerEnteredRoom -= UpdateQuickMatchPanel;
    }

    private void MatchResult(bool isSuccess)
    {
        quickMatchingButton.interactable = true;

        if (isSuccess)
        {
            quickMatchingPanel.gameObject.SetActive(false);
        }
        else
        {
            quickMatchingText.text = "매칭에 실패하였습니다.";
        }
    }

    private void UpdateQuickMatchPanel()
    {
        quickMatchingText.text = "플레이어를 기다리고 있습니다.\n" + $"{PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
    }

    public void onClickMatchButton()
    {
        if (PhotonNetwork.IsConnected)
        {
            quickMatchingButton.interactable = false;
            quickMatchingPanel.gameObject.SetActive(true);
            quickMatchingText.text = "매칭을 시도합니다.";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public void OnClickMatchCancelButton()
    {
        PhotonNetwork.LeaveRoom();
        quickMatchingButton.interactable = true;
        quickMatchingPanel.gameObject.SetActive(false);
    }
}
