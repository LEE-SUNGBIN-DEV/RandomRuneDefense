using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    public float moveSpeed;
    public int wayNum;
    public float distance;
    
    public GameObject HealthBar;


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
            //healthTMP.text = value.ToString();
        }
    }
    #endregion

    public void Damage(int damage)
    {
        Health -= damage;
        Health = Mathf.Max(0, Health);
        HealthBar.GetComponent<Image>().fillAmount = Health / maxHealth;

        if(Health <= 0 && gameObject.activeSelf)
        {
            OnGameScene.Inst.Total_SP += 10;
            gameObject.SetActive(false);
        }
    }    
    private void OnEnable()
    {
        wayNum = 0;
        StartCoroutine(MovePath());
    }
    IEnumerator MovePath()
    {
        while(true)
        {        
           transform.position = Vector2.MoveTowards(transform.position, Constant.enemyWays[wayNum], moveSpeed * Time.deltaTime);
           distance += moveSpeed * Time.deltaTime;

           if ((Vector2)transform.position == Constant.enemyWays[wayNum])
               wayNum++;
           
           if (wayNum == Constant.enemyWays.Length)
           {
                print("µµÂø");
                gameObject.SetActive(false);
                yield break;
           }

            yield return null;
        }
    }
    private void OnDisable()
    {     
        Health = maxHealth;
        distance = 0;
        moveSpeed = 0.5f;
        EnemyObjectPool.Instance.enemys.Remove(this);
        EnemyObjectPool.Instance.InsertQueue(gameObject);        
    }

}
