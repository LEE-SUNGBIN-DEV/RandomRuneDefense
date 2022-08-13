using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketBox : ShopItem
{
    public override void PurchaseItem()
    {
        Debug.Log("입장권 상자 구매");
    }
}
