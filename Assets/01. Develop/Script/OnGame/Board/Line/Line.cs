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
              
              if(LineEffect.activeInHierarchy && !lineEffectOn) // ���� ��ȭ ����Ʈ�� Hierarchy ���� true ���  
              {                  
                  Debug.Log("���� ��ȭ ����Ʈ ���ε� ����");
                  for (int i = 0; i < tiles.Length; i++)
                  {                   
                     tiles[i].rune.RuneDamage += 10;
                    //tiles[i].rune.RuneDamage + 10;
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
