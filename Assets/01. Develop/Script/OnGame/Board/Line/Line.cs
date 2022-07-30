using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Tile[] tiles;
    [SerializeField] uint currentIndex;
    [SerializeField] uint destroyIndex;
    public bool isFull;


    [SerializeField] int[] originRuneDamage;

    public GameObject LineEffect;
    public GameObject LineUpgradeEffect;
    [SerializeField] bool lineEffectOn;

    private void Start()
    {
        lineEffectOn = false;
        tiles = GetComponentsInChildren<Tile>();
        originRuneDamage = new int[tiles.Length]; // 기본 데미지 값 저장

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].owner = this;
        }        
    }
    private void LateUpdate()
    {
        if(DestroyIndex == tiles.Length)
        {
              isFull = true;

            for (int i = 0; i < tiles.Length; i++) // 룬에 원래 데미지를 확인해서 넣어준다.
            {
                if (tiles[i].rune != null)
                {
                    originRuneDamage[i] = tiles[i].rune.RuneDamage;
                }
            }

            if (LineEffect.activeInHierarchy && !lineEffectOn) // 라인 강화 퀘스트가 Hierarchy 에서 true 라면  
              {                 
                  Debug.Log("라인 강화 퀘스트 중인데 꽉참");
                  for (int i = 0; i < tiles.Length; i++)
                  {
                    tiles[i].rune.RuneDamage = originRuneDamage[i] + 20;                    
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
                    tiles[i].rune.RuneDamage = originRuneDamage[i] - 20;
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

        //for (int i = 0; i < tiles.Length; i++) // 룬에 원래 데미지를 확인해서 넣어준다.
        //{
        //    if (tiles[i].rune != null)
        //    {
        //        originRuneDamage[i] = tiles[i].rune.RuneDamage;
        //    }
        //}
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
