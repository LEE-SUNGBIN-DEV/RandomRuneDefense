using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInformationPanel : Panel
{
    [SerializeField] private Card card;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardDescriptionText;
    [SerializeField] private TextMeshProUGUI cardEffectText;

    public void OnClickCloseButton()
    {
        cardNameText.text = null;
        cardDescriptionText.text = null;
        cardEffectText.text = null;
        gameObject.SetActive(false);
    }

    public void OnClickEquipButton()
    {  
    }
}
