using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool Instance;

    [SerializeField] GameObject[] poolingPrefab;
    [SerializeField] int size;
    public List<Enemy> enemys;  
    public Queue<GameObject> queue = new Queue<GameObject>();

    public int stage;

    [HideInInspector] public float stageTime;

    [HideInInspector] public bool bossStage;
    [HideInInspector] public bool endStage;

    void Awake()
    {
        endStage = true;
        Instance = this;
    }
    private void Start()
    {
        // 미리생성
        for (int i = 0; i < size; i++)
        {
            GameObject enemy = Instantiate(poolingPrefab[Random.Range(0,2)], Constant.ENEMY_WAYS[0], Quaternion.identity);                       
            queue.Enqueue(enemy);
            enemy.transform.parent = this.transform;
            enemy.SetActive(false);
        }
    }
    private void Update()
    {
        ArrangeEnemies();
    }
    public void InsertQueue(GameObject p_object)
    {      
        queue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue()
    {
        // 풀링에 Enemy가 있다면 있는걸 리턴
        foreach (GameObject Enemys in queue)
        {
            if (!Enemys.activeInHierarchy)
            {
                enemys.Add(Enemys.GetComponent<Enemy>());               
                Enemys.transform.position = Constant.ENEMY_WAYS[0];
                Enemys.SetActive(true);

                return Enemys;
            }
        }
        //  풀링이 꽉찼으면 새로 만들어서 리턴.
        GameObject enemy = Instantiate(poolingPrefab[Random.Range(0, 2)], Constant.ENEMY_WAYS[0], Quaternion.identity);
        enemys.Add(enemy.GetComponent<Enemy>());
        queue.Enqueue(enemy);
        enemy.transform.parent = this.transform;
        enemy.SetActive(true);
        return enemy;
    }

    public IEnumerator EnemySpawn(int LineEffectValue)
    {

        Debug.Log("이번 강화 라인 " + LineEffectValue);
        stage += 1;
        Debug.Log(stage + " 스테이지 시작");       
        StartCoroutine(OnGameScene.Inst.QuestLine(LineEffectValue , true));
        if (stage % Constant.BOSS_STAGE == 0 ) // 보스 
        {
            Debug.Log("보스 스테이지");

            bossStage = true;
            BossObjectPool.Instance.EnemySpawn();

            yield return new WaitForSeconds(Constant.NEXT_SPAWN_WAIT_TIME);
            StartCoroutine(OnGameScene.Inst.QuestLine(LineEffectValue, false));
            yield break; // 보스 나오면 스탑            
        }
        else
        {
            for (int i = 0; i < (size + stage); ++i)
            {
                GetQueue();                

                yield return new WaitForSeconds(1f);
            }           
            endStage = true;

            yield return new WaitForSeconds(Constant.NEXT_SPAWN_WAIT_TIME);
            StartCoroutine(OnGameScene.Inst.QuestLine(LineEffectValue, false));
        }              
    }

    void ArrangeEnemies()
    {
        // 거리가 작은게 오더가 낮고 큰게 오더가 크다!
        enemys.Sort((x, y) => x.distance.CompareTo(y.distance));

        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<Order>().SetOrder(i);
        }
    }

    public Enemy GetRandomEnemy()
    {
        if(enemys.Count <= 0)
        { return null; }

        return enemys[Random.Range(0, enemys.Count)];
    }
    public Enemy GetFirstEnemy()
    {
        if (enemys.Count <= 0)
        { return null; }

        return enemys.Last();
    }
}


