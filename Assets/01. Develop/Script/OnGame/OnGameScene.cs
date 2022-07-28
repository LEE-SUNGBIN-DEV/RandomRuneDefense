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
    [SerializeField] TMP_Text total_SP_TMP; // �۾� ǥ��
    [SerializeField] TMP_Text spawn_SP_TMP; // ���� ǥ��

    EnemyObjectPool enemyObjectPool;
    public int LineEffectValue;

    int totalSp; // ��ü sp
    int spawnSP; // ���� sp

    bool isDie; // ����

    //-----------CAMERA SHAKE--------------//
    [Header("�� CAMERA_SHAKE")]
    [SerializeField] float shakeTime;
    [SerializeField] float shakeIntensity;

    #region ������Ƽ
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
        enemyObjectPool = EnemyObjectPool.Instance;
        GameStartSp();
    }

    private void Update()
    {
        StageStart();
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
            if(HeartImages[i].activeSelf) // �������� �ϳ��� �����ϰ� �극��ũ
            {
                HeartImages[i].SetActive(false);
                StartCoroutine(Camera.main.ShakeCamera(shakeTime, shakeIntensity));
                break;
            }
        }

        if (System.Array.TrueForAll(HeartImages, x => x.activeSelf == false))
        {
            isDie = true;
            print("���� ����");
        }
    }

    void GameStartSp()
    {
        TotalSP = 10000;
        SpawnSP = 10;
    }
    void StageStart()
    {
        if (enemyObjectPool.bossStage || !enemyObjectPool.endStage)
        {            
            enemyObjectPool.stageTime = 0;
        }
        if (!enemyObjectPool.bossStage && enemyObjectPool.endStage)
        {
            enemyObjectPool.stageTime += Time.deltaTime;
        }

        if (enemyObjectPool.stageTime >=  Constant.NEXT_SPAWN_WAIT_TIME && enemyObjectPool.endStage)
        {
            LineEffectValue = Random.Range(0, 11); 

            StartCoroutine(enemyObjectPool.EnemySpawn(LineEffectValue));
            enemyObjectPool.endStage = false;
            enemyObjectPool.stageTime = 0;
        }
    }

    public IEnumerator QuestLine(int lineEffectValue , bool check)
    {
        Board.Inst.lines[lineEffectValue].LineEffect.SetActive(check);
        yield break;
    }

}
