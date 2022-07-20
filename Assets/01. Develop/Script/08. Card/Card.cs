using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;

    private bool isActive;

    private void Awake()
    {
        isActive = false;
    }
}
