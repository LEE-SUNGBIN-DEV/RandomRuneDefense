using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPanel : Panel
{
    [SerializeField] private float scrollPosition;
    [SerializeField] private float minScrollRange;
    [SerializeField] private float maxScrollRange;

    public bool IsTargetScrollPanel(float scrollValue)
    {
        return (scrollValue >= minScrollRange && scrollValue < maxScrollRange);
    }

    #region Property
    public float ScrollPosition
    { 
        get => scrollPosition;
    }
    public float MinScrollRange
    {
        get => minScrollRange;
    }
    public float MaxScrollRange
    {
        get => maxScrollRange;
    }
    #endregion
}
