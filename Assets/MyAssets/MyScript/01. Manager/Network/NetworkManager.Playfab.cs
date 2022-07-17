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
    // 현재 PlayFab 사용자를 정상적으로 인증합니다.
    // 인증이 성공한 경우 다음 단계를 위한 콜백으로 RequestPhotonToken을 전달합니다.
    public void LoginWithPlayFab(string userName, string userPassword)
    {
        ConnectionInfomationText.text = "로그인 정보를 확인합니다.";

        var request = new LoginWithPlayFabRequest { Username = userName, Password = userPassword };
        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnPlayFabError);
    }

    // Step 2
    // PlayFab에서 Photon 인증 토큰을 요청합니다.
    // 인증에 합격했습니다.만약 우리가 성공적으로 토큰을 획득했다면, Photon을 콜백으로 하여 우리의 다음 단계가 될 것입니다.
    private void RequestToken(LoginResult loginResult)
    {
        ConnectionInfomationText.text = "네트워크에 인증 토큰을 요청합니다.";

        // 포톤 토큰
        playFabUserId = loginResult.PlayFabId;

        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
        }, AuthenticateWithPhoton, OnPlayFabError);

        // 엔터티 토큰
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

        //AuthType을 사용자 지정으로 설정합니다. 즉, 자체 PlayFab 인증 절차를 가져옵니다.
        //"username" 매개변수를 추가합니다. PlayFab은 이 매개 변수에 사용자 이름이 아닌 플레이어 PlayFab ID(!)가 포함될 것으로 예상합니다.
        // "token" 매개변수를 추가합니다. PlayFab은 이전 단계에서 Photon 인증 토큰 문제를 포함할 것으로 예상합니다.
        //우리는 마침내 Photon에게 전체 애플리케이션에서 이 인증 매개변수를 사용하도록 지시합니다.
        AuthenticationValues customAuthenticationValues = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        customAuthenticationValues.AddAuthParameter("username", playFabUserId);
        customAuthenticationValues.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);
        PhotonNetwork.AuthValues = customAuthenticationValues;

        OnLogin();
    }

    private void OnLogin()
    {
        ConnectionInfomationText.text = "로그인에 성공하였습니다.";
        onLogin(true);
        Connect();
    }
    #endregion
    #region Register
    // ! 가입
    public void Register(string userID, string userPassword, string userEmail, string nickname)
    {
        var request = new RegisterPlayFabUserRequest { Username = userID, Password = userPassword, Email = userEmail, DisplayName = nickname };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }
    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        UIManager.Instance.DoSystemNotice("회원가입에 성공하였습니다.");
        onRegister(true);
    }

    private void RegisterFailure(PlayFabError error)
    {
        UIManager.Instance.DoSystemNotice("회원가입에 실패하였습니다.");
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
