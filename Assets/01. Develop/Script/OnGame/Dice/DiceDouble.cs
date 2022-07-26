using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDouble : MonoBehaviour
{
    float time;

    private void OnEnable()
    {
        Update();
    }

    private void Update()
    {
        time += Time.deltaTime * 1.5f;

        transform.localScale = new Vector3(2, 1, 0) * (time + 2);

        if (time > 1f)
        {
            gameObject.SetActive(false);
        }
    }   

    private void OnDisable()
    {
        time = 0;
        transform.localScale = new Vector3(2,1,0);
    }
}
