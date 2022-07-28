using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Tile[] tiles;
    [SerializeField] uint currentIndex;
    [SerializeField] uint destroyIndex;
    public bool isFull;

    public GameObject LineEffect;

    private void Start()
    {
        tiles = GetComponentsInChildren<Tile>();

        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i].owner = this;
        }        
    }
    private void Update()
    {
        if (currentIndex == tiles.Length)
        {
            isFull = true;
            if(LineEffect.activeSelf)// ¶óÀÎ °­È­ Äù½ºÆ®°¡ true ¶ó¸é  
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].rune.RuneDamage = tiles[i].rune.RuneDamage + 10;
                }
            }
            else
            {
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].rune.RuneDamage = tiles[i].rune.RuneDamage;
                }
            }
        }
        if (DestroyIndex != tiles.Length)
        {
            isFull = false;
            //LineEffect.SetActive(false);
        }
    }

    public void AddRune(Rune rune)
    {
        if (CurrentIndex == tiles.Length)
        {           
            CurrentIndex = 0;
            Debug.Log("²ËÃ¡½À´Ï´Ù.");
        }
        if(DestroyIndex != tiles.Length)
        {            
            CurrentIndex = DestroyIndex;
        }
        if(DestroyIndex == tiles.Length)
        {
            DestroyIndex = (uint)tiles.Length - 1;
        }

        if (isFull)
        {
            tiles[CurrentIndex].UpgradeRune();           
        }
        else
        {            
            tiles[CurrentIndex].AddRune(rune);         
        }
    }

    public void DestroyRune()
    {
        CurrentIndex -= 1;
        DestroyIndex -= 1;
        tiles[DestroyIndex].DestroyRune();
    }

    #region Index Property
    public uint CurrentIndex
    {
        get => currentIndex;
        set => currentIndex = value;
    }
    public uint DestroyIndex
    {
        get => destroyIndex;
        set => destroyIndex = value;
    }
    #endregion
}
