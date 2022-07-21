using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item<T> : MonoBehaviour where T : Item<T>
{
    [SerializeField] private string itemName;
    [SerializeField] private uint itemPrice;
    [SerializeField] private Image itemImage;

    public abstract void LoadItemInformation(T loadedItem);

    #region Property
    public string ItemName
    {
        get => itemName;
        set => itemName = value;
    }
    public uint ItemPrice
    {
        get => itemPrice;
        set => itemPrice = value;
    }
    public Image ItemImage
    {
        get => itemImage;
        set => itemImage = value;
    }
    #endregion
}
