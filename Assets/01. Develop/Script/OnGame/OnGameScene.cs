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

    [Header("SUN")]
    public GameObject sun; // 태양
    [SerializeField] float sunMoveSpeed; // 태양 스피드
    public int sunWayNum; // 태양 웨이 포인트
    [SerializeField] SpriteRenderer backGround; // 배경
    [SerializeField] UB.Simple2dWeatherEffects.Standard.D2FogsPE d2FogsPE;

    EnemyObjectPool enemyObjectPool;
    public float backGroundColor;    
    public int LineEffectValue;
    public TMP_Text WaveNum;

    int totalSp; // 전체 sp
    int spawnSP; // 스폰 sp
    public bool isDie; // 죽음

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
            totalSp = value;
            if (totalSp < 0)
            {
                value = 0;               
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

    public float BackGroundColor
    {
        get => backGroundColor;
        set
        {
            backGroundColor = value;
            if (BackGroundColor > 255)
            {
                BackGroundColor = 255;              
            }
            if(BackGroundColor < 50)
            {
                BackGroundColor = 50;                
            }            
        }
    }
    #endregion

    void Start()
    {        
        BackGroundColor = 255;
        enemyObjectPool = EnemyObjectPool.Instance;
        GameStartSp();       
    }

    private void Update()
    {
        //if (!isDie)
        //{
        //    d2FogsPE.Density -= Time.deltaTime * 2;
        //}
        //if (isDie)
        //{
        //    d2FogsPE.Density += Time.deltaTime * 2;
        //    return;
        //}
        // 스테이지 표시
        WaveNum.text = enemyObjectPool.stage.ToString();

        // 마스터 만 함수 실행
        if (PhotonNetwork.IsMasterClient)       
            StageStart();
        

            if (enemyObjectPool.bossStage)
            {
                sunWayNum = 2;
                BackGroundColor -= Time.deltaTime * 50;
            }
            else
            {
                sunWayNum = 1;
                BackGroundColor += Time.deltaTime * 50;
                sun.gameObject.SetActive(true);
            }
            

            if (sun.activeInHierarchy)
            {
                SunMove(sunWayNum);
            }
            backGround.color = new Color(BackGroundColor / 255f, BackGroundColor / 255f, BackGroundColor / 255f);
        
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

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
            //d2FogsPE.EndSmoke();
            print("게임 오바");
        }
    }

    void GameStartSp()
    {
        TotalSP = 100;
        SpawnSP = 10;
    }
    
    public void StageStart()
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
        
        if(check == false)
        {
            Board.Inst.lines[lineEffectValue].LineUpgradeEffect.SetActive(false);
        }
        yield return null;
    }

    public void SunMove(int wayNum)
    {       
        sun.transform.position = Vector2.MoveTowards(sun.transform.position, Constant.SUN_WAYS[wayNum], sunMoveSpeed * Time.deltaTime);  
     
        if((Vector2)sun.transform.position == Constant.SUN_WAYS[2])
        {
            sun.gameObject.SetActive(false);
            sun.transform.position = Constant.SUN_WAYS[0];                      
        }
        
    }

}
