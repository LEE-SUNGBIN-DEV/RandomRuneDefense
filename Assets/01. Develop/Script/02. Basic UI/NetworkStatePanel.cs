using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NetworkStatePanel : Panel
{
    [SerializeField] private TextMeshProUGUI networkInformationText = null;
    private IEnumerator showNetworkInformationCoroutine;

    private void OnDisable()
    {
        networkInformationText.text = null;
    }

    public void ShowNetworkStateMessage(string textContent)
    {
        if (showNetworkInformationCoroutine != null)
        {
            StopCoroutine(showNetworkInformationCoroutine);
        }
        showNetworkInformationCoroutine = FadeOutNetworkMessage(textContent);
        StartCoroutine(showNetworkInformationCoroutine);
    }

    public IEnumerator FadeOutNetworkMessage(string textContent)
    {
        networkInformationText.text = textContent;
        Color color = networkInformationText.color;
        color.a = Constant.COLOR_ALPHA_OPACITY;
        networkInformationText.color = color;

        float noticeTime = 0f;
        while (noticeTime < Constant.TIME_NETWORK_NOTICE)
        {
            noticeTime += Time.deltaTime;
            color.a = Constant.COLOR_ALPHA_OPACITY - (noticeTime / Constant.TIME_NETWORK_NOTICE);
            networkInformationText.color = color;
            yield return null;
        }

        color.a = Constant.COLOR_ALPHA_TRANSLUCENT;
        networkInformationText.color = color;
        Function.ClosePanel(this);
    }
}
