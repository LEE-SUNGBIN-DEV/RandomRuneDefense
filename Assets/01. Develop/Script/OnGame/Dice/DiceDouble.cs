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
        time += Time.deltaTime * 20;

        transform.localScale = Vector3.one * (time);  

        if(time > 4)
        {
            transform.localScale = Vector3.one * 5;
        }
    }   

    private void OnDisable()
    {
        time = 0;
        transform.localScale = Vector3.one;
    }
}
