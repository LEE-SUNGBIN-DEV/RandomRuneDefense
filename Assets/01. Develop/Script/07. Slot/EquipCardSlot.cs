using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCardSlot : CardSlot
{
    public void EquipCard(Card requestCard)
    {
        RegisterCardToSlot(requestCard);
        requestCard.EquipCard();
    }

    public void ReleaseCard()
    {
        if (IsEmpty() == false)
        {
            Card.ReleaseCard();
            ClearSlot();
        }
    }
}
