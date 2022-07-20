using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class LoginPanel : Panel
{
    [SerializeField] private TMP_InputField loginIDInputField;
    [SerializeField] private TMP_InputField loginPasswordInputField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signUpButton;

    #region Property
    public TMP_InputField LoginIDInputField
    {
        get => loginIDInputField;
    }
    public TMP_InputField LoginPasswordInputField
    {
        get => loginPasswordInputField;
    }
    public Button LoginButton
    {
        get => loginButton;
    }
    public Button SignUpButton
    {
        get => signUpButton;
    }
    #endregion
}
