using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RunePanel : ScrollPanel
{
    [SerializeField] private List<RuneData> runeDatabase;

    [Header("Rune Slot Container")]
    [SerializeField] private RuneSlotContainer fireRuneSlotContainer;
    [SerializeField] private RuneSlotContainer iceRuneSlotContainer;
    [SerializeField] private RuneSlotContainer windRuneSlotContainer;
    [SerializeField] private RuneSlotContainer lightningRuneSlotContainer;
    [SerializeField] private RuneSlotContainer poisonRuneSlotContainer;

    [SerializeField] private ContentSizeFitter[] sizeFitters;

    private void Awake()
    {
        sizeFitters = GetComponentsInChildren<ContentSizeFitter>();
        runeDatabase = DataManager.Instance.RuneDatabase;

        DataManager.onLoadCardDatabase -= RefreshRunes;
        DataManager.onLoadCardDatabase += RefreshRunes;
    }

    private void OnEnable()
    {
        RefreshRunes();
    }

    private void OnDestroy()
    {
        DataManager.onLoadCardDatabase -= RefreshRunes;
    }

    public void RefreshRunes()
    {
        ClearRuneContainers();
        for (int i = 0; i < runeDatabase.Count; ++i)
        {
            switch (runeDatabase[i].RuneType)
            {
                case RUNE_TYPE.FIRE:
                    {
                        fireRuneSlotContainer.RegisterRune(runeDatabase[i]);
                        break;
                    }
                case RUNE_TYPE.ICE:
                    {
                        iceRuneSlotContainer.RegisterRune(runeDatabase[i]);
                        break;
                    }
                case RUNE_TYPE.WIND:
                    {
                        windRuneSlotContainer.RegisterRune(runeDatabase[i]);
                        break;
                    }
                case RUNE_TYPE.LIGHTNING:
                    {
                        lightningRuneSlotContainer.RegisterRune(runeDatabase[i]);
                        break;
                    }
                case RUNE_TYPE.POISON:
                    {
                        poisonRuneSlotContainer.RegisterRune(runeDatabase[i]);
                        break;
                    }
            }
        }

        for (int i = 0; i < sizeFitters.Length; ++i)
        {
            sizeFitters[i].enabled = false;
            sizeFitters[i].enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sizeFitters[i].transform);
        }
    }

    public void ClearRuneContainers()
    {
        fireRuneSlotContainer.ClearSlots();
        iceRuneSlotContainer.ClearSlots();
        windRuneSlotContainer.ClearSlots();
        lightningRuneSlotContainer.ClearSlots();
        poisonRuneSlotContainer.ClearSlots();
    }
}
