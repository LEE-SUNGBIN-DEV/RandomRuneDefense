using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using Photon.Pun;

//IPunObservable
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected static float maxHealth;
    [SerializeField] protected float originSpeed;
    [SerializeField] protected float currentSpeed;

     public int wayNum;
     public float distance;
    
    public GameObject HealthBar;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(Health);
    //    }
    //    else
    //    {
    //        this.Health = (float)stream.ReceiveNext();
    //    }
    //}

    public virtual void Awake()
    {
        MaxHealth = MaxHealth;
        Health = Health;       
        originSpeed = Constant.BIG_ENEMY_MOVE_SPEED;
    }
    private void Update()
    {
        HealthBar.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }             

    #region Property
    public float Health 
    {
        get => health;
        set
        {
            health = value;
            HealthBar.GetComponent<Image>().fillAmount = health / MaxHealth;
            if(Health < 0 && gameObject.activeSelf)
            {
                Die();
            }
            if(MaxHealth < health)
            {
                health = MaxHealth;
            }
        }
    }
    public static float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
        }
    }
    
    public float OriginSpeed
    {
        get => originSpeed;
    }
    public float CurrentSpeed
    {
        get => currentSpeed;
        set
        {
            currentSpeed = value;
            if (currentSpeed < 0)
            {
                currentSpeed = 0;
            }
        }
    }
    #endregion

    public void Damage(int damage)
    {
        Health -= damage;
        Health = Mathf.Max(0, Health);
        HealthBar.GetComponent<Image>().fillAmount = Health / MaxHealth;
    }    
    public virtual void OnEnable()
    {
        InitializeEnemy();
    }

    public void InitializeEnemy()
    {
        HealthBar.GetComponent<Image>().color = Color.red;

        wayNum = 0;
        Health = MaxHealth;        
        CurrentSpeed = OriginSpeed;
        HealthBar.GetComponent<Image>().fillAmount = 1;

        //이미 오브젝트풀로 들어간 게임오브젝트에서 코루틴을 실행시키는 걸 방지
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(MovePath());
        }
    }
    public IEnumerator MovePath()
    {
        while(true)
        {        
           transform.position = Vector2.MoveTowards(transform.position, Constant.ENEMY_WAYS[wayNum], CurrentSpeed * Time.deltaTime);
           distance += CurrentSpeed * Time.deltaTime;

           if ((Vector2)transform.position == Constant.ENEMY_WAYS[wayNum])
               wayNum++;
           
           if (wayNum == Constant.ENEMY_WAYS.Length)
           {              
                OnGameScene.Inst.DecreaseHeart();
                gameObject.SetActive(false);
                yield break;
           }

            yield return null;
        }
    }

    public virtual void Die()
    {       
        OnGameScene.Inst.TotalSP += 10;               
        gameObject.SetActive(false);
    }

    public virtual void OnDisable()
    {
        MaxHealth += 1;                
        distance = 0;                
                
        EnemyObjectPool.Instance.enemys.Remove(this);
        EnemyObjectPool.Instance.InsertQueue(gameObject);        
    }

}
