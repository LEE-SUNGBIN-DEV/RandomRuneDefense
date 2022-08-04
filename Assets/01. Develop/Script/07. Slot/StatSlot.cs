using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatSlot : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private float statAmount;
    [SerializeField] private Image statGreenFillImage;
    [SerializeField] private Image statYellowFillImage;
    [SerializeField] private Image statRedFillImage;

    private void OnEnable()
    {
        UpdateFillImage();
    }

    public void SetStatSlot(float amount)
    {
        StatAmount = amount;
    }

    private float GetStatRatio()
    {
        return statAmount / Constant.NUMBER_STAT_MAX;
    }

    private void UpdateFillImage()
    {
        float ratio = GetStatRatio();

        if (ratio <= 0.333f)
        {
            statGreenFillImage.enabled = true;
            statYellowFillImage.enabled = false;
            statRedFillImage.enabled = false;

            statGreenFillImage.fillAmount = ratio;
        }
        else if (0.333f < ratio && ratio <= 0.666f)
        {
            statGreenFillImage.enabled = false;
            statYellowFillImage.enabled = true;
            statRedFillImage.enabled = false;

            statYellowFillImage.fillAmount = ratio;
        }
        else
        {
            statGreenFillImage.enabled = false;
            statYellowFillImage.enabled = false;
            statRedFillImage.enabled = true;

            statRedFillImage.fillAmount = ratio;
        }
    }

    #region Property
    public string StatName
    {
        get => statName;
        set => statName = value;
    }
    public float StatAmount
    {
        get => statAmount;
        set
        {
            statAmount = value;
            UpdateFillImage();
        }
    }
    #endregion
}
