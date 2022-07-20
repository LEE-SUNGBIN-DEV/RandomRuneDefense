using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool Instance;

    [SerializeField] GameObject poolingPrefab;
    [SerializeField] int size;
    public List<Enemy> enemys;  

    public Queue<GameObject> queue = new Queue<GameObject>();

    void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            StartCoroutine(EnemySpawn());
        }

        ArrangeEnemies();
    }
    public void InsertQueue(GameObject p_object)
    {      
        queue.Enqueue(p_object);          
    }

    public GameObject GetQueue()
    {        
        GameObject enemyObject = queue.Dequeue();
        enemys.Add(enemyObject.GetComponent<Enemy>());
        enemyObject.transform.position = Constant.enemyWays[0];
        enemyObject.SetActive(true);
        
        return enemyObject;
    }

    IEnumerator EnemySpawn()
    {
        if(queue.Count == 0)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject enemy = Instantiate(poolingPrefab, Constant.enemyWays[0], Quaternion.identity);
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


