using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSkill : MonoBehaviour
{
    [SerializeField] protected float damage;

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Health -= damage;
        }      
    }   
}
