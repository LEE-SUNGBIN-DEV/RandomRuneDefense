using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerInventory
{
    #region Event
    public static event UnityAction<PlayerInventory> onPlayerEquipCardChanged;
    public static event UnityAction<PlayerInventory> onLoadPlayerInventory;
    #endregion

    [SerializeField] private string equipCardName;
    [SerializeField] private List<string> itemNames;
    [SerializeField] private List<int> itemAmounts;

    public void GetPlayerInventory()
    {
        DataManager.Instance.GetPlayerInventory();
    }

    public void OnLoadPlayerInventory()
    {
        EquipCardName = equipCardName;
        ItemNames = itemNames;
        ItemAmounts = itemAmounts;
        if(onLoadPlayerInventory != null)
        {
            onLoadPlayerInventory(this);
        }
    }

    public void ClearPlayerInventory()
    {
        equipCardName = null;
        itemNames.Clear();
        itemAmounts.Clear();
    }

    #region Property
    public string EquipCardName
    {
        get => equipCardName;
        set
        {
            equipCardName = value;
            if (onPlayerEquipCardChanged != null)
            {
                onPlayerEquipCardChanged(this);
            }
        }
    }
    public List<string> ItemNames
    {
        get => itemNames;
        set
        {
            itemNames = value;
        }
    }
    public List<int> ItemAmounts
    {
        get => itemAmounts;
        set
        {
            itemAmounts = value;
        }
    }
    #endregion
}
