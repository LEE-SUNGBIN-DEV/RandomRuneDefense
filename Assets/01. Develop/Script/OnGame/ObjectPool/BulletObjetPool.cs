using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.gameObject.name ,Vector3.zero ,Quaternion.identity);
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
        // Ǯ���� Bullet�� �ִٸ� �ִ°� ����
        if(Instance.queue.Count > 0)
        {
            var Bullets = Instance.queue.Dequeue();
            bullets.Add(Bullets.GetComponent<RuneBullet>());
            Bullets.SetActive(true);
            return Bullets;
        }      

        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.gameObject.name, Vector3.zero, Quaternion.identity);
        queue.Enqueue(bullet);
        bullet.transform.parent = this.transform;
        bullet.SetActive(true);

        return bullet;

        //GameObject bulletObj = queue.Dequeue();
        //bulletObj.SetActive(true);
        //return bulletObj;
    }
}
