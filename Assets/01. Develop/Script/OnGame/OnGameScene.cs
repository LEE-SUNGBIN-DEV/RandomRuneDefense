using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class OnGameScene : MonoBehaviour
{
    public static OnGameScene Inst { get; private set; }
    private void Awake() => Inst = this;

    [SerializeField] GameObject[] HeartImages;
    [SerializeField] TMP_Text total_SP_TMP; // 글씨 표시
    [SerializeField] TMP_Text spawn_SP_TMP; // 글자 표시
    int totalSp; // 전체 sp
    int spawnSP; // 스폰 sp

    bool isDie; // 죽음

    //-----------CAMERA SHAKE--------------//
    [Header("※ CAMERA_SHAKE")]
    [SerializeField] float shakeTime;
    [SerializeField] float shakeIntensity;

    #region 프로퍼티
    public int TotalSP
    {
        get => totalSp;
        set
        {
            if(totalSp < 0)
            {
                totalSp = 0;
            }
            else
            {
                totalSp = value;
            }            
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

    private void OnEnable()
    {
        GameManager.onSceneLoaded -= CreatePlayer;
        GameManager.onSceneLoaded += CreatePlayer;
    }

    private void OnDisable()
    {
        GameManager.onSceneLoaded -= CreatePlayer;
    }

    private void CreatePlayer(string sceneName)
    {
        if (sceneName == Constant.NAME_SCENE_GAME)
        {
            PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
        }
    }

    public void DecreaseHeart()
    {
        if(isDie)
        {
            return;
        }

        for (int i = 0; i < HeartImages.Length; i++)
        {
            if(HeartImages[i].activeSelf) // 켜있으면 하나를 종료하고 브레이크
            {
                HeartImages[i].SetActive(false);
                StartCoroutine(Camera.main.ShakeCamera(shakeTime, shakeIntensity));
                break;
            }
        }

        if (System.Array.TrueForAll(HeartImages, x => x.activeSelf == false))
        {
            isDie = true;
            print("게임 오바");
        }
    }

    public void GameStart()
    {
        TotalSP = 100;
        SpawnSP = 10;
    }
       
}
