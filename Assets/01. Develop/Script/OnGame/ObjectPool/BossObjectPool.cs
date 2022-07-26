using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObjectPool : MonoBehaviour
{
    public static BossObjectPool Instance;

    [SerializeField] GameObject[] poolingPrefab;
    [SerializeField] int size;
    public Queue<GameObject> queue = new Queue<GameObject>();
    void Awake()
    {
        Instance = this;
    }
    public void InsertQueue(GameObject p_object)
    {
        queue.Enqueue(p_object);
    }
    public GameObject GetQueue()
    {       
        GameObject boss = queue.Dequeue();
        boss.transform.position = Constant.ENEMY_WAYS[0];
        EnemyObjectPool.Instance.enemys.Add(boss.GetComponent<BossEnemy>());
        boss.transform.parent = this.transform;
        boss.SetActive(true);

       return boss;
       
    }

    public void EnemySpawn()
    {
        if(queue.Count == 0)
        {          
             GameObject boss = Instantiate(poolingPrefab[0], Constant.ENEMY_WAYS[0], Quaternion.identity);
             EnemyObjectPool.Instance.enemys.Add(boss.GetComponent<BossEnemy>());
             queue.Enqueue(boss);
             boss.transform.parent = this.transform;
             boss.SetActive(true);                          
        }
        else
        {
             GetQueue();
        }
    }
}
