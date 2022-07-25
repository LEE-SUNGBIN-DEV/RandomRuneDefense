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

    [SerializeField] private int stage;
    [SerializeField] private float stageTime;

    void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        stageTime += Time.deltaTime;

        if(stageTime >= 10)
        {
            StartCoroutine(EnemySpawn());
            stageTime = 0;
        }
        ArrangeEnemies();

        // if (Input.GetKeyDown(KeyCode.Space)) // 스타트 테스트 용 
        // {
        //     StartCoroutine(EnemySpawn());
        // }
    }
    public void InsertQueue(GameObject p_object)
    {      
        queue.Enqueue(p_object);          
    }

    public GameObject GetQueue()
    {        
        // 풀링에 Enemy가 있다면 있는걸 리턴
        foreach(GameObject Enemys in queue)
        {
            if(!Enemys.activeInHierarchy)
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

    IEnumerator EnemySpawn()
    {
        Debug.Log("스테이지 시작");
        stage += 1;

        if(stage % 2 == 0 ) // 보스 
        {
            GameObject enemy = Instantiate(poolingPrefab[2], Constant.ENEMY_WAYS[0], Quaternion.identity);
            enemys.Add(enemy.GetComponent<Enemy>());
            queue.Enqueue(enemy);
            enemy.transform.parent = this.transform;
            enemy.SetActive(true);

            yield break; // 보스 나오면 스탑
        }


        if (queue.Count == 0)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject enemy = Instantiate(poolingPrefab[Random.Range(0,2)], Constant.ENEMY_WAYS[0], Quaternion.identity);
                enemys.Add(enemy.GetComponent<Enemy>());
                queue.Enqueue(enemy);
                enemy.transform.parent = this.transform;
                enemy.SetActive(true);

                yield return new WaitForSeconds(1f);
            }                      
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                GetQueue();                

                yield return new WaitForSeconds(1f);
            }       
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


