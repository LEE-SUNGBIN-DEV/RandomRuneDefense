using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjetPool : MonoBehaviour
{
    public static BulletObjetPool Instance;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int size;
    public List<RuneBullet> bullets;
    public Queue<GameObject> queue = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            queue.Enqueue(bullet);
            bullet.transform.parent = this.transform;
            bullet.SetActive(false);
        }
    }

    public void InsertQueue(GameObject bullet)
    {
        queue.Enqueue(bullet);
        bullet.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject bulletObj = queue.Dequeue();
        bulletObj.SetActive(true);
        return bulletObj;
    }
}
