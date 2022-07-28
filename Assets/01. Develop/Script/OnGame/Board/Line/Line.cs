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
    public GameObject LineUpgradeEffect;
    [SerializeField] bool lineEffectOn;

    private void Start()
    {
        lineEffectOn = true;
        tiles = GetComponentsInChildren<Tile>();

        for(int i = 0; i < tiles.Length; i++)
        {
            tiles[i].owner = this;
        }        
    }
    private void Update()
    {
        if(DestroyIndex == tiles.Length)
        {
              isFull = true;
              
              if(LineEffect.activeInHierarchy && !lineEffectOn) // 라인 강화 퀘스트가 Hierarchy 에서 true 라면  
              {                  
                  Debug.Log("라인 강화 퀘스트 중인데 꽉참");
                  for (int i = 0; i < tiles.Length; i++)
                  {                   
                     tiles[i].rune.RuneDamage += 10;
                    //tiles[i].rune.RuneDamage + 10;
                  }
              
                  lineEffectOn = true;
                  LineUpgradeEffect.SetActive(true); // 강화 라인이고 그 라인이 업그레이드 중이라면
              }
              if(!LineEffect.activeInHierarchy && lineEffectOn) // 라인 강화 퀘스트가 Hierarchy 에서 false 라면
              {                  
                  Debug.Log("라인 강화 퀘스트 아닌데 꽉참");
                  for (int i = 0; i < tiles.Length; i++)
                  {
                    Debug.Log(tiles[i].name);
                    tiles[i].rune.RuneDamage -= 10;
                  }

                  lineEffectOn = false;
                  LineUpgradeEffect.SetActive(false);
              }           
        }
        else
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
            Debug.Log("꽉찼습니다.");
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
