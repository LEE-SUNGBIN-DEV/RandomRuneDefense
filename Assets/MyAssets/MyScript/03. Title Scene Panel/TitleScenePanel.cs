using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class TitleScenePanel : Panel
{
    #region Events
    public static event UnityAction<string, string> onRequestLogin;
    public static event UnityAction<string, string, string, string> onRequestRegister;
    #endregion

    [Header("Panels in Title Scene")]
    [SerializeField] private Panel[] titleScenePanels;

    [Header("Login Panel")]
    [SerializeField] private LoginPanel loginPanel;

    [Header("Register Panel")]
    [SerializeField] private RegisterPanel registerPanel;

    [Header("Login Result")]
    [SerializeField] private bool isLoginSuccess;

    private void Awake()
    {
        NetworkManager.onConnect -= OnConnectNetwork;
        NetworkManager.onConnect += OnConnectNetwork;
        NetworkManager.onLogin -= SetIsLoginSuccess;
        NetworkManager.onLogin += SetIsLoginSuccess;
        NetworkManager.onRegister -= SetIsRegisterSuccess;
        NetworkManager.onRegister += SetIsRegisterSuccess;
    }

    private void OnEnable()
    {
        Function.SetPanelsActivation(titleScenePanels, false);
        Function.OpenPanel(loginPanel);
    }

    private void OnDisable()
    {
        Function.SetPanelsActivation(titleScenePanels, false);
    }

    private void OnDestroy()
    {
        NetworkManager.onLogin -= SetIsLoginSuccess;
        NetworkManager.onRegister -= SetIsRegisterSuccess;
    }

    public void SetLoginButtonInteraction(bool isInteractable)
    {
        loginPanel.LoginButton.interactable = isInteractable;
        loginPanel.SignUpButton.interactable = isInteractable;
    }
    public void SetRegisterButtonInteraction(bool isInteractable)
    {
        registerPanel.RegisterButton.interactable = isInteractable;
        registerPanel.CancelButton.interactable = isInteractable;
    }

    public void OnConnectNetwork(bool isConnect)
    {
        SetLoginButtonInteraction(true);
    }

    public void OnSignUpButtonClicked()
    {
        Function.ClosePanel(loginPanel);
        Function.OpenPanel(registerPanel);
    }
    public void OnCancelButtonClicked()
    {
        Function.ClosePanel(registerPanel);
        Function.OpenPanel(loginPanel);
    }

    public void SetIsLoginSuccess(bool isSuccess)
    {
        if (!isSuccess)
        {
            SetLoginButtonInteraction(true);
        }
    }
    public void SetIsRegisterSuccess(bool isSuccess)
    {
        SetRegisterButtonInteraction(true);
    }

    public void OnLoginButtonClicked()
    {
        SetLoginButtonInteraction(false);
        onRequestLogin(loginPanel.LoginIDInputField.text.ToString(), loginPanel.LoginPasswordInputField.text.ToString());
    }

    public void OnRegisterButtonClicked()
    {
        SetRegisterButtonInteraction(false);

        onRequestRegister(
            registerPanel.RegisterIDInputField.text.ToString(),
            registerPanel.RegisterPasswordInputField.text.ToString(),
            registerPanel.RegisterEmailInputField.text.ToString(),
            registerPanel.RegisterNicknameInputField.text.ToString());
    }

    #region Property
    public bool IsLoginSuccess
    {
        get => isLoginSuccess;
        set => isLoginSuccess = value;
    }
    #endregion
}
