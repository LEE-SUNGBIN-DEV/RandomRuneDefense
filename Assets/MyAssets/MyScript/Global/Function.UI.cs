using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static partial class Function
{
    //! 함수를 지연 호출한다.
    public static IEnumerator DoLateCall(UnityAction<object[]> callback, float delay, object[] parameters)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke(parameters);
    }

    public static void OpenPanel(Panel targetPanel)
    {
        targetPanel.gameObject.SetActive(true);
    }
    public static void OpenPanel(Panel targetPanel, out Panel currentPanel)
    {
        OpenPanel(targetPanel);
        currentPanel = targetPanel;
    }

    public static void ClosePanel(Panel targetPanel)
    {
        targetPanel.gameObject.SetActive(false);
    }
    public static void ClosePanel(Panel targetPanel, out Panel currentPanel)
    {
        ClosePanel(targetPanel);
        currentPanel = null;
    }

    public static void SetPanelsActivation(Panel[] panels, bool isActive)
    {
        for (int i = 0; i < panels.Length; ++i)
        {
            panels[i].gameObject.SetActive(isActive);
        }
    }
}
