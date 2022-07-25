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

    [SerializeField] List<string> collectedCardNames;

    public void OnLoadPlayerCollections()
    {
    }

    #region Property
    public List<string> CollectedCardNames
    {
        get => collectedCardNames;
        set
        {
            collectedCardNames = value;
            if (onPlayerCollectionsChanged != null)
            {
                onPlayerCollectionsChanged(this);
            }
        }
    }
    #endregion
}
