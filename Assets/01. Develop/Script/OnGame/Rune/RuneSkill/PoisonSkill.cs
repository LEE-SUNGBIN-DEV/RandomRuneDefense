using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSkill : RuneSkill
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {           
            StartCoroutine(OnPoison(collision));
        }
    }
    IEnumerator OnPoison(Collider2D collision)
    {
        collision.GetComponent<Enemy>().HealthBar.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        for (int i = 0; i < 3; i++)
        {
            collision.GetComponent<Enemy>().Health -= damage;
            yield return new WaitForSeconds(0.5f);
        }                    
        collision.GetComponent<Enemy>().HealthBar.GetComponent<UnityEngine.UI.Image>().color = Color.red;
    }
}
