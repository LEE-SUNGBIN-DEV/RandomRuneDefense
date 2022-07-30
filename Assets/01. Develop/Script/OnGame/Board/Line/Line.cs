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
        originRuneDamage = new int[tiles.Length]; // �⺻ ������ �� ����

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

            for (int i = 0; i < tiles.Length; i++) // �鿡 ���� �������� Ȯ���ؼ� �־��ش�.
            {
                if (tiles[i].rune != null)
                {
                    originRuneDamage[i] = tiles[i].rune.RuneDamage;
                }
            }

            if (LineEffect.activeInHierarchy && !lineEffectOn) // ���� ��ȭ ����Ʈ�� Hierarchy ���� true ���  
              {                 
                  Debug.Log("���� ��ȭ ����Ʈ ���ε� ����");
                  for (int i = 0; i < tiles.Length; i++)
                  {
                    tiles[i].rune.RuneDamage = originRuneDamage[i] + 20;                    
                  }
              
                  lineEffectOn = true;
                  LineUpgradeEffect.SetActive(true); // ��ȭ �����̰� �� ������ ���׷��̵� ���̶��
              }
              if(!LineEffect.activeInHierarchy && lineEffectOn) // ���� ��ȭ ����Ʈ�� Hierarchy ���� false ���
              {                  
                  Debug.Log("���� ��ȭ ����Ʈ �ƴѵ� ����");
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
            Debug.Log("��á���ϴ�.");
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

        //for (int i = 0; i < tiles.Length; i++) // �鿡 ���� �������� Ȯ���ؼ� �־��ش�.
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
