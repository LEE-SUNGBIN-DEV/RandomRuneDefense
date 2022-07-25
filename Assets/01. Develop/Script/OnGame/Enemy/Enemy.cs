using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region SINGLETON
    public static Enemy Inst { get; private set; }
    private void Awake() => Inst = this;
    #endregion

    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float moveSpeed;

     public int wayNum;
     public float distance;
    
    public GameObject HealthBar;


    public virtual void Start()
    {
        health = 200;
        maxHealth = 200;
        MoveSpeed = Constant.BIG_ENEMY_MOVE_SPEED;
    }
    private void Update()
    {
        HealthBar.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    #region HP Property
    public float Health 
    {
        get => health;
        set
        {
            health = value;
            HealthBar.GetComponent<Image>().fillAmount = health / MaxHealth;
            if(Health <= 0 && gameObject.activeSelf)
            {
                Die();
            }
        }
    }
    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
        }
    }
    
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            if (moveSpeed <= 0)
            {
                moveSpeed = 0;
                return;
            }
            moveSpeed = value;            
        }
    }
    #endregion

    public void Damage(int damage)
    {
        Health -= damage;
        Health = Mathf.Max(0, Health);
        HealthBar.GetComponent<Image>().fillAmount = Health / MaxHealth;
    }    
    private void OnEnable()
    {
        wayNum = 0;       
        HealthBar.GetComponent<Image>().fillAmount = 1;
        StartCoroutine(MovePath());
    }
    IEnumerator MovePath()
    {
        while(true)
        {        
           transform.position = Vector2.MoveTowards(transform.position, Constant.ENEMY_WAYS[wayNum], MoveSpeed * Time.deltaTime);
           distance += MoveSpeed * Time.deltaTime;

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
        MoveSpeed = Constant.SPEED_ENEMY_MOVE_SPEED;
        MaxHealth += 10; // Á×À»¶§ ¸¶´Ù °­ÇØÁü.
        OnGameScene.Inst.TotalSP += 10;
        gameObject.SetActive(false);
    }
    public void OnDisable()
    {
        distance = 0;      
        Health = MaxHealth;       
        EnemyObjectPool.Instance.enemys.Remove(this);
        EnemyObjectPool.Instance.InsertQueue(gameObject);        
    }

}
