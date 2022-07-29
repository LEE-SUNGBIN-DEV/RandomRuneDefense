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

    public void AddRune(Rune _rune)
    {
        if (IsEmpty())
        {
            int random = Random.Range(0,Constant.RUNE_RANDOM_MAX_VALUE);
            _rune = RuneManager.Instance.FindRuneFromList(random);
            rune = Instantiate(_rune, transform.position, Quaternion.identity);
            rune.transform.parent = transform;
            owner.CurrentIndex += 1;
            owner.DestroyIndex += 1;
        }
    }

    public void UpgradeRune()
    {
        if (runeLevel == 3) // ·é¸¸·¾ ÀÏ½Ã
        {
            Debug.Log("·é ¸¸·¾");
            runeLevel = 3;

            owner.CurrentIndex += 1;
            owner.DestroyIndex += 1;

            Destroy(rune.gameObject);

            int random = Random.Range(0, Constant.RUNE_RANDOM_MAX_VALUE);
            Rune runes = RuneManager.Instance.FindRuneFromList(random);

            rune = Instantiate(runes, transform.position, Quaternion.identity);
            rune.RuneDamage += runes.RuneDamage + (runeLevel * 10);
            rune.RuneLevelUP(runeLevel);

            rune.transform.parent = transform;

            return;
        }
        else
        {
            runeLevel += 1;
            Debug.Log("¾÷±×·¹ÀÌµå È£Ãâ");
            owner.CurrentIndex += 1;
            owner.DestroyIndex += 1;

            Destroy(rune.gameObject);

            int random = Random.Range(0, Constant.RUNE_RANDOM_MAX_VALUE);
            Rune runes = RuneManager.Instance.FindRuneFromList(random);

            rune = Instantiate(runes, transform.position, Quaternion.identity);
            rune.RuneDamage = runes.RuneDamage + (runeLevel * 10);
            rune.RuneLevelUP(runeLevel);

            rune.transform.parent = transform;
        }
    }

    public void DestroyRune()
    {
        runeLevel = 0;
        Destroy(rune.gameObject);       
    }
}
