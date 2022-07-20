using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    [SerializeField] float minOffsetY;
    [SerializeField] float maxOffsetY;
    [SerializeField] TMP_Text damageTMP;

    Transform target;  
    float totalTime;

    public void Setup(Transform target, int damage)
    {
        this.target = target;
        totalTime = 0f;
        gameObject.SetActive(true);
        StartCoroutine(DamageTMP(damage));
    }

    IEnumerator DamageTMP(int damage)
    {
        while (totalTime <= 0.3f)
        {
            if (target != null)
            {              
                var targetPos = target.position;
                targetPos.y += minOffsetY;
                transform.position = targetPos;

                damageTMP.text = damage.ToString();
            }

            totalTime += Time.deltaTime;
            yield return null;
        }
        // 점점 올라가며 페이드 아웃
        totalTime = 0;
        while (totalTime <= 0.2f)
        {
            if (target != null)
            {
                float lerpTime = totalTime * 2f;

                var targetPos = target.position;
                targetPos.y += Mathf.Lerp(minOffsetY , maxOffsetY, lerpTime);
                transform.position = targetPos;

                // 페이드 아웃 하기
                damageTMP.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), lerpTime);
            }

            totalTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }

    void OnDisable()
    {        
        DamageObjectPool.Instance.InsertQueue(gameObject); 
        target = null;
        totalTime = 0f;
        damageTMP.text = "";
    }
}
