using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Item<T> where T : Item<T>
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [System.NonSerialized] private Sprite itemSprite;

    public virtual void LoadItem(string jsonString)
    {
        JsonConvert.PopulateObject(jsonString, this);
        itemSprite = ResourceManager.Instance.CardResourceDictionary[ItemName].itemSprite;
    }

    #region Property
    public string ItemName
    {
        get => itemName;
        set => itemName = value;
    }
    public string ItemDescription
    {
        get => itemDescription;
        set => itemDescription = value;
    }
    public Sprite ItemSprite
    {
        get => itemSprite;
        set => itemSprite = value;
    }
    #endregion
}
