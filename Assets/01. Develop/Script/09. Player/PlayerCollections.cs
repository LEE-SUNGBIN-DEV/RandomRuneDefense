using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerCollections
{
    #region Event
    public static event UnityAction<PlayerCollections> onPlayerCollectionsChanged;
    #endregion

    [SerializeField] private Card[] collectedCards;

    public void OnLoadPlayerCollections()
    {
    }

    #region Property
    public Card[] CollectedCards
    {
        get => collectedCards;
        set
        {
            collectedCards = value;
            if (onPlayerCollectionsChanged != null)
            {
                onPlayerCollectionsChanged(this);
            }
        }
    }
    #endregion
}
