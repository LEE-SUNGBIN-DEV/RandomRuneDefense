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
    [SerializeField] private float stageTime;

    [HideInInspector] public bool bossStage;
    [HideInInspector] public bool endStage;

    void Awake()
    {
        endStage = true;
        Instance = this;
    }
    private void Start()
    {
        // �̸�����
        for (int i = 0; i < size; i++)
        {
            GameObject enemy = Instantiate(poolingPrefab[Random.Range(0, 2)], Constant.ENEMY_WAYS[0], Quaternion.identity);
            enemys.Add(enemy.GetComponent<Enemy>());
            queue.Enqueue(enemy);
            enemy.transform.parent = this.transform;
            enemy.SetActive(false);
        }
    }
    private void Update()
    {
        if(bossStage && !endStage)
        {
            stageTime = 0;           
        }
        if(!bossStage && endStage)
        {
            stageTime += Time.deltaTime;
        }       
              
        if(stageTime >= 5 && endStage)
        {          
            StartCoroutine(EnemySpawn());
            endStage = false;
            stageTime = 0;
        }
        ArrangeEnemies();

        if (Input.GetKeyDown(KeyCode.Space)) // ��ŸƮ �׽�Ʈ �� 
        {
            StartCoroutine(EnemySpawn());
        }
    }
    public void InsertQueue(GameObject p_object)
    {      
        queue.Enqueue(p_object);          
    }

    public GameObject GetQueue()
    {        
        // Ǯ���� Enemy�� �ִٸ� �ִ°� ����
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
        //  Ǯ���� ��á���� ���� ���� ����.
        GameObject enemy = Instantiate(poolingPrefab[Random.Range(0, 2)], Constant.ENEMY_WAYS[0], Quaternion.identity);
        enemys.Add(enemy.GetComponent<Enemy>());
        queue.Enqueue(enemy);
        enemy.transform.parent = this.transform;
        enemy.SetActive(true);
        return enemy;
    }

    IEnumerator EnemySpawn()
    {
        Debug.Log("�������� ����");
        stage += 1;

        if(stage % 2 == 0 ) // ���� 
        {
            bossStage = true;
            BossObjectPool.Instance.EnemySpawn();

            yield break; // ���� ������ ��ž
        }

        else
        {
            for (int i = 0; i < size; i++)
            {
                GetQueue();                

                yield return new WaitForSeconds(1f);
            }
            endStage = true;
        }
}

    void ArrangeEnemies()
    {
        // �Ÿ��� ������ ������ ���� ū�� ������ ũ��!
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


