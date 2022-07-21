using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;
using Photon.Realtime;

public partial class NetworkManager
{
    #region Events
    public static event UnityAction<bool> onLogin;
    public static event UnityAction<bool> onRegister;
    #endregion

    [Header("Playfab Settings")]
    private string playFabUserId;

    #region Playfab API
    #region Login
    public void LoginWithPlayFab(string userName, string userPassword)
    {
        UIManager.Instance.ShowNetworkState("로그인 정보를 확인합니다.");

        var request = new LoginWithPlayFabRequest { Username = userName, Password = userPassword };
        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnPlayFabError);
    }

    private void RequestToken(LoginResult loginResult)
    {
        UIManager.Instance.ShowNetworkState("네트워크에 인증 토큰을 요청합니다.");

        // 플레이팹 토큰
        playFabUserId = loginResult.PlayFabId;
        DataManager.Instance.SetPlayFabEntity(loginResult);

        // 포톤 토큰
        PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
        }, AuthenticateWithPhoton, OnPlayFabError);
    }

    // Step 3
    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        Debug.Log("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

        AuthenticationValues customAuthenticationValues = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        customAuthenticationValues.AddAuthParameter("username", playFabUserId);
        customAuthenticationValues.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);
        PhotonNetwork.AuthValues = customAuthenticationValues;

        OnLogin();
    }

    private void OnLogin()
    {
        UIManager.Instance.ShowNetworkState("로그인에 성공하였습니다.");
        DataManager.Instance.RequestCardDatabase();
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

    private void OnPlayFabError(PlayFabError obj)
    {
        UIManager.Instance.ShowNetworkState("오류가 발생하였습니다.");
        Debug.Log(obj.GenerateErrorReport());
        onLogin(false);
    }
    #endregion
}
