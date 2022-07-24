using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 centerPosition;
    public Rune rune;
    public Line owner;
    public int runeLevel;
    private void Awake()
    {
        rune = null;
        owner = null;
    }

    public bool IsEmpty()
    {
        if (rune == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddTower(Rune _rune)
    {
        if (IsEmpty())
        {
            int random = Random.Range(0,Constant.RUNE_RANDOM_MAX_VALUE);
            _rune = RuneManager.Instance.FindRuneFromList(random);
            rune = Instantiate(_rune, transform.position, Quaternion.identity);
            rune.transform.parent = transform;
            owner.CurrentIndex += 1;
        }
    }

    public void UpgradeTower()
    {
        runeLevel += 1;
        Debug.Log("업그레이드 호출");
        owner.CurrentIndex += 1;
        
        Destroy(rune.gameObject);

        int random = Random.Range(0, Constant.RUNE_RANDOM_MAX_VALUE);
        var runes = RuneManager.Instance.FindRuneFromList(random);
        rune = Instantiate(runes, transform.position, Quaternion.identity);
        rune.RuneLevelUP(runeLevel);
        rune.RuneDamage = rune.RuneDamage + (runeLevel * 10);
        rune.transform.parent = transform;       
    }
}
