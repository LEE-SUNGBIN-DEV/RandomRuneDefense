using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSlotContainer : MonoBehaviour
{
    [SerializeField] private RuneSlot[] runeSlots;
    [SerializeField] private int currentSlotIndex;

    public void RegisterRune(RuneData requestRune)
    {
        if (runeSlots.Length == 0)
        {
            runeSlots = GetComponentsInChildren<RuneSlot>();
            currentSlotIndex = 0;
        }
        runeSlots[currentSlotIndex].RegisterRune(requestRune);
        ++currentSlotIndex;
    }

    public void ClearSlots()
    {
        for(int i=0; i<runeSlots.Length; ++i)
        {
            runeSlots[i].ClearSlot();
        }
        currentSlotIndex = 0;
    }
}
