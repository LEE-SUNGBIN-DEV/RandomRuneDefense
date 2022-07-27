using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageObjectPool : MonoBehaviour
{
    public static DamageObjectPool Instance;

    [SerializeField] GameObject damageTMP;
    [SerializeField] int size;   

    public Queue<GameObject> queue = new Queue<GameObject>();

    private void Start()
    {
        Instance = this;

        for (int i = 0; i < size; ++i)
        {   
            GameObject damageTmp = Instantiate(damageTMP,this.transform.position,Quaternion.identity);
            damageTmp.transform.SetParent(transform);            
            queue.Enqueue(damageTmp);                 
            damageTmp.SetActive(false);
        }
    }

    public void InsertQueue(GameObject damageTMP)
    {
        queue.Enqueue(damageTMP);
        damageTMP.SetActive(false);
    }

    public GameObject GetQueue()
    {
        if(Instance.queue.Count >0)
        {
            var DamageTmp = Instance.queue.Dequeue();
            DamageTmp.SetActive(true);
            return DamageTmp;
        }
      
        GameObject damageTmp = Instantiate(damageTMP, this.transform.position, Quaternion.identity);
        damageTmp.transform.SetParent(transform);
        queue.Enqueue(damageTmp);
        damageTmp.SetActive(true);

        return damageTmp;
    }
}
