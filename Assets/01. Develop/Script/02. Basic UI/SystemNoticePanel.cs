using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemNoticePanel : Panel
{
    [SerializeField] private TextMeshProUGUI systemNoticeContent;
    [SerializeField] private Button confirmButton;

    public void OnClickConfirmButton()
    {
        systemNoticeContent.text = null;
        gameObject.SetActive(false);
    }

    #region Property
    public TextMeshProUGUI SystemNoticeContent
    {
        get => systemNoticeContent;
    }
    #endregion
}
