using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class ShopItem : Item<ShopItem>
{
    [SerializeField] private string currencyType;
    [SerializeField] private int itemPrice;

    public virtual void PurchaseItem(){ }
    public override void LoadItem(string jsonString)
    {
        base.LoadItem(jsonString);
        ItemSprite = ResourceManager.Instance.ShopResourceDictionary[ItemName].itemSprite;

        JObject jObject = JObject.Parse(jsonString);
        currencyType = jObject["currencyType"].ToString();
        itemPrice = int.Parse(jObject["itemPrice"].ToString());
    }
    #region Property
    public string CurrencyType
    {
        get => currencyType;
        set => currencyType = value;
    }
    public int ItemPrice
    {
        get => itemPrice;
        set => itemPrice = value;
    }
    #endregion
}
