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
    private IEnumerator loginCoroutine;

    #region Playfab API
    #region Login
    // 로그인 로직
    public void LoginWithPlayFab(string userName, string userPassword)
    {
        UIManager.Instance.ShowNetworkState("로그인 정보를 확인합니다.");

        var request = new LoginWithPlayFabRequest { Username = userName, Password = userPassword };
        PlayFabClientAPI.LoginWithPlayFab(request, RequestToken, OnLoginError);
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
        }, AuthenticateWithPhoton, OnLoginError);
    }

    // Step 3
    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        Debug.Log("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

        AuthenticationValues customAuthenticationValues = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        customAuthenticationValues.AddAuthParameter("username", playFabUserId);
        customAuthenticationValues.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);
        PhotonNetwork.AuthValues = customAuthenticationValues;

        loginCoroutine = OnLogin();
        StartCoroutine(loginCoroutine);
    }

    private IEnumerator OnLogin()
    {
        UIManager.Instance.ShowNetworkState("로그인에 성공하였습니다.");
        //yield return Function.WaitPlayFabAPI(DataManager.Instance.SetPlayerData);
        yield return DataManager.Instance.RequestGameDatabase();
        yield return DataManager.Instance.RequestPlayerDatabase();

        onLogin(true);
        Connect();
    }
    private void OnLoginError(PlayFabError error)
    {
        if (loginCoroutine != null)
        {
            StopCoroutine(loginCoroutine);
        }
        UIManager.Instance.ShowNetworkState("로그인에 실패하였습니다.");
        Debug.LogWarning(error.GenerateErrorReport());
        onLogin(false);
    }
    #endregion
    #region Register
    // 가입 로직
    public void Register(string userID, string userPassword, string userEmail, string nickname)
    {
        var request = new RegisterPlayFabUserRequest { Username = userID, Password = userPassword, Email = userEmail, DisplayName = nickname };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, OnRegisterError);
    }
    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        UIManager.Instance.DoSystemNotice("회원가입에 성공하였습니다.");
        onRegister(true);
    }

    private void OnRegisterError(PlayFabError error)
    {
        UIManager.Instance.DoSystemNotice("회원가입에 실패하였습니다.");
        Debug.LogWarning(error.GenerateErrorReport());
        onRegister(false);
    }
    #endregion
    #endregion
}
