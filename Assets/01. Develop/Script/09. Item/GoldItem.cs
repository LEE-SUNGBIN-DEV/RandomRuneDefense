using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldItem : Item<GoldItem>
{
    [SerializeField] private uint rewardGold;

    public override void LoadItemInformation(GoldItem loadedItem)
    {

    }
}
