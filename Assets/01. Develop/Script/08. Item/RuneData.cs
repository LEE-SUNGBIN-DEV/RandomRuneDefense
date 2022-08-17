using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class RuneData
{
    [SerializeField] private string runeName;
    [SerializeField] private Sprite runeSprite;
    [SerializeField] private int runeNumber;
    [SerializeField] private RUNE_TYPE runeType;
    [SerializeField] private int runeDamage;
    [SerializeField] private float runeAttackSpeed;

    public void LoadItem(string itemName, string jsonString)
    {
        JObject jObject = JObject.Parse(jsonString);

        runeName = itemName;
        runeSprite = ResourceManager.Instance.RuneResourceDictionary[runeName].itemSprite;

        runeNumber = int.Parse(jObject["runeNumber"].ToString());
        runeType = (RUNE_TYPE)int.Parse(jObject["runeType"].ToString());
        runeDamage = int.Parse(jObject["runeDamage"].ToString());
        runeAttackSpeed = float.Parse(jObject["runeAttackSpeed"].ToString());
    }

    #region Property
    public string RuneName
    {
        get => runeName;
    }
    public Sprite RuneSprite
    {
        get => runeSprite;
        set => runeSprite = value;
    }
    public int RuneNumber
    {
        get => runeNumber;
    }
    public RUNE_TYPE RuneType
    {
        get => runeType;
    }
    public int RuneDamage
    {
        get => runeDamage;
    }
    public float RuneAttackSpeed
    {
        get => runeAttackSpeed;
    }
    #endregion
}
