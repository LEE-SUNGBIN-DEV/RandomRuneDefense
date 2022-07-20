using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public partial class NetworkManager
{
    [Header("Chat Settings")]
    [SerializeField] private Button sendButton;
    [SerializeField] private TextMeshProUGUI chatOutput;
    [SerializeField] private TMP_InputField chatInputField;
    [SerializeField] private ScrollRect chatScrollRect;

    public void OnSendButtonClicked()
    {
        if (chatInputField.text.Equals(""))
        {
            return;
        }
        string message = string.Format($"[{PhotonNetwork.LocalPlayer.NickName}]: {chatInputField.text}");
        photonView.RPC("ReceiveMessage", RpcTarget.OthersBuffered, message);
        ReceiveMessage(message);
        chatInputField.ActivateInputField();
        chatInputField.text = "";
    }

    [PunRPC]
    public void ReceiveMessage(string message)
    {
        chatOutput.text += "\n" + message;
        chatScrollRect.verticalNormalizedPosition = 0.0f;
    }

}
