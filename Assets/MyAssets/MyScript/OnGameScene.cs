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
    public int Total_SP
    {
        get => totalSp;
        set
        {
            total_SP_TMP.text = value.ToString();
            totalSp = value;
        }
    }
    public int SpawnSP
    {
        get => spawnSP;
        set
        {
            spawn_SP_TMP.text = value.ToString();
            totalSp = value;
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
        if (sceneName == Constant.NAME_GAME_SCENE)
        {
            PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
        }
    }

    public void DecreaseHeart()
    {

    }

    public void GameStart()
    {
        Total_SP = 100;
        SpawnSP = 10;
    }
       
}
