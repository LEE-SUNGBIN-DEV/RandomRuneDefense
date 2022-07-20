using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public partial class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        TitleScenePanel.onRequestLogin -= LoginWithPlayFab;
        TitleScenePanel.onRequestLogin += LoginWithPlayFab;
        TitleScenePanel.onRequestRegister -= Register;
        TitleScenePanel.onRequestRegister += Register;

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !chatInputField.isFocused)
        {
            OnSendButtonClicked();
        }
    }
    private void OnDestroy()
    {
        TitleScenePanel.onRequestLogin -= LoginWithPlayFab;
        TitleScenePanel.onRequestRegister -= Register;
    }

}
