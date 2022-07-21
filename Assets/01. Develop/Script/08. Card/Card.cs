using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;

    #region Property
    public string CardName
    {
        get => cardName;
        set => cardName = value;
    }
    public string CardDescription
    {
        get => cardDescription;
        set => cardDescription = value;
    }
    #endregion
}
