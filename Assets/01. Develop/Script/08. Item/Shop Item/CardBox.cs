using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBox : ShopItem
{
    public override void PurchaseItem()
    {
        Debug.Log("카드 상자 구매");
    }
}
