using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    [SerializeField] float minOffsetY;
    [SerializeField] float maxOffsetY;
    [SerializeField] TMP_Text damageTmpValue;

    Transform target;  
    float totalTime;

    public TMP_Text DamageTmpValue
    {
        get => damageTmpValue;
        set => damageTmpValue = value;
    }

    public void Setup(Transform target, int damage)
    {
        this.target = target;
        totalTime = 0f;       
        StartCoroutine(DamageTMP(damage));
    }

    IEnumerator DamageTMP(int damage)
    {
        while (totalTime <= 0.1f)
        {
            if (target != null)
            {              
                var targetPos = target.position;
                targetPos.y += minOffsetY;
                transform.position = targetPos;

                DamageTmpValue.text = damage.ToString();
            }

            totalTime += Time.deltaTime;
            yield return null;
        }

        // ���� �ö󰡸� ���̵� �ƿ�
        totalTime = 0;
        while (totalTime <= 0.4f)
        {
            if (target != null)
            {
                float lerpTime = totalTime;

                var targetPos = target.position;
                targetPos.y += Mathf.Lerp(minOffsetY , maxOffsetY, lerpTime);
                transform.position = targetPos;

                // ���̵� �ƿ� �ϱ�
                DamageTmpValue.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), lerpTime);
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
        DamageTmpValue.text = null;
    }
}
