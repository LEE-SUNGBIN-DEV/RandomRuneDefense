using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopPanel : ScrollPanel
{
    #region Events
    public static event UnityAction<ShopPanel> onShopChanged;
    #endregion
    [SerializeField] private ShopSlot[] goldShopSlots;
    [SerializeField] private ShopSlot[] crystalShopSlots;
    [SerializeField] private ContentSizeFitter[] sizeFitters;

    private void Awake()
    {
        sizeFitters = GetComponentsInChildren<ContentSizeFitter>();
        DataManager.onLoadDatabase -= RefreshShop;
        DataManager.onLoadDatabase += RefreshShop;
    }

    private void OnEnable()
    {
        RefreshShop();
    }

    private void OnDisable()
    {
        for (int i = 0; i < goldShopSlots.Length; ++i)
        {
            goldShopSlots[i].ClearSlot();
            goldShopSlots[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < crystalShopSlots.Length; ++i)
        {
            crystalShopSlots[i].ClearSlot();
            crystalShopSlots[i].gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        DataManager.onLoadDatabase -= RefreshShop;
    }

    public void RefreshShop()
    {
        var goldShopDatabase = DataManager.Instance.GoldShopDatabase;
        var crystalShopDatabase = DataManager.Instance.CrystalShopDatabase;

        for (int i = 0; i < goldShopDatabase.Count; ++i)
        {
            if (goldShopDatabase[i] != null)
            {
                goldShopSlots[i].RegisterItemToSlot(goldShopDatabase[i]);
                goldShopSlots[i].gameObject.SetActive(true);
            }

            else
            {
                goldShopSlots[i].ClearSlot();
                goldShopSlots[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < crystalShopDatabase.Count; ++i)
        {
            if (crystalShopDatabase[i] != null)
            {
                crystalShopSlots[i].RegisterItemToSlot(crystalShopDatabase[i]);
                crystalShopSlots[i].gameObject.SetActive(true);
            }

            else
            {
                crystalShopSlots[i].ClearSlot();
                crystalShopSlots[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < sizeFitters.Length; ++i)
        {
            sizeFitters[i].enabled = false;
            sizeFitters[i].enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sizeFitters[i].transform);
        }

        onShopChanged?.Invoke(this);
    }
}
