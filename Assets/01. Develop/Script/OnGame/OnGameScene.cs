using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class OnGameScene : MonoBehaviour
{
    public static OnGameScene Inst { get; private set; }
    private void Awake() => Inst = this;

    public GameObject[] HeartImages;
    [SerializeField] TMP_Text total_SP_TMP;
    [SerializeField] TMP_Text spawn_SP_TMP;
    int totalSp;
    int spawnSP;

    #region 프로퍼티
    public int TotalSP
    {
        get => totalSp;
        set
        {
            totalSp = value;
            total_SP_TMP.text = value.ToString();           
        }
    }
    public int SpawnSP
    {
        get => spawnSP;
        set
        {
            spawnSP = value;
            spawn_SP_TMP.text = value.ToString();          
        }
    }
    #endregion

    void Start()
    {
        GameStart();
    }

    private void Update()
    { 
        // hp 테스트
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < HeartImages.Length; i++)
            {
                HeartImages[i].SetActive(true);
            }         
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TotalSP -= SpawnSP;
        }

    }

    public void DecreaseHeart()
    {

    }

    public void GameStart()
    {
        TotalSP = 10;
        SpawnSP = 10;
    }
       
}
