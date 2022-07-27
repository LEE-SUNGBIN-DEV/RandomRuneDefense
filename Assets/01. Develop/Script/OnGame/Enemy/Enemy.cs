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
    [SerializeField] protected static float maxHealth;
    [SerializeField] protected float moveSpeed;

     public int wayNum;
     public float distance;
    
    public GameObject HealthBar;

    public virtual void Start()
    {
        Health = Health;
        MaxHealth = MaxHealth;
        MoveSpeed = Constant.BIG_ENEMY_MOVE_SPEED;
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
            if(Health <= 0 && gameObject.activeSelf)
            {
                Die();
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
    
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
            if (MoveSpeed < 0)
            {
                MoveSpeed = 0;
                return;
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
        wayNum = 0;
        Health = MaxHealth;
        MoveSpeed = Constant.BIG_ENEMY_MOVE_SPEED;
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
        OnGameScene.Inst.TotalSP += 10;
        //MoveSpeed = Constant.BIG_ENEMY_MOVE_SPEED;        
        gameObject.SetActive(false);
    }

    public virtual void OnDisable()
    {
        MaxHealth += 10;
        HealthBar.GetComponent<Image>().color = Color.red;
        MoveSpeed = Constant.BIG_ENEMY_MOVE_SPEED;
        distance = 0;                
                
        EnemyObjectPool.Instance.enemys.Remove(this);
        EnemyObjectPool.Instance.InsertQueue(gameObject);        
    }

}
