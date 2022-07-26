using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderEffect : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider sliderEffect;

    private void Update()
    {
        transform.position = new Vector3(-1.4f + (sliderEffect.value * 2.85f), 
                                         -2.4f, -5);
    }
}
