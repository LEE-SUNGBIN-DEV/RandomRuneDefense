using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static partial class Function
{
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

    public static void SetAllPanelActivation(Panel[] panels, bool isActive)
    {
        for (int i = 0; i < panels.Length; ++i)
        {
            panels[i].gameObject.SetActive(isActive);
        }
    }

    public static Color SetAlpha(Color color, float alphaValue)
    {
        Color newColor = color;
        newColor.a = alphaValue;
        return newColor;
    }
}
