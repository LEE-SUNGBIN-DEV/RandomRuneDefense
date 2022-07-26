using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Tile[] tiles;
    [SerializeField] uint currentIndex;
    [SerializeField] int destroyIndex;
    public bool isFull;

    private void Start()
    {
        tiles = GetComponentsInChildren<Tile>();

        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i].owner = this;
        }        
    }

    public void AddRune(Rune rune)
    {
        if (currentIndex == tiles.Length)
        {
            isFull = true;
            CurrentIndex = 0;
            Debug.Log("²ËÃ¡½À´Ï´Ù.");
        }      
        if(destroyIndex == tiles.Length)
        {
            destroyIndex = tiles.Length - 1;
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
        tiles[CurrentIndex].DestroyRune();
    }

    #region Index Property
    public uint CurrentIndex
    {
        get => currentIndex;
        set => currentIndex = value;
    }
    public int DestroyIndex
    {
        get => destroyIndex;
        set => destroyIndex = value;
    }
    #endregion
}
