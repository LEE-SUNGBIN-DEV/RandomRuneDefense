using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChatPanel : Panel
{
    private void OnEnable()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
    }
    private void OnDisable()
    {
        PhotonNetwork.IsMessageQueueRunning = false;
    }
}
