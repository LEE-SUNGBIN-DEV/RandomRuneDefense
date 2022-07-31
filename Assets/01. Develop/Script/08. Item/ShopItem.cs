using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem : Item<ShopItem>
{
    [SerializeField] private int itemPrice;

    public abstract void PurchaseItem();
}
