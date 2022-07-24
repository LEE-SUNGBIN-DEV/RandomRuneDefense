using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Resource", fileName = "Item Resource")]
public class ItemResource : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
}
