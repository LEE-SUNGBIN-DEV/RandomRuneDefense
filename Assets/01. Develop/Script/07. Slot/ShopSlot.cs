using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : Slot
{
    [SerializeField] private ShopItem shopItem;
    [SerializeField] private TextMeshProUGUI shopItemNameText;
    [SerializeField] private TextMeshProUGUI shopItemPriceText;
    [SerializeField] private Image shopItemImage;

    public void RegisterItemToSlot(ShopItem requestItem)
    {
        shopItem = requestItem;
        shopItemNameText.text = shopItem.ItemName;
        shopItemPriceText.text = shopItem.ItemPrice.ToString();
        shopItemImage.sprite = requestItem.ItemSprite;

        shopItemImage.color = Function.SetAlpha(shopItemImage.color, 1f);
    }

    public override void ClearSlot()
    {
        shopItem = null; 
        shopItemNameText.text = null;
        shopItemPriceText.text = null;
        shopItemImage.sprite = null;

        shopItemImage.color = Function.SetAlpha(shopItemImage.color, 0f);
    }

    public void OnClickShopSlot()
    {
        shopItem?.PurchaseItem();
    }
}
