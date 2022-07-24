using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card : Item<Card>
{
    public override void LoadItem(string jsonString)
    {
        base.LoadItem(jsonString);
    }

    public void EquipCard()
    {

    }

    public void ReleaseCard()
    {

    }
}
