using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Slot : MonoBehaviour
{
    [SerializeField] private Image slotImage;

    public abstract void ClearSlot();

    #region Property
    public Image SlotImage
    {
        get => slotImage;
    }
    #endregion
}
