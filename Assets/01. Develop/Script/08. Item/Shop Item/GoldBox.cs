using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBox : ShopItem
{
    public override void PurchaseItem()
    {
        Debug.Log("골드 상자 구매");
    }
}
