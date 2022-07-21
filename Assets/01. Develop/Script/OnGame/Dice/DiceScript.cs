using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceScript : MonoBehaviour
{
    [SerializeField] private int diceValue;
    // 주사위 면
    private float dirX, dirY, dirZ;
    // 주사위 위치
    [SerializeField] float dirPoisitionX, dirPoisitionY;

    bool isRooling;
    float time;

    private void Start()
    {
        dirPoisitionX = transform.position.x;
        dirPoisitionY = transform.position.y;       
    }


    private void Update()
    {             
        transform.Rotate(dirX, dirY, dirZ);

        if(isRooling)
        {
            DiceAnim();
        }
    }

    public IEnumerator StartRotate()
    {
        dirX = Time.deltaTime * 1000;
        dirY = Time.deltaTime * 1000;
        dirZ = Time.deltaTime * 1000;

        isRooling = true;

        yield return new WaitForSeconds(Constant.DICE_ROLL_TIME);

        isRooling = false;

        dirX = 0; dirY = 0; dirZ = 0;

        #region 주사위 값에 따른 회전
        if (diceValue == 1)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_ONE);
        }
        if (diceValue == 2)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_TWO);
        }
        if (diceValue == 3)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_THREE);
        }
        if (diceValue == 4)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_FOUR);
        }
        if (diceValue == 5)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_FIVE);
        }
        if (diceValue == 6)
        {
            transform.rotation = Quaternion.LookRotation(Constant.DICE_SIDE_SIX);
        }
        #endregion
    }

    public int DiceValue
    {
        get => diceValue;
        set => diceValue = value;
    }

    public void DiceAnim()
    {
        if (time < 0.4f) //특정 위치에서 원점으로 이동
        {
            transform.position = new Vector2(Random.Range(-6, -7), Random.Range(-3, -5) * time);
        }
        else if (time < 0.10f) // 튕기고
        {
           transform.position = new Vector2(Random.Range(-6, -7), time - 0.4f) * 4;
        }
        else if (time < 0.20f) //다시 제자리로
        {
            transform.position = new Vector2(Random.Range(-6, -7), 0.6f - time) * 4;
        }
        else if (time < 0.30f) //튕기고
        {
            transform.position = new Vector2(Random.Range(-6, -7), (time - 0.6f) / 2) * 4;
        }
        else if (time < 0.8f) //다시 제자리
        {
            transform.position = new Vector2(Random.Range(-6, -7), 0.05f - (time - 0.7f) / 2) * 4;
        }
        else
        {
            transform.position = new Vector2(dirPoisitionX, dirPoisitionY);
        }

        time += Time.deltaTime;
        Debug.Log(time);
    }
}
