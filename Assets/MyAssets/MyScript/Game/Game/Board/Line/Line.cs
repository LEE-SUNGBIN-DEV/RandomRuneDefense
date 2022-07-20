using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] Tile[] tiles;
    [SerializeField] uint currentIndex;
    public bool isFull;

    private void Start()
    {
        tiles = GetComponentsInChildren<Tile>();

        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i].owner = this;
        }        
    }

    public void AddTower(Rune rune)
    {
        if (currentIndex == tiles.Length)
        {
            isFull = true;
            CurrentIndex = 0;
            Debug.Log("²ËÃ¡½À´Ï´Ù.");
        }

        if (isFull)
        {
            tiles[CurrentIndex].UpgradeTower();           
        }
        else
        {
            tiles[CurrentIndex].AddTower(rune);         
        }
    }

    public uint CurrentIndex
    {
        get => currentIndex;
        set => currentIndex = value;
    }
}
