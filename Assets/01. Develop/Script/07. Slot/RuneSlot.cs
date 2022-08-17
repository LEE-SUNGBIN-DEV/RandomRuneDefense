using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneSlot : Slot
{
    [SerializeField] private string runeName;
    [SerializeField] private RuneData runeData;

    public void RegisterRune(RuneData requestRune)
    {
        runeData = requestRune;
        SlotImage.sprite = runeData.RuneSprite;
        SlotImage.color = Function.SetAlpha(SlotImage.color, 1f);
    }

    public override void ClearSlot()
    {
        runeData = null;
        SlotImage.sprite = null;
        SlotImage.color = Function.SetAlpha(SlotImage.color, 0f);
    }

    #region Property
    public string RuneName
    {
        get => runeName;
    }
    public RuneData RuneData
    {
        get => runeData;
    }
    #endregion
}
