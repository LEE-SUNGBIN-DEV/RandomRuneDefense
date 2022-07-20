using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] backRenderers;
    [SerializeField] Canvas[] middleRenderers;
    [SerializeField] string sortingLayerName;
    int originOrder;

    public void SetOrder(int order)
    {
        int mulOrder = order * 10;

        foreach(var renderer in backRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
        }

        foreach ( var renderer in middleRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder + 1;
        }
    }

    public void SetOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        
    }
}
