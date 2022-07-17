using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class RegisterPanel : Panel
{
    [SerializeField] private TMP_InputField registerIDInputField;
    [SerializeField] private TMP_InputField registerPasswordInputField;
    [SerializeField] private TMP_InputField registerEmailInputField;
    [SerializeField] private TMP_InputField registerNicknameInputField;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button cancelButton;


    #region Property
    public TMP_InputField RegisterIDInputField
    {
        get => registerIDInputField;
    }
    public TMP_InputField RegisterPasswordInputField
    {
        get => registerPasswordInputField;
    }
    public TMP_InputField RegisterEmailInputField
    {
        get => registerEmailInputField;
    }
    public TMP_InputField RegisterNicknameInputField
    {
        get => registerNicknameInputField;
    }
    public Button RegisterButton
    {
        get => registerButton;
    }
    public Button CancelButton
    {
        get => cancelButton;
    }
    #endregion
}
