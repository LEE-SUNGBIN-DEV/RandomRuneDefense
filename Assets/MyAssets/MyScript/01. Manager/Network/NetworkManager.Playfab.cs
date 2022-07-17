using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.Realtime;

public partial class NetworkManager
{
    [Header("Playfab Settings")]
    private string playFabUserId;

    #region Playfab API
    #region Login
    // Step 1
    // ���� PlayFab ����ڸ� ���������� �����մϴ�.
    // ������ ������ ��� ���� �ܰ踦 ���� �ݹ����� RequestPhotonToken�� �����մϴ�.
    public void LoginWithPlayFab(string userName, string userPassword)
    {
        ConnectionInfomationText.text = "�α��� ������ Ȯ���մϴ�.";

        var request = new LoginWithPlayFabRequest { Username = userName, Password = userPassword };
        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnPlayFabError);
    }

    // Step 2
    // PlayFab���� Photon ���� ��ū�� ��û�մϴ�.
    // ������ �հ��߽��ϴ�.���� �츮�� ���������� ��ū�� ȹ���ߴٸ�, Photon�� �ݹ����� �Ͽ� �츮�� ���� �ܰ谡 �� ���Դϴ�.
    private void RequestToken(LoginResult loginResult)
    {
        ConnectionInfomationText.text = "��Ʈ��ũ�� ���� ��ū�� ��û�մϴ�.";

        // ���� ��ū
        playFabUserId = loginResult.PlayFabId;

        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
        }, AuthenticateWithPhoton, OnPlayFabError);

        // ����Ƽ ��ū
        playFabEntityId = loginResult.EntityToken.Entity.Id;
        playFabEntityType = loginResult.EntityToken.Entity.Type;

        LoadPlayerData();
        GetUserData();
        UpdateUserData();
    }

    // Step 3
    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        LogMessage("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

        //AuthType�� ����� �������� �����մϴ�. ��, ��ü PlayFab ���� ������ �����ɴϴ�.
        //"username" �Ű������� �߰��մϴ�. PlayFab�� �� �Ű� ������ ����� �̸��� �ƴ� �÷��̾� PlayFab ID(!)�� ���Ե� ������ �����մϴ�.
        // "token" �Ű������� �߰��մϴ�. PlayFab�� ���� �ܰ迡�� Photon ���� ��ū ������ ������ ������ �����մϴ�.
        //�츮�� ��ħ�� Photon���� ��ü ���ø����̼ǿ��� �� ���� �Ű������� ����ϵ��� �����մϴ�.
        AuthenticationValues customAuthenticationValues = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        customAuthenticationValues.AddAuthParameter("username", playFabUserId);
        customAuthenticationValues.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);
        PhotonNetwork.AuthValues = customAuthenticationValues;

        OnLogin();
    }

    private void OnLogin()
    {
        ConnectionInfomationText.text = "�α��ο� �����Ͽ����ϴ�.";
        onLogin(true);
        Connect();
    }
    #endregion
    #region Register
    // ! ����
    public void Register(string userID, string userPassword, string userEmail, string nickname)
    {
        var request = new RegisterPlayFabUserRequest { Username = userID, Password = userPassword, Email = userEmail, DisplayName = nickname };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }
    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        UIManager.Instance.DoSystemNotice("ȸ�����Կ� �����Ͽ����ϴ�.");
        onRegister(true);
    }

    private void RegisterFailure(PlayFabError error)
    {
        UIManager.Instance.DoSystemNotice("ȸ�����Կ� �����Ͽ����ϴ�.");
        Debug.LogWarning(error.GenerateErrorReport());
        onRegister(false);
    }
    #endregion
    #region Message
    private void OnPlayFabError(PlayFabError obj)
    {
        LogMessage(obj.GenerateErrorReport());
        onLogin(false);
    }
    public void LogMessage(string message)
    {
        Debug.Log("PlayFab + Photon : " + message);
    }
    #endregion
    
    #endregion
}
