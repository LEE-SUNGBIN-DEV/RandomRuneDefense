//#define DEBUG_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class TopUserPanel : Panel
{
    [SerializeField] private TextMeshProUGUI topUserNicknameText;
    [SerializeField] private TextMeshProUGUI topUserGoldText;
    [SerializeField] private TextMeshProUGUI topUserCrystalText;
    [SerializeField] private TextMeshProUGUI topUserLevelText;

    private void Awake()
    {
        PlayerStats.onPlayerStatsChanged -= UpdateStatsUI;
        PlayerStats.onPlayerStatsChanged += UpdateStatsUI;
        PlayerCurrency.onPlayerCurrencyChanged -= UpdateCurrencyUI;
        PlayerCurrency.onPlayerCurrencyChanged += UpdateCurrencyUI;
    }

    private void OnEnable()
    {
        UpdateCurrencyUI(DataManager.Instance.PlayerData.PlayerCurrency);
        UpdateStatsUI(DataManager.Instance.PlayerData.PlayerStats);
    }

    private void OnDestroy()
    {
        PlayerStats.onPlayerStatsChanged -= UpdateStatsUI;
        PlayerCurrency.onPlayerCurrencyChanged -= UpdateCurrencyUI;
    }

    public void UpdateCurrencyUI(PlayerCurrency playerCurrency)
    {
#if DEBUG_MODE
        Debug.Log("UpdateCurrencyUI Called");
#endif
        topUserGoldText.text = playerCurrency.Gold.ToString();
        topUserCrystalText.text = playerCurrency.Crystal.ToString();
    }
    public void UpdateStatsUI(PlayerStats playerStats)
    {
#if DEBUG_MODE
        Debug.Log("UpdateStatsUI Called");
#endif
        topUserNicknameText.text = PhotonNetwork.LocalPlayer.NickName;
        topUserLevelText.text = playerStats.Level.ToString();
    }
}
